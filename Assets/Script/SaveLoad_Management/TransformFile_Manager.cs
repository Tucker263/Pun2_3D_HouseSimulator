using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

//Transformの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class TransformInfo
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}


public static class TransformFile_Manager
{

    public static void Save(string directoryPath, string saveTag)
    {
        //{saveTag}.jsonのような形式で保存
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);
        if (saveTag == "camera_rig")
        {
            objList.Add(GameObject.FindWithTag(saveTag));
        }

        for (int i = 0; i < objList.Count; i++)
            {
                GameObject obj = objList[i];
                string objName = obj.name;
                objName = objName.Replace("(Clone)", "");

                //Transform情報を取得
                TransformInfo info = new TransformInfo();
                info.name = objName;
                info.position = obj.GetComponent<Transform>().position;
                info.rotation = obj.GetComponent<Transform>().rotation;

                //JSONに変換
                string jsonData = JsonUtility.ToJson(info);

                string fileName = saveTag + (i + 1) + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                //ファイルに保存
                File.WriteAllText(filePath, jsonData);

            }
        
    }


    public static void Load(string directoryPath, string loadTag)
    {
        //{loadTag}.jsonのような形式を読み込み
        List<string> jsonList = ReadAllFilesOfJSON(directoryPath, loadTag);

        if(loadTag == "camera_rig" || loadTag == "sun")
        {

            string jsonData = jsonList[0];
            //jsonからTransformInfoに変換
            TransformInfo info = JsonUtility.FromJson<TransformInfo>(jsonData);

            //Transformの情報を適用
            GameObject obj = NetworkObject_Search.GetObjectFromTag(loadTag);
            if (loadTag == "camera_rig")
            {
                obj = GameObject.FindWithTag(loadTag);
            }
            obj.name = info.name;
            Transform obj_tf = obj.GetComponent<Transform>();
            obj_tf.position = info.position;
            obj_tf.rotation = info.rotation;

            return;
        }

        //MovedObjectのロード
        foreach (string jsonData in jsonList)
        {
            //jsonからTransformInfoに変換
            TransformInfo info = JsonUtility.FromJson<TransformInfo>(jsonData);
            //ネットワークオブジェクト化
            PhotonNetwork.Instantiate(info.name, info.position, info.rotation);

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
