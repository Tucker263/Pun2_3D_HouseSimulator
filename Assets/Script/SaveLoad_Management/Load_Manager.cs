using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class Load_Manager
{
    private static string dataPath = Application.persistentDataPath;
    private static string directoryPath = Path.Combine(dataPath, Config.directoryName);

    public static void Load()
    {
        //ディレクトリのパスを更新
        directoryPath = Path.Combine(dataPath, Config.directoryName);
        //「初めから」の場合
        if (Config.isInitialStart)
        {
            if (Directory.Exists(directoryPath))
            {
                //ディレクトリを中身ごと削除して、新たに生成
                Directory.Delete(directoryPath, true);
            }
            Directory.CreateDirectory(directoryPath);
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            return;
        }


        //位置情報をロード
        //VR用カメラ、太陽、家具のロード処理
        List<string> transformTagList = new List<string>()
        {
            "camera_rig",
            "sun",
            "furniture"
        };
        foreach(string tag in transformTagList)
        {
            TransformFile_Manager.Load(directoryPath, tag);
        }


        //マテリアルの情報をロード
        //家の内壁、床、外壁、屋根、天井のロード処理
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
            MaterialFile_Manager.Load(directoryPath, tag);
        }


        //ドアのロード処理
        DoorFile_Manager.Load(directoryPath);
        //照明のロード処理
        LightingFile_Manager.Load(directoryPath);
        //house_smallの情報のロード処理
        HouseSmallFile_Manager.Load(directoryPath);
        
    }
}   
        
