using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// TransformView以外の情報の同期を管理するクラス: 新規プレイヤーが入ってきた時
// アタッチ対象: Initial Synchronizer

[RequireComponent(typeof(ObjectNameSynchronizer))]
[RequireComponent(typeof(EventTriggerSynchronizer))]
[RequireComponent(typeof(RayInteractableSynchronizer))]
[RequireComponent(typeof(BaseInfoBatchSynchronizer))]
public class InitialSynchronizer : MonoBehaviourPunCallbacks
{
    private ObjectNameSynchronizer _objectNameSynchronizer; // オブジェクトの名前の同期
    private EventTriggerSynchronizer _eventTriggerSynchronizer; // イベントトリガー登録の同期、VR環境以外で必要
    private RayInteractableSynchronizer  _rayInteractableSynchronizer; // RayInteractable登録の同期、VR環境で必要
    private BaseInfoBatchSynchronizer _baseInfoBatchSynchronizer; // オブジェクトbaseInfo型のバッチ同期


    private void Awake()
    {
        _objectNameSynchronizer = GetComponent<ObjectNameSynchronizer>();
        _eventTriggerSynchronizer = GetComponent<EventTriggerSynchronizer>();
        _rayInteractableSynchronizer = GetComponent<RayInteractableSynchronizer>();
        _baseInfoBatchSynchronizer = GetComponent<BaseInfoBatchSynchronizer>();

    }


     public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //マスタークライアント以外は処理を中断
        if(!PhotonNetwork.IsMasterClient) return;

        //新しいプレイヤーの同期処理
        Debug.Log("新しいプレイヤーが参加しました。状態を同期します。");
        SynchronizeAll(newPlayer);
        Debug.Log("状態の同期が完了しました。");

    }
 
 
    public void SynchronizeAll(Player targetPlayer)
    {
        // オブジェクトの場所の名前の同期
        _objectNameSynchronizer.Synchronize(targetPlayer);

        // イベントトリガー登録の同期
        _eventTriggerSynchronizer.Synchronize(targetPlayer);

        //  RayInteractableの同期
        //_rayInteractableSynchronizer.Synchronize(targetPlayer);

        // BaseInfo型の同期、固定オブジェクト
        foreach (var kv in InitialSyncTagMap.FixedInfoTagMap)
        {
            Type infoType = kv.Key;
            List<TagID> tags = kv.Value;

            foreach (var tag in tags)
            {
                _baseInfoBatchSynchronizer.SynchronizeBatch(tag, infoType, targetPlayer);
            }
        }

    }

}
