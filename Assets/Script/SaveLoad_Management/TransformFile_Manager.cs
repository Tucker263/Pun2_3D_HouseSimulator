using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


public static class TransformFile_Manager
{

    public static void Save(string directoryPath, string saveTag)
    {
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(saveTag);
        if (saveTag == "camera_rig")
        {
            objList.Add(GameObject.FindWithTag(saveTag));
        }

        //JSONのリストに変換
        List<string> jsonList = new List<string>();
        foreach (GameObject obj in objList)
        {
            string objName = obj.name;
            obj.name = objName.Replace("(Clone)", "");

            //TransformInfoに変換
            TransformInfo info = TransformInfo_Format.Convert(obj);

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
            TransformInfo_Format.ApplyObject(obj, info);

            return;
        }

        //PlacedObjectのロード
        foreach (string jsonData in jsonList)
        {
            //jsonからTransformInfoに変換
            TransformInfo info = JsonUtility.FromJson<TransformInfo>(jsonData);
            //ネットワークオブジェクト化
            PhotonNetwork.Instantiate(info.name, info.position, info.rotation);

        }

    }
  
}
