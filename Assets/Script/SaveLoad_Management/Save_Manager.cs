using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class Save_Manager
{
    private static string dataPath = Application.persistentDataPath;
    private static string directoryPath = Path.Combine(dataPath, Config.directoryName);

    public static void Save()
    {
        //常に最新の状態にしたいので、データを初期化(中身ごと削除)してセーブ
        Directory.Delete(directoryPath, true);
        Directory.CreateDirectory(directoryPath);

        //位置情報をセーブ
        //VR用カメラ、太陽、家具のセーブ処理
        List<string> transformTagList = new List<string>()
        {
            "camera_rig",
            "sun",
            "furniture"
        };
        foreach(string tag in transformTagList)
        {
            TransformFile_Manager.Save(directoryPath, tag);
        }


        //マテリアルの情報をセーブ
        //家の内壁、床、外壁、屋根、天井のセーブ処理
        List<string> materialTagList = new List<string>()
        {
            "innerWall",
            "floor",
            "outerWall",
            "roof",
            "ceiling"
        };
        foreach(string tag in materialTagList)
        {
            MaterialFile_Manager.Save(directoryPath, tag);
        }


        //ドアのセーブ処理
        DoorFile_Manager.Save(directoryPath);
        //照明のセーブ処理
        LightingFile_Manager.Save(directoryPath);  
        
        //house_smallの情報のセーブ処理
        HouseSmallFile_Manager.Save(directoryPath);

    }

}