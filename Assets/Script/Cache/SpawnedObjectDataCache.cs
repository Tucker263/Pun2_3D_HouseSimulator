using System.Collections.Generic;
using UnityEngine;


// Resourcesフォルダ内のSpawnedObjectDataを一時保存するクラス
public static class SpawnedObjectDataCache
{
    // key: カテゴリ, value: ScriptableObjectのリスト
    private static Dictionary<string, List<SpawnedObjectData>> _spawnedObjectDataTable;
    private const string _basePath = "ScriptableObjects/";
    private static readonly string[] _categories = { "FurnitureData", "BuildingData", "VehicleData" }; // 将来拡張


    public static void Initialize()
    {
        _spawnedObjectDataTable = new Dictionary<string, List<SpawnedObjectData>>();

        foreach (string category in _categories)
        {
            // Resources/ScriptableObjects/各カテゴリ からロード
            SpawnedObjectData[] spawendObjDataList = Resources.LoadAll<SpawnedObjectData>(_basePath + category);

            if (spawendObjDataList.Length == 0)
            {
                Debug.LogWarning($"[SpawnedObjectDataResourcesCache] {category} にデータが見つかりませんでした。");
                continue;
            }

            _spawnedObjectDataTable.Add(category, new List<SpawnedObjectData>(spawendObjDataList));
    
        } 

    }


    public static List<SpawnedObjectData> GetValue(string category)
    {
        if(_spawnedObjectDataTable.TryGetValue(category, out var spawendObjDataList)) return spawendObjDataList;
        return null;
    }
    

    public static void ClearCache()
    {
        _spawnedObjectDataTable?.Clear();
    }
    
}
