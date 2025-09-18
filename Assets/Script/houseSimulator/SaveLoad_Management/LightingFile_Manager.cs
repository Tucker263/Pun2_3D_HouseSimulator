using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;
using System.IO;



//照明の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class LightingInfo
{
    public string name; //照明の場所の名前,ここの名前が被るとうまくロードができなくなるので注意
    public bool enabled; //照明のオンオフ
    public float intensity; //光の明るさ
    public string lightKind;//照明の種類、常夜灯、白熱灯、LEDなど
}


public static class LightingFile_Manager
{

    public static void Save(string directoryPath)
    {
        //lighting1.jsonのような形式で保存
        string saveTag = "lighting";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);

        for(int i = 0; i < objList.Count; i++)
        {
            GameObject obj = objList[i];

            Light light = obj.GetComponent<Light>();
            TMP_Text objTMP = obj.GetComponent<TMP_Text>();
            //照明の情報を取得
            LightingInfo lighting = new LightingInfo();
            lighting.name = obj.name;
            lighting.enabled = light.enabled;
            lighting.intensity = light.intensity;
            lighting.lightKind = objTMP.text;

            //JSONに変換
            string jsonData = JsonUtility.ToJson(lighting);

            string fileName = saveTag + (i+1) + ".json";
            string filePath = Path.Combine(directoryPath, fileName);
            //ファイルに保存
            File.WriteAllText(filePath, jsonData);

        }

    }


    public static void Load(string directoryPath)
    {
        //light1.jsonのような形式を読み込み
        string loadTag = "lighting";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag, directoryPath);

        foreach (string jsonData in jsonList)
        {
            //jsonからlightingオブジェクトに変換
            LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
            GameObject obj = NetworkObject_Search.GetObjectFromTagAndName(loadTag, lighting.name);

            Light light = obj.GetComponent<Light>();
            TMP_Text tmp = obj.GetComponent<TMP_Text>();

            //Resourcesフォルダ内の照明の種類をロードして変更
            ChangeLightKind(light, lighting.lightKind);

            //lightのその他の情報を書き換え
            tmp.text = lighting.lightKind;
            light.enabled = lighting.enabled;
            light.intensity = lighting.intensity;
                       
        }

    }

    private static List<string> ReadAllFilesOfJSON(string loadTag, string directoryPath)
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


    private static void ChangeLightKind(Light targetLight, string lightKind)
    {
        //Resourcesフォルダ内のコピー元のオブジェクトをロード
        GameObject sourceObj = Resources.Load<GameObject>("Lights/"+ lightKind);
        //コピー元のlight
        Light sourceLight = sourceObj.GetComponent<Light>();
        //Lightのプロパティーを全てコピー
        LightCopier.CopyLightProperties(sourceLight, targetLight);
        
    }
}
