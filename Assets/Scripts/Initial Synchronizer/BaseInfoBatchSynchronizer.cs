using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// オブジェクトBaseInfo型のバッチ同期を行うクラス
// 差分 -> バッチ化 -> 圧縮 -> 送信(1フレーム間隔に制御)

[RequireComponent(typeof(FrameBatchSender))]
public class BaseInfoBatchSynchronizer : MonoBehaviourPunCallbacks
{
    private FrameBatchSender _frameBatchSender;

    [SerializeField] private int _batchSize = 50;
    public const string RPC_METHOD_NAME = "RPC_SynchronizeBatch";

    private void Awake()
    {
        _frameBatchSender = GetComponent<FrameBatchSender>();

        // バッチサイズを指定
        BaseInfoDiffBatcher.SetBatchSize(_batchSize);
    }
    

    public void SynchronizeBatch(TagID tagID, Type infoType, Player targetPlayer)
    {
        // TagID → string
        string tag = Tags.Get(tagID);

        // タグに該当するオブジェクトを取得
        List<GameObject> objList = PhotonObjectFinder.FindObjectsWithTag(tag);

        // key: "オブジェクト名", value: BaseInfo に変換
        Dictionary<string, BaseInfo> infoDict = new Dictionary<string, BaseInfo>();
        foreach (var obj in objList)
        {
            BaseInfo info = (BaseInfo)Activator.CreateInstance(infoType);
            info.ExtractFrom(obj);
            infoDict[obj.name] = info;
        }

        // 初期状態との差分リストを取得
        List<BaseInfo> diffList = InitialBaseInfoDiffExtractor.ExtractDiffList(infoDict);

        // 差分をバッチ化
        List<string[]> batchedList = BaseInfoDiffBatcher.ConvertBatches(diffList);

        // 差分バッチを順に送信（キューに追加）
        foreach (var batch in batchedList)
        {
            // 差分バッチを圧縮
            string compressedBatch = StringCompressor.CompressBatch(batch);

            // 送信キューに追加
            _frameBatchSender.Enqueue(targetPlayer, tag, compressedBatch);

        }

    }


    [PunRPC]
    private void RPC_SynchronizeBatch(string tag, string compressedBatch)
    {
        // 圧縮バッチを展開
        string[] batchData = StringCompressor.DecompressBatch(compressedBatch);

        // バッチデータを適用
        foreach (var item in batchData)
        {
            // "型名:JSON" を分割
            int split = item.IndexOf(':');
            if (split < 0) continue;

            string typeName = item.Substring(0, split);
            string jsonData = item.Substring(split + 1);

            // 型取得
            Type infoType = Type.GetType(typeName);
            if (infoType == null) continue;

            // JSON → Info
            BaseInfo info = (BaseInfo)JsonUtility.FromJson(jsonData, infoType);
            if (info == null) continue;

            // オブジェクトを検索して反映
            GameObject obj = PhotonView.Find(info.viewID).gameObject;
            info?.ApplyTo(obj);
        }
    }
}
