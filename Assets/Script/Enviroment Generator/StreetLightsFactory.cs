using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// StreetLightsを生成するファクトリークラス
public static class StreetLightsFactory
{
    private const string StreetLightsPrefabName = "StreetLights_Prefab"; // プレハブ名


    public static GameObject Create(Vector3 position = default, Quaternion rotation = default)
    {
        // デフォルト値の設定
        if (position == default) position = Vector3.zero;
        if (rotation == default) rotation = Quaternion.identity;

        // StreetLightsを生成
        GameObject streetLights = PhotonNetwork.Instantiate(StreetLightsPrefabName, position, rotation);
        streetLights.name = streetLights.name.Replace("(Clone)", "");

        // 全ての子孫のオブジェクトに番号付け、名前被り対策
        AssignNumbersToAllObjects(streetLights);

        return streetLights;

    }


    public static void AssignNumbersToAllObjects(GameObject streetLights)
    {
        List<Transform> streetLightsChildren = DescendantFinder.FindDescendantsWithTag(streetLights.transform, Tags.Get(TagID.StreetLighting));
        for (int i = 0; i < streetLightsChildren.Count; i++)
        {
            streetLightsChildren[i].name += $"{i + 1}";
        }

    }
    
}
