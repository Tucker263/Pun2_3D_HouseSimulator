using System;
using System.Collections.Generic;
using UnityEngine;


// セーブ・ロード対象の Info 型とタグをまとめたクラス
public static class SaveLoadTagMap
{
    // 固定オブジェクト
    public static readonly Dictionary<Type, List<TagID>> FixedInfoTagMap = new Dictionary<Type, List<TagID>>
    {
        // BaseInfo (VR用カメラ、太陽)
        { typeof(BaseInfo), new List<TagID> { TagID.CameraRig, TagID.Sun } },
        // MaterialInfo（天井・内壁・床・外壁・屋根・階段）
        { typeof(MaterialInfo), new List<TagID> { TagID.Ceiling, TagID.InnerWall, TagID.Floor, TagID.OuterWall, TagID.Roof, TagID.Stairs } },
        // LightingInfo（家の照明・街灯）
        { typeof(LightingInfo), new List<TagID> { TagID.HouseLighting, TagID.StreetLighting } },
        // DoorInfo (家のドア)
        { typeof(DoorInfo), new List<TagID> { TagID.Door } },
        // WindowInfo (家の窓)
        { typeof(WindowInfo), new List<TagID> { TagID.Window } },
        // SmallHouseInfo (smallHouse)
        { typeof(SmallHouseInfo), new List<TagID> { TagID.SmallHouse } }
    };


    // 生成オブジェクト
    public static readonly Dictionary<Type, List<TagID>> SpawnedInfoTagMap = new Dictionary<Type, List<TagID>>
    {
        // BaseInfo (家具)
        { typeof(BaseInfo), new List<TagID> { TagID.Furniture } }
    };


     // 全てのセーブ対象を列挙（Fixed + Spawned）
    public static IEnumerable<(Type InfoType, TagID Tag, bool IsSpawned)> EnumerateAll()
    {
        foreach (var kvp in FixedInfoTagMap)
            foreach (var tag in kvp.Value)
                yield return (kvp.Key, tag, false);

        foreach (var kvp in SpawnedInfoTagMap)
            foreach (var tag in kvp.Value)
                yield return (kvp.Key, tag, true);
    }
}
