using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;
using System.IO;


public static class LightingInfo_Manager
{

    public static void Save(string directoryPath)
    {
        string saveTag = "lighting";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        //JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach (GameObject obj in objList)
        {
            //LightingInfoに変換
            LightingInfo lighting = LightingInfo_Format.Convert(obj);
            //JSONに変換
            string jsonData = JsonUtility.ToJson(lighting);
            jsonList.Add(jsonData);

        }
        //JSONデータをセーブ
        JsonFile_Manager.Save(directoryPath, saveTag, jsonList);

    }


    public static void Load(string directoryPath)
    {
        string loadTag = "lighting";
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);

        foreach (string jsonData in jsonList)
        {
            //jsonからlightingオブジェクトに変換
            LightingInfo info = JsonUtility.FromJson<LightingInfo>(jsonData);
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, info.placeName);
            //LightingInfoを適用
            LightingInfo_Format.ApplyObject(obj, info);
                       
        }

    }

}
