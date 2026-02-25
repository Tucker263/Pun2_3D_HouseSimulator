using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// オブジェクトの名前の同期を行うクラス
public class ObjectNameSynchronizer : MonoBehaviourPunCallbacks
{ 
    // 同期対象
    private readonly Dictionary<string, Action<GameObject>> _syncTargets =
    new Dictionary<string, Action<GameObject>>()
    {
        { Tags.Get(TagID.House), HouseFactory.AssignNumbersToAllObjects },
        { Tags.Get(TagID.SmallHouse), SmallHouseFactory.AssignNumbersToAllObjects },
        { Tags.Get(TagID.StreetLighting), StreetLightsFactory.AssignNumbersToAllObjects },
    };


    public void Synchronize(Player targetPlayer)
    {
        photonView.RPC(nameof(RPC_SynchronizeObjectName), targetPlayer);
    }


    [PunRPC]
    private void RPC_SynchronizeObjectName()
    {
         foreach (var kvp in _syncTargets)
        {
            string tag = kvp.Key;
            Action<GameObject> numberingAction = kvp.Value;

            GameObject obj = PhotonObjectFinder.FindWithTag(tag);
            if (obj != null) numberingAction(obj);
            else Debug.LogError($"{tag} が見つかりませんでした");

        }

    }

}
