using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


public static class DoorInfo_Manager
{

    public static void Save(string directoryPath)
    {
        string saveTag = "door";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        //JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach (GameObject obj in objList)
        {
            //DoorInfoに変換
            DoorInfo door = DoorInfo_Format.Convert(obj);
            //JSONに変換
            string jsonData = JsonUtility.ToJson(door);
            jsonList.Add(jsonData);

        }

        //JSONデータをセーブ
        JsonFile_Manager.Save(directoryPath, saveTag, jsonList);

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
            //DoorInfoを適用
            DoorInfo_Format.ApplyObject(obj, info);

        }

    }

}
