using System;
using System.Collections.Generic;
using UnityEngine;


// BaseInfo派生クラスのセーブ・ロードクラス、固定オブジェクト
public static class FixedInfoFileManager
{
    // GameObject → BaseInfo派生クラス → JSON でセーブ
    public static void Save(string directoryPath, TagID tagID, Type infoType)
    {
        string saveTag = Tags.Get(tagID);

        List<GameObject> objList = PhotonObjectFinder.FindObjectsWithTag(saveTag);

        // JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach (var obj in objList)
        {
            // BaseInfo のインスタンスを Activator.CreateInstance で生成
            BaseInfo info = (BaseInfo)Activator.CreateInstance(infoType);
            if (info == null)
            {
                Debug.LogWarning($"InfoFileManager: {infoType} のインスタンス生成に失敗しました");
                continue;
            }

            // GameObject から情報を抽出
            info.ExtractFrom(obj);

            string jsonData = JsonUtility.ToJson(info);
            jsonList.Add(jsonData);
        }

        // JSONデータをセーブ
        JsonFileManager.Save(directoryPath, saveTag, jsonList);
    }


    // JSON → BaseInfo派生クラス → GameObject に反映
    public static void Load(string directoryPath, TagID tagID, Type infoType)
    {
        // JSONリストを取得
        string loadTag = Tags.Get(tagID);
        List<string> jsonList = JsonFileManager.Load(directoryPath, loadTag);
        if (jsonList == null || jsonList.Count == 0) return;

        // 順に反映
        foreach (var jsonData in jsonList)
        {
            // JSONをBaseInfo派生クラスに変換
            BaseInfo info = (BaseInfo)JsonUtility.FromJson(jsonData, infoType);
            if (info == null) continue;

            GameObject obj = GameObject.Find(info.name);
            if (obj == null)
            {
                Debug.LogWarning($"{loadTag}: {info.name} が見つかりませんでした");
                continue;
            }

            // Infoを適用
            info.ApplyTo(obj);
        }
        
    }
}
