using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//照明の同期を行うクラス
public class Lighting_Synchronize : MonoBehaviourPunCallbacks
{

    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> objList = NetworkObject_Search.GetListFromTag("lighting");
        foreach(GameObject obj in objList)
        {
            //LightingInfoに変換
            LightingInfo lighting = LightingInfo_Format.Convert(obj);
            //JSONに変換
            string jsonData = JsonUtility.ToJson(lighting);
            photonView.RPC("ReflectLighting", RpcTarget.Others, jsonData);

        }

    }

    [PunRPC]
    public void ReflectLighting(string jsonData)
    {
        //JSONをLightingInfoに変換
        LightingInfo info = JsonUtility.FromJson<LightingInfo>(jsonData);
        //lightingタグと名前の両方が一致しているオブジェクトを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName("lighting", info.placeName);
        //LightingInfoを適用
        LightingInfo_Format.ApplyObject(obj, info);

    }
    
}