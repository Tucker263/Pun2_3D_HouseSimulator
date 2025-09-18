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
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);
        if (saveTag == "camera_rig")
        {
            objList.Add(GameObject.FindWithTag(saveTag));
        }

        //JSONリストに変換
        List<string> jsonList = new List<string>();
        foreach (GameObject obj in objList)
        {
            string objName = obj.name;
            obj.name = objName.Replace("(Clone)", "");

            //TransformInfoに変換
            TransformInfo info = ConvertFromObject(obj);

            //JSONに変換
            string jsonData = JsonUtility.ToJson(info);
            jsonList.Add(jsonData);
        }

        //JSONデータをセーブ
        JsonFile_Manager.Save(directoryPath, saveTag, jsonList);

    }


    public static void Load(string directoryPath, string loadTag)
    {
        List<string> jsonList = JsonFile_Manager.Load(directoryPath, loadTag);

        if (loadTag == "camera_rig" || loadTag == "sun")
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
            ApplyObject(obj, info);

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

    
    public static TransformInfo ConvertFromObject(GameObject obj)
    {
        TransformInfo info = new TransformInfo();
        info.name = obj.name;
        info.position = obj.GetComponent<Transform>().position;
        info.rotation = obj.GetComponent<Transform>().rotation;

        return info;
    }
    

    public static void ApplyObject(GameObject obj, TransformInfo info)
    {
        obj.name = info.name;
        Transform obj_tf = obj.GetComponent<Transform>();
        obj_tf.position = info.position;
        obj_tf.rotation = info.rotation;

    }
  
}
