using System.Collections.Generic;
using UnityEngine;


// Resourcesフォルダ内のマテリアルを一時保存するクラス
public static class MaterialCache
{
    private static Dictionary<string, Material> _materialTable;
    private const string _filePath = "Materials";


    public static void Initialize()
    {
        _materialTable = new Dictionary<string, Material>();

        // Resources/Materialsフォルダ内のすべてのマテリアルを読み込み、辞書に登録
        Material[] materialList = Resources.LoadAll<Material>(_filePath);
        foreach (Material material in materialList)
        {
            if (!_materialTable.ContainsKey(material.name))
            {
                _materialTable.Add(material.name, material);
            }
        }

    }


    public static Material GetValue(string materialName)
    {
        if(_materialTable.TryGetValue(materialName, out var material)) return material;
        return null;
    }
    

    public static void ClearCache()
    {
        _materialTable.Clear();
    }
    
}
