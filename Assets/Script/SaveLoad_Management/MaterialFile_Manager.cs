using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//マテリアルの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class MaterialInfo
{
    public string placeName; //場所の名前
    public string materialName; //マテリアルの名前
}


public static class MaterialFile_Manager
{

    public static void Save(string directoryPath, string saveTag)
    {
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        //JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach (GameObject obj in objList)
        {
            //MaterialInfoに変換
            MaterialInfo info = Convert(obj);

            //JSONに変換
            string jsonData = JsonUtility.ToJson(info);
            jsonList.Add(jsonData);
        }

        //JSONデータをセーブ
        JsonFile_Manager.Save(directoryPath, saveTag, jsonList);

    }


    public static void Load(string directoryPath, string loadTag)
    {
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);

        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            MaterialInfo info = JsonUtility.FromJson<MaterialInfo>(jsonData);
            //loadTagのオブジェクトを取得
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, info.placeName);
            //マテリアルの情報を適用
            Renderer renderer = obj.GetComponent<Renderer>();
            Material material = Resources.Load<Material>("Materials/" + info.materialName);
            renderer.material = material;

        }

    }
    
    public static MaterialInfo Convert(GameObject obj)
    {
        MaterialInfo info = new MaterialInfo();
        info.placeName = obj.name;

        Renderer renderer = obj.GetComponent<Renderer>();
        string materialName = renderer.material.name;
        info.materialName = materialName.Replace(" (Instance)", "");

        return info;
    }
    
}
