using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class HouseSmallFile_Manager
{

    public static void Save(string directoryPath)
    {
        string saveTag = "house_small";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        //JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach(GameObject obj in objList)
        {
            //HouseSmallInfoに変換
            HouseSmallInfo info = HouseSmallInfo_Format.Convert(obj);
            //JSONに変換
            string jsonData = JsonUtility.ToJson(info);
            jsonList.Add(jsonData);

        }
        //JSONデータをセーブ
        JsonFile_Manager.Save(directoryPath, saveTag, jsonList);

    }


    public static void Load(string directoryPath)
    {
        string loadTag = "house_small";
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);

        foreach(string jsonData in jsonList)
        {
            //jsonからHouseSmallInfoに変換
            HouseSmallInfo info = JsonUtility.FromJson<HouseSmallInfo>(jsonData);

            GameObject obj = NetworkObject_Search.GetObjectFromTag(loadTag);
            //HouseSmallnfoを適用
            HouseSmallInfo_Format.ApplyObject(obj, info);

        }

    }

}