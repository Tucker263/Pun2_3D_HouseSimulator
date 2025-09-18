using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


//イベントトリガー登録の同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class EventTrigger_Synchronize : MonoBehaviourPunCallbacks
{
  
    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReflectEventTrigger", RpcTarget.Others);

    }


    [PunRPC]
    public void ReflectEventTrigger()
    {
        EventTrigger_Register.register();
   
    }

}