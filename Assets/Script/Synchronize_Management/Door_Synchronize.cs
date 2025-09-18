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

        //名前が被るとうまく反映ができなくなるので注意
        obj.name = info.placeName;
        //開閉状態を反映
        Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
        d_o.SetIsOpen(info.isOpen);
        //rotationを反映
        obj.GetComponent<Transform>().rotation = info.rotation;

        //マテリアルを適用
        Renderer renderer = obj.GetComponent<Renderer>();
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/" + info.materialName);
        renderer.material = material; 
   
    }

}