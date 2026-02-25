using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// イベントトリガーを登録するクラス
public static class EventTriggerRegister
{
    public static void Register()
    {
        // ドアのイベントを登録
        DoorEventRegister.Register(Tags.Get(TagID.Door));
        // 窓のイベントを登録
        WindowEventRegister.Register(Tags.Get(TagID.Window));
        // 家の照明のイベントを登録
        HouseLightingEventRegister.Register(Tags.Get(TagID.HouseLighting));

        // 家の天井、内壁、床、階段、外壁、屋根、家具のイベントを登録
        string[] materialTags =
        {
            Tags.Get(TagID.Ceiling),
            Tags.Get(TagID.InnerWall),
            Tags.Get(TagID.Floor),
            Tags.Get(TagID.Stairs),
            Tags.Get(TagID.OuterWall),
            Tags.Get(TagID.Roof),
            Tags.Get(TagID.Furniture)
        };
        foreach (string tag in materialTags)
        {
            SelectedObjectEventRegister.Register(tag);
        }

    }

}
      