using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//house_smallの情報の同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class HouseSmall_Synchronize : MonoBehaviourPunCallbacks
{
  
    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        GameObject obj = NetworkObject_Search.GetObjectFromTag("house_small");

        //house_smallの情報を取得
        HouseSmallInfo house_small = new HouseSmallInfo();
        house_small.name = obj.name;
        house_small.isActive = obj.activeSelf;
        //JSONに変換してRPC通信
        string jsonData = JsonUtility.ToJson(house_small);
        photonView.RPC("ReflectHouseSmall", RpcTarget.Others, jsonData);
    
    }


    [PunRPC]
    public void ReflectHouseSmall(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        HouseSmallInfo house_small = JsonUtility.FromJson<HouseSmallInfo>(jsonData);
        //house_smallを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTag("house_small");
        
        obj.name = house_small.name;
        //アクティブ情報の反映
        obj.SetActive(house_small.isActive);
        
    }

}