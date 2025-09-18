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
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);

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

}
