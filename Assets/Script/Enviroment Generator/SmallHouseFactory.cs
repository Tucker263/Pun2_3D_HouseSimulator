using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// SmallHouseを生成するファクトリークラス
public static class SmallHouseFactory
{
    private const string SmallHousePrefabName = "Buildings/SmallHouse"; // プレハブ名


    public static GameObject Create(Vector3 position = default, Quaternion rotation = default)
    {
        // デフォルト値の設定
        if(position == default) position = new Vector3(15, 0, 0);
        if(rotation == default) rotation =  Quaternion.identity;

        GameObject smallHouse = PhotonNetwork.Instantiate(SmallHousePrefabName, position, rotation);
        smallHouse.name = smallHouse.name.Replace("(Clone)", "");

        //子孫のオブジェクトに名前をつける、名前被り対策
        AssignNumbersToAllObjects(smallHouse);

        return smallHouse;

    }


    public static void AssignNumbersToAllObjects(GameObject smallHouse)
    {
        //子孫のオブジェクトにインデックスを付ける
        NameObjectsWithIndex(smallHouse);

        //階層を振る処理、オブジェクトの下から順に、_1F
        AssignNumbersToStructures(smallHouse);

        //子孫のオブジェクトに接尾語を付ける
        NameWithSuffix(smallHouse);

    }


    private static void NameObjectsWithIndex(GameObject smallHouse)
    {
        List<Transform> children = DescendantFinder.FindDescendants(smallHouse.transform);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].name += (i + 1);
        }
    }


    private static void AssignNumbersToStructures(GameObject smallHouse)
    {
        List<Transform> structures = DescendantFinder.FindDescendantsWithTag(smallHouse.transform, Tags.Get(TagID.Structure));
        for (int i = 0; i < structures.Count; i++)
        {
            Transform structure = structures[i];
            int floorNumber = structures.Count - i;

            List<Transform> structureChildren = DescendantFinder.FindDescendants(structure);
            foreach (Transform child in structureChildren)
            {
                //階層名
                child.name += $"_{floorNumber}F";
            }

            //構造物名に階層を付ける
            structure.name += $"_{floorNumber}F";

        }

    }


    private static void NameWithSuffix(GameObject smallHouse)
    {
        List<Transform> children = DescendantFinder.FindDescendants(smallHouse.transform);
        foreach (Transform child in children)
        {
            //接尾語をつける
            child.name += "_Small";
        }

    }

}
