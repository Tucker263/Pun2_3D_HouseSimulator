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
            //ドアの情報を取得
            DoorInfo door = new DoorInfo();

            door.name = obj.name;
            //ドアの開閉状態
            Door_OpenClose door_OpenClose = obj.GetComponent<Door_OpenClose>();
            bool isOpen = door_OpenClose.GetIsOpen();
            door.isOpen = isOpen;
            //ドアのrotation
            door.rotation = obj.GetComponent<Transform>().rotation;
            //ドアのマテリアル
            string materialName = obj.GetComponent<Renderer>().material.name;
            door.materialName = materialName.Replace(" (Instance)", "");
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(door);
            photonView.RPC("ReflectDoor", RpcTarget.Others, jsonData);

        }

    }


    [PunRPC]
    public void ReflectDoor(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        DoorInfo door = JsonUtility.FromJson<DoorInfo>(jsonData);
        //doorタグと名前の両方が一致しているオブジェクトを探す
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName("door", door.name);

        //名前が被るとうまく反映ができなくなるので注意
        obj.name = door.name;
        //開閉状態を反映
        Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
        d_o.SetIsOpen(door.isOpen);
        //rotationを反映
        obj.GetComponent<Transform>().rotation = door.rotation;

        //マテリアルを適用
        Renderer renderer = obj.GetComponent<Renderer>();
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/" + door.materialName);
        renderer.material = material; 
   
    }

}