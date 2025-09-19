using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


public static class HouseSmallFile_Manager
{

    public static void Save(string directoryPath)
    {
        //house_small.jsonのような形式で保存
        string saveTag = "house_small";
        GameObject obj = NetworkObject_Search.GetObjectFromTag(saveTag);
        //HouseSmallInfoに変換
        HouseSmallInfo info = HouseSmallInfo_Format.Convert(obj);

        //JSONに変換
        string jsonData = JsonUtility.ToJson(info);

        string fileName = saveTag + ".json";
        string filePath = Path.Combine(directoryPath, fileName);
        //ファイルに保存
        File.WriteAllText(filePath, jsonData); 

    }


    public static void Load(string directoryPath)
    {
        //house_small.jsonのような形式を読み込み
        string loadTag = "house_small";
        string jsonData = ReadFileOfJSON(loadTag, directoryPath);

        //jsonからHouseSmallInfoに変換
        HouseSmallInfo info = JsonUtility.FromJson<HouseSmallInfo>(jsonData);

        GameObject obj = NetworkObject_Search.GetObjectFromTag(loadTag);
        //HouseSmallnfoを適用
        HouseSmallInfo_Format.ApplyObject(obj, info);

    }

    private static string ReadFileOfJSON(string loadTag, string directoryPath)
    {
        //フォルダ内の"{loadTag}.json"を読み込む
        //フォルダ内は"{loadTag}.json"という形式で順に保存されている
        string fileName = loadTag + ".json";
        string filePath = Path.Combine(directoryPath, fileName);
        string jsonData = "";
        //ファイルが存在するか確認
        if (File.Exists(filePath))
        {
            //ファイルからJSONデータを読み込む
            jsonData = File.ReadAllText(filePath);
        }
        else
        {
            Debug.Log("house_smallファイルが見つかりませんでした。");
        }
           
        return jsonData;
    }

}