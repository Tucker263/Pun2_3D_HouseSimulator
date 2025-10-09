using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FurnitureList", menuName = "Data/Furniture List")]
public class FurnitureListData : ScriptableObject
{
    public List<FurnitureData> furnitureList;
}
