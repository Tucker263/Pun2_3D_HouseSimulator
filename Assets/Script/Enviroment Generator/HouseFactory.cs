using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// Houseを生成するファクトリークラス
public static class HouseFactory
{
    private const string HousePrefabName = "Buildings/House"; // プレハブ名


    public static GameObject Create(Vector3 position = default, Quaternion rotation = default)
    {
        // デフォルト値の設定
        if (position == default) position = Vector3.zero;
        if (rotation == default) rotation = Quaternion.identity;

        // Houseを生成
        GameObject house =  PhotonNetwork.Instantiate(HousePrefabName, position, rotation);
        house.name = house.name.Replace("(Clone)", "");

        //番号付け、名前被り対策
        AssignNumbersToAllObjects(house);

        return house;
    }
   

    public static void AssignNumbersToAllObjects(GameObject house)
    {
        // 全ての子孫のオブジェクトに番号付け
        AssignNumbersToHouseObjects(house);
        
        //階層を振る処理、オブジェクトの下から順に、_1F
        AssignNumbersToStructures(house);

    }   


    private static void AssignNumbersToHouseObjects(GameObject house)
    {
        Transform houseTransform = house.transform;
        List<Transform> houseChildren = DescendantFinder.FindDescendants(houseTransform);

        for (int i = 0; i < houseChildren.Count; i++)
        {
            houseChildren[i].name += $"{i + 1}";
        }

    }     
    

    private static void AssignNumbersToStructures(GameObject house)
    {
        Transform houseTransform = house.transform;
        List<Transform> structureList = DescendantFinder.FindDescendantsWithTag(houseTransform, Tags.Get(TagID.Structure));

        for (int i = 0; i < structureList.Count; i++)
        {
            Transform structure = structureList[i];
            int numFloor = structureList.Count - i;

            List<Transform> structureChildren = DescendantFinder.FindDescendants(structure);
            foreach (Transform child in structureChildren)
            {
                //階層名
                child.name += $"_{numFloor}F";
            }
            
            //構造物名に階層を付ける
            structure.name += $"_{numFloor}F";

        }

    }

}
