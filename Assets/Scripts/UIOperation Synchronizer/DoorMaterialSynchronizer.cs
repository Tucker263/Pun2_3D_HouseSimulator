using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// ドアのマテリアルの変更を行うクラス
// アタッチ対象: UIOperation Synchoronizer
[RequireComponent(typeof(PhotonView))]
public class DoorMaterialSynchronizer : MonoBehaviourPunCallbacks
{

    public void SynchronizeLocal(string materialName)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // ローカルに反映
        RPC_ApplyMaterial(targetView.ViewID, materialName);

    }


    public void SynchronizeOthers(string materialName)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        // マテリアルが一致していたら処理を中断、重複送信をなくす
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;
        Material material = MaterialCache.GetValue(materialName);
        if(material == null) return;
        if(renderer.sharedMaterial == material) return;

        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // 他のクライアントに同期
        photonView.RPC(nameof(RPC_ApplyMaterial), RpcTarget.Others, targetView.ViewID, materialName);

    }


    [PunRPC]
    private void RPC_ApplyMaterial(int viewID, string materialName)
    {
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if (obj == null) return;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;

        // ドアのマテリアルの変更
        Material material = MaterialCache.GetValue(materialName);
        if(material != null) renderer.material = material;
        else Debug.LogWarning($"MaterialResourcesCache:  {materialName} が見つかりません");

        // ドアのフレームの変更
        Transform parentObj = obj.transform.parent;
        Renderer parentRenderer = parentObj?.GetComponent<Renderer>();
        if(parentRenderer != null)parentRenderer.material = material;

    }

}