using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//ドアの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class DoorInfo
{
    public string placeName; //場所の名前
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

        for (int i = 0; i < objList.Count; i++)
        {
            GameObject obj = objList[i];
            //DoorInfoに変換
            DoorInfo door = Convert(obj);

            //JSONに変換
            string jsonData = JsonUtility.ToJson(door);

            string fileName = saveTag + (i + 1) + ".json";
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
            DoorInfo info = JsonUtility.FromJson<DoorInfo>(jsonData);
            //ネットワークオブジェクト化したhouseからdoorを手に入れる
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, info.placeName);

            obj.name = info.placeName;
            //ドアの開閉状態を適用
            Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
            d_o.SetIsOpen(info.isOpen);
            //ドアの回転を適用
            obj.GetComponent<Transform>().rotation = info.rotation;
            //Resourcesフォルダ内のマテリアルをロード
            Renderer renderer = obj.GetComponent<Renderer>();
            Material material = Resources.Load<Material>("Materials/" + info.materialName);
            //ロードしたマテリアルをオブジェクトに適用
            renderer.material = material;

        }

    }

    public static DoorInfo Convert(GameObject obj)
    {
        DoorInfo info = new DoorInfo();

        info.placeName = obj.name;
        //開閉状態
        Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
        info.isOpen = d_o.GetIsOpen();
        //rotation
        info.rotation = obj.GetComponent<Transform>().rotation;
        //マテリアル
        Renderer renderer = obj.GetComponent<Renderer>();
        string materialName = renderer.material.name;
        info.materialName = materialName.Replace(" (Instance)", "");

        return info;
        
    }

}
