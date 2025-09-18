using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


//ドアの同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Door_Synchronize : MonoBehaviourPunCallbacks
{
  
    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> doorList = NetworkObject_Search.GetListFromTag("door");
        foreach(GameObject obj in doorList)
        {
            //DoorInfoに変換
            DoorInfo info = DoorFile_Manager.Convert(obj);
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(info);
            photonView.RPC("ReflectDoor", RpcTarget.Others, jsonData);

        }

    }


    [PunRPC]
    public void ReflectDoor(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        DoorInfo info = JsonUtility.FromJson<DoorInfo>(jsonData);
        //doorタグと名前の両方が一致しているオブジェクトを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName("door", info.placeName);
        //ドアの情報を適用
        DoorFile_Manager.ApplyObject(obj, info);
   
    }

}