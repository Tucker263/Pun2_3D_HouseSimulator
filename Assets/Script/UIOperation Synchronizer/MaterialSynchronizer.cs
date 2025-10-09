using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// マテリアルの変更を同期するクラス
// アタッチ対象: Material Synchronizer

[RequireComponent(typeof(PhotonView))]
public class MaterialSynchronizer : MonoBehaviourPunCallbacks
{
    // Interior タグセット
    private readonly HashSet<string> _interiorTagSet = new HashSet<string>
    {
        Tags.Get(TagID.Stairs),
        Tags.Get(TagID.InnerWall),
        Tags.Get(TagID.Floor),
        Tags.Get(TagID.Ceiling)
    };


    public void SynchronizeLocal(string materialName)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // ローカル反映
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

        // マテリアル変更対象のオブジェクトを取得
        List<GameObject> targetList = FindObjectsForMaterial(obj);

        // マテリアルを適用
        foreach (GameObject target in targetList)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer == null) continue;

            Material material = MaterialCache.GetValue(materialName);
            if(material != null) renderer.material = material;
            else Debug.LogWarning($"MaterialResourcesCache:  {materialName} が見つかりません");
        
        }
    }


    private List<GameObject> FindObjectsForMaterial(GameObject obj)
    {
        return _interiorTagSet.Contains(obj.tag) 
            ? FindInteriorObjects(obj) 
            : FindExteriorObjects(obj);
    }


    private List<GameObject> FindInteriorObjects(GameObject child)
    {
        // Interior: 同じ Room 配下の同タグオブジェクトを取得
        var result = new List<GameObject>();

        var ancestor = AncestorFinder.FindAncestorWithTag(child.transform, Tags.Get(TagID.Room));
        if (ancestor == null) return result;

        var descendants = DescendantFinder.FindDescendantsWithTag(ancestor, child.tag);
        foreach (var t in descendants)
        {
            result.Add(t.gameObject);
        }

        return result;
    }


    private List<GameObject> FindExteriorObjects(GameObject obj)
    {
        return PhotonObjectFinder.FindObjectsWithTag(obj.tag);
    }

}
    