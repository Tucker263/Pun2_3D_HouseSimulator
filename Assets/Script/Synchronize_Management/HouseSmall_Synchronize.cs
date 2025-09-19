using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//house_smallの同期を行うクラス
public class HouseSmall_Synchronize : MonoBehaviourPunCallbacks
{
  
    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        GameObject obj = NetworkObject_Search.GetObjectFromTag("house_small");

        //HouseSmallInfoに変換
        HouseSmallInfo info = HouseSmallInfo_Format.Convert(obj);
        //JSONに変換してRPC通信
        string jsonData = JsonUtility.ToJson(info);
        photonView.RPC("ReflectHouseSmall", RpcTarget.Others, jsonData);
    
    }


    [PunRPC]
    public void ReflectHouseSmall(string jsonData)
    {
        //JSONをHouseSmallInfoに変換
        HouseSmallInfo info = JsonUtility.FromJson<HouseSmallInfo>(jsonData);
        //house_smallを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTag("house_small");
        //HouseSmallInfoを適用  
        HouseSmallInfo_Format.ApplyObject(obj, info);
        
    }

}