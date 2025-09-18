using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//ドアの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class DoorInfo
{
    public string name; //ここの名前が被るとうまくロードができなくなるので注意
    public bool isOpen;
    public Quaternion rotation;
    public string materialName;
}


public static class DoorFile_Manager
{

    public static void Save(string directoryPath)
    {
        //door1.jsonのような形式で保存
        string saveTag = "door";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        for(int i = 0; i < objList.Count; i++)
        {
            GameObject obj = objList[i];
            Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
            Renderer renderer = obj.GetComponent<Renderer>();

            //ドアの情報を取得
            DoorInfo door = new DoorInfo();
            door.name = obj.name;
            door.isOpen = d_o.GetIsOpen();
            door.rotation = obj.GetComponent<Transform>().rotation;
            string materialName = renderer.material.name;
            door.materialName = materialName.Replace(" (Instance)", "");

            //JSONに変換
            string jsonData = JsonUtility.ToJson(door);

            string fileName = saveTag + (i+1) + ".json";
            string filePath = Path.Combine(directoryPath, fileName);
            //ファイルに保存
            File.WriteAllText(filePath, jsonData);

        }

    }


    public static void Load(string directoryPath)
    {
        string loadTag = "door";
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);
        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            DoorInfo door = JsonUtility.FromJson<DoorInfo>(jsonData);
            //ネットワークオブジェクト化したhouseからdoorを手に入れる
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, door.name);
      
            Renderer renderer = obj.GetComponent<Renderer>();
            obj.name = door.name;
            //ドアの開閉状態を適用
            Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
            d_o.SetIsOpen(door.isOpen);
            //ドアの回転を適用
            obj.GetComponent<Transform>().rotation = door.rotation;
            //Resourcesフォルダ内のマテリアルをロード
            Material material = Resources.Load<Material>("Materials/"+ door.materialName);
            //ロードしたマテリアルをオブジェクトに適用
            renderer.material = material;
             
        }

    }

}
