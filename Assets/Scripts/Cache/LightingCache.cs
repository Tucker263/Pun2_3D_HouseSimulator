using System.Collections.Generic;
using UnityEngine;


// Resourcesフォルダ内のライティングを一時保存するクラス
public static class LightingCache
{
    private static Dictionary<string, GameObject> _lightingTable;
    private const string _filePath = "Lightings";


    public static void Initialize()
    {
        _lightingTable = new Dictionary<string, GameObject>();

        // Resources/Lightingsフォルダ内のすべてのGameObjectを読み込み、辞書に登録
        GameObject[] lighingList = Resources.LoadAll<GameObject>(_filePath);
        foreach (GameObject lighting in lighingList)
        {
            if(!_lightingTable.ContainsKey(lighting.name))
            {
                _lightingTable.Add(lighting.name, lighting);
            }
        }

    }


    public static GameObject GetValue(string lightingName)
    {
        if(_lightingTable.TryGetValue(lightingName, out var lighting)) return lighting;
        return null;
    }
    

    public static void ClearCache()
    {
        _lightingTable.Clear();
    }

}
