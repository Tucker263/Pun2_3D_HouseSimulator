using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


// セーブ・ロードを行うクラス
public static class SaveLoadManager
{
    private static string _dataPath = Application.persistentDataPath;
    private static string _directoryPath = Path.Combine(_dataPath, Config.DirectoryName);

    public static void SaveAll()
    {
        // 常に最新の状態にしたいので、データを初期化(中身ごと削除)してセーブ
        Directory.Delete(_directoryPath, true);
        Directory.CreateDirectory(_directoryPath);

        // BaseInfo型のセーブ、(固定,生成オブジェクト)
        foreach (var (infoType, tag, isSpawned) in SaveLoadTagMap.EnumerateAll())
        {
            if (isSpawned)
                SpawnedInfoFileManager.Save(_directoryPath, tag, infoType);
            else
                FixedInfoFileManager.Save(_directoryPath, tag, infoType);
        }

    }


    public static void LoadAll()
    {
        //ディレクトリのパスを更新
        _directoryPath = Path.Combine(_dataPath, Config.DirectoryName);
        //「初めから」の場合
        if (Config.IsInitialStart)
        {
            if (Directory.Exists(_directoryPath))
            {
                //ディレクトリを中身ごと削除して、新たに生成
                Directory.Delete(_directoryPath, true);
            }
            Directory.CreateDirectory(_directoryPath);
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
            return;
        }


        // BaseInfo型のロード、(固定,生成オブジェクト)
        foreach (var (infoType, tag, isSpawned) in SaveLoadTagMap.EnumerateAll())
        {
            if (isSpawned)
                SpawnedInfoFileManager.Load(_directoryPath, tag, infoType);
            else
                FixedInfoFileManager.Load(_directoryPath, tag, infoType);
        }

    }
        
}
