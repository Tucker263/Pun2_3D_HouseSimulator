using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// SmallHouseの表示・非表示を切り替えるクラス
// アタッチ対象: SmallHouse Synchronizer

[RequireComponent(typeof(PhotonView))]
public class SmallHouseVisibilitySynchronizer : MonoBehaviourPunCallbacks
{
    private GameObject _smallHouse;


    private void Initialize()
    {
        _smallHouse = PhotonObjectFinder.FindWithTag(Tags.Get(TagID.SmallHouse));
        if (_smallHouse == null) Debug.LogWarning($"{Tags.Get(TagID.SmallHouse)}が見つかりませんでした");
    }


    public void SynchronizeLocal(bool isActive)
    {
        // ローカル反映
        RPC_ApplyVisibility(isActive);
    }


    public void SynchronizeOthers(bool isActive)
    {
        // 他クライアントの同期
        photonView.RPC(nameof(RPC_ApplyVisibility), RpcTarget.Others, isActive);
    }


    [PunRPC]
    private void RPC_ApplyVisibility(bool isActive)
    {
        if (_smallHouse == null) Initialize();
        
        if (_smallHouse != null) _smallHouse.SetActive(isActive);

    }
    
}
