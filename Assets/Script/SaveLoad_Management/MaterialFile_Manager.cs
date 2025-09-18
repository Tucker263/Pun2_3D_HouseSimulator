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
        //{saveTag}.jsonのような形式で保存
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        for(int i = 0; i < objList.Count; i++)
        {
            GameObject obj = objList[i];

            Renderer renderer = obj.GetComponent<Renderer>();
            //マテリアルの情報を取得
            MaterialInfo info = new MaterialInfo();
            info.placeName = obj.name;
            string materialName = renderer.material.name;
            info.materialName = materialName.Replace(" (Instance)", "");

            //JSONに変換
            string jsonData = JsonUtility.ToJson(info);

            string fileName = saveTag + (i+1) + ".json";
            string filePath = Path.Combine(directoryPath, fileName);
            //ファイルに保存
            File.WriteAllText(filePath, jsonData);

        }

    }


    public static void Load(string directoryPath, string loadTag)
    {
        List<string> jsonList = ReadAllFilesOfJSON(directoryPath, loadTag);

        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            MaterialInfo info = JsonUtility.FromJson<MaterialInfo>(jsonData);
            //loadTagのオブジェクトを手に入れる
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, info.placeName);
         
            Renderer renderer = obj.GetComponent<Renderer>();
            //Resourcesフォルダ内のマテリアルをロード
            Material material = Resources.Load<Material>("Materials/"+ info.materialName);
            //ロードしたマテリアルをオブジェクトに適用
            renderer.material = material;
              
        }

    }


    private static List<string> ReadAllFilesOfJSON(string directoryPath, string loadTag)
    {
        //フォルダ内の全ての"{loadTag}.json"を読み込む
        //フォルダ内は"{loadTag}.json"という形式で順に保存されている

        List<string> jsonList = new List<string>();
        int index = 1;
        while (true)
        {
            string fileName = loadTag + index + ".json";
            string filePath = Path.Combine(directoryPath, fileName);
            // ファイルが存在するか確認
            if (File.Exists(filePath))
            {
                // ファイルからJSONデータを読み込む
                string jsonData = File.ReadAllText(filePath);
                jsonList.Add(jsonData);
            }
            else
            {
                break;
            }
            index++;
        }

        return jsonList;
    }
}
