using System;
using System.Collections.Generic;
using UnityEngine;


// 新規プレイヤー同期用の Info 型とタグをまとめたクラス
public static class InitialSyncTagMap
{
    // 固定オブジェクト
    public static readonly Dictionary<Type, List<TagID>> FixedInfoTagMap = new Dictionary<Type,  List<TagID>>
    {
        // WindowInfo (家の窓)
        { typeof(WindowInfo), new  List<TagID> { TagID.Window } },
        // DoorInfo (家のドア)
        { typeof(DoorInfo), new  List<TagID> { TagID.Door } },
        // LightingInfo（家の照明・街灯）
        { typeof(LightingInfo), new  List<TagID> { TagID.HouseLighting, TagID.StreetLighting } },
        // SmallHouseInfo (smallHouse)
        { typeof(SmallHouseInfo), new  List<TagID> { TagID.SmallHouse } },
        // MaterialInfo（天井・内壁・床・外壁・屋根・階段）
        { typeof(MaterialInfo), new List<TagID> { TagID.Ceiling, TagID.InnerWall, TagID.Floor, TagID.OuterWall, TagID.Roof, TagID.Stairs } }
    };
}
