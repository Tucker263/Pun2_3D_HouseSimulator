using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//マテリアルの情報の同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class MaterialInfo_Synchronize : MonoBehaviourPunCallbacks
{
   
    public void Synchronize(string targetTag)
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(targetTag);
        foreach(GameObject obj in objList)
        {
            //マテリアルの情報を取得
            MaterialInfo info = new MaterialInfo();
            info.placeName = obj.name;
            string materialName = obj.GetComponent<Renderer>().material.name;
            info.materialName = materialName.Replace(" (Instance)", "");
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(info);
            photonView.RPC("ReflectMaterial", RpcTarget.Others, targetTag, jsonData);
        }

    }


    [PunRPC]
    public void ReflectMaterial(string targetTag, string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        MaterialInfo info = JsonUtility.FromJson<MaterialInfo>(jsonData);
        //同期するタグと名前の両方が一致しているオブジェクトを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(targetTag, info.placeName);

        //場所の名前が被るとうまく反映ができなくなるので注意
        Renderer renderer = obj.GetComponent<Renderer>();
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/" + info.materialName);
        //ロードしたマテリアルをオブジェクトに適用
        renderer.material = material;

    }

}