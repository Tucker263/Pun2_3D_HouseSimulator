using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class JsonFile_Manager
{

    public static void Save(string directoryPath, string saveTag, List<string> jsonList)
    {
        //{saveTag}.jsonのような形式で保存
        for (int i = 0; i < jsonList.Count; i++)
        {
            string fileName = $"{saveTag}{i + 1}.json";
            string filePath = Path.Combine(directoryPath, fileName);
            //ファイルに保存
            File.WriteAllText(filePath, jsonList[i]);

        }
        
    }


    public static List<string> Load(string directoryPath, string loadTag)
    {
        //フォルダ内の全ての"{loadTag}.json"を読み込む
        List<string> jsonList = new List<string>();
        int i = 1;
        while (true)
        {
            string fileName = $"{loadTag}{i}.json";
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
            i++;
        }

        return jsonList;
    }

}
