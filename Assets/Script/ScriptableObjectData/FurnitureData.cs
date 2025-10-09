using UnityEngine;

// 家具データ
[CreateAssetMenu(fileName = "NewFurnitureData", menuName = "Data/Furniture Data")]
public class FurnitureData : SpawnedObjectData
{
    [Header("カテゴリ")]
    public string subCategory;             // Chair / Table / Decoration
}
