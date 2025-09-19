using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//オブジェクトの名前の同期を行うクラス
public class ObjectName_Synchronize : MonoBehaviourPunCallbacks
{ 

    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReflectObjName", RpcTarget.Others);

    }

    [PunRPC]
    public void ReflectObjName()
    {
        //houseの名前の同期
        GameObject house = NetworkObject_Search.GetObjectFromTag("house");
        House_Generator.NameAllObject(house);

        //house_smallの名前の同期
        GameObject houseSmall = NetworkObject_Search.GetObjectFromTag("house_small");
        HouseSmall_Generator.NameAllObject(houseSmall);

    }


}