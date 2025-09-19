using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


//ドアの同期を行うクラス
public class Door_Synchronize : MonoBehaviourPunCallbacks
{
  
    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> doorList = NetworkObject_Search.GetListFromTag("door");
        foreach(GameObject obj in doorList)
        {
            //DoorInfoに変換
            DoorInfo info = DoorInfo_Format.Convert(obj);
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(info);
            photonView.RPC("ReflectDoor", RpcTarget.Others, jsonData);

        }

    }


    [PunRPC]
    public void ReflectDoor(string jsonData)
    {
        //JSONをDoorInfoに変換
        DoorInfo info = JsonUtility.FromJson<DoorInfo>(jsonData);
        //doorタグと名前の両方が一致しているオブジェクトを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName("door", info.placeName);
        //DoorInfoを適用
        DoorInfo_Format.ApplyObject(obj, info);
   
    }

}