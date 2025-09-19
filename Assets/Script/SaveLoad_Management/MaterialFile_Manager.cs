using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


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
            MaterialInfo info = MaterialInfo_Format.Convert(obj);

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
            MaterialInfo_Format.ApplyObject(obj, info);

        }

    }
    
}
