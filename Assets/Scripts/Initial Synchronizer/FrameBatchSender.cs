using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// フレーム単位でバッチを順次送信するクラス
[RequireComponent(typeof(PhotonView))]
public class FrameBatchSender : MonoBehaviourPun
{
    // 送信キューの内部クラス
    private class BatchItem
    {
        public Player TargetPlayer; // 送信先
        public string Tag;          // タグ
        public string Batch;      // バッチデータ
    }
    private Queue<BatchItem> _queue = new Queue<BatchItem>();

    [SerializeField] private int _batchesPerFrame = 1; // 1フレームあたりに送信するバッチ数


    // Updateで毎フレーム送信
    private void Update()
    {
        int sent = 0;
        while (_queue.Count > 0 && sent < _batchesPerFrame)
        {
            var item = _queue.Dequeue();
            photonView.RPC(BaseInfoBatchSynchronizer.RPC_METHOD_NAME, item.TargetPlayer, item.Tag, item.Batch);
            sent++;
        }
    }

    // バッチリストをキューに追加
    public void Enqueue(Player targetPlayer, string tag, string batch)
    {
         _queue.Enqueue(new BatchItem
        {
            TargetPlayer = targetPlayer,
            Tag = tag,
            Batch = batch
        });

    }

    // 送信キューをクリア
    public void ClearQueue()
    {
        _queue.Clear();
    }

}
