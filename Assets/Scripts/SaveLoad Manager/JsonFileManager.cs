using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


// JSONファイルをセーブ・ロードするクラス
// 圧縮に gzip + base64 を使用
public static class JsonFileManager
{

    public static void Save(string directoryPath, string saveTag, List<string> jsonList)
    {
        if (jsonList == null || jsonList.Count == 0) return;

        // {saveTag}.jsonのような形式で保存 
        for (int i = 0; i < jsonList.Count; i++)
        {
            string fileName = $"{saveTag}{i + 1}.json.gz";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // 圧縮後、ファイルを保存
                string compressed = StringCompressor.Compress(jsonList[i]);
                File.WriteAllText(filePath, compressed);
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSONデータのセーブに失敗しました: {filePath}\n{ex}");
            }
        }
    }


    public static List<string> Load(string directoryPath, string loadTag)
    {
        List<string> jsonList = new List<string>();
        
        // {loadTag}.jsonのような形式で読み込み
        try
        {
            string[] files = Directory.GetFiles(directoryPath, $"{loadTag}*.json.gz");
            Array.Sort(files); // 連番順に並べ替え

            foreach (string filePath in files)
            {
                // ファイルの読み込み後、解凍
                string compressed = File.ReadAllText(filePath);
                string jsonData = StringCompressor.Decompress(compressed);
                jsonList.Add(jsonData);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"JSONデータのロードに失敗しました: {directoryPath}\n{ex}");
        }

        return jsonList;
    }
}
