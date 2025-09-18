using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//マテリアルの情報の同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Material_Synchronize : MonoBehaviourPunCallbacks
{
   
    public void Synchronize(string targetTag)
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(targetTag);
        foreach(GameObject obj in objList)
        {
            //MaterialInfoに変換
            MaterialInfo info = MaterialInfo_Format.Convert(obj);
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
        //マテリアルの情報を適用
        MaterialInfo_Format.ApplyObject(obj, info);

    }

}