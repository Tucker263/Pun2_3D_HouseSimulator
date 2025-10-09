using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 生成されたオブジェクトのイベント登録を同期するクラス
// アタッチ対象: SpawnedObject Synchronizer

[RequireComponent(typeof(PhotonView))]
public class SpawnedObjectEventSynchronizer : MonoBehaviourPunCallbacks
{

    public void SynchronizeLocal(GameObject obj)
    {
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // ローカル反映
        RPC_RegisterEvent(targetView.ViewID);
        //RPC_RegisterRayInteractable(targetView.viewID);

    }


    public void SynchronizeOthers(GameObject obj)
    {
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // 他のクライアントに同期
        photonView.RPC(nameof(RPC_RegisterEvent), RpcTarget.Others, targetView.ViewID);
        //photonView.RPC(nameof(RPC_RegisterRayInteractable), RpcTarget.Others, targetView.ViewID);

    }
   

    [PunRPC]
    private void RPC_RegisterEvent(int viewID)
    {
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if(obj == null) return;

        SelectedObjectEventRegister.RegisterFromObj(obj);

    }

    /*
   [PunRPC]
   private void RPC_RegisterRayInteractable(int viewID)
   {
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if(obj == null) return;

        SelectedObjectEventRegister.RegisterFromObj(obj);
        
   }
   */
    
}
