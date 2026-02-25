using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// イベント登録の同期を行うクラス
public class EventTriggerSynchronizer : MonoBehaviourPunCallbacks
{
  
    public void Synchronize(Player targetPlayer)
    {
        photonView.RPC(nameof(RPC_SynchronizeEventTrigger), targetPlayer);
    }

    [PunRPC]
    public void RPC_SynchronizeEventTrigger()
    {
        EventTriggerRegister.Register();
   
    }

}