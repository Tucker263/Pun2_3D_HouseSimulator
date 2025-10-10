using System.Collections.Generic;
using UnityEngine;

// タグのIDをenumで定義
public enum TagID
{
    CameraRig,
    CenterEyeAnchor,
    MenuBarPanel,
    MenuBarCanvas,
    OVRInputManager,
    UIOperationSynchronizer,
    Sun,
    Avatar,
    House,
    SmallHouse,
    Door,
    Window,
    HouseLighting,
    StreetLighting,
    Furniture,
    Ceiling,
    InnerWall,
    Stairs,
    Floor,
    Roof,
    OuterWall,
    Room,
    Structure
}

// タグを管理するクラス
public static class Tags
{
    // TagIDと文字列の対応表
    public static readonly Dictionary<TagID, string> Names = new()
    {
        { TagID.CameraRig, "CameraRig" },
        { TagID.CenterEyeAnchor, "CenterEyeAnchor" },
        { TagID.MenuBarPanel, "MenuBarPanel" },
        { TagID.MenuBarCanvas, "MenuBarCanvas" },
        { TagID.OVRInputManager, "OVRInputManager" },
        { TagID.UIOperationSynchronizer, "UIOperationSynchronizer"},
        { TagID.Sun, "Sun" },
        { TagID.Avatar, "Avatar" },
        { TagID.House, "House" },
        { TagID.SmallHouse, "SmallHouse" },
        { TagID.Door, "Door" },
        { TagID.Window, "Window" },
        { TagID.HouseLighting, "HouseLighting" },
        { TagID.StreetLighting, "StreetLighting" },
        { TagID.Furniture, "Furniture" },
        { TagID.Ceiling, "Ceiling" },
        { TagID.InnerWall, "InnerWall" },
        { TagID.Stairs, "Stairs" },
        { TagID.Floor, "Floor" },
        { TagID.Roof, "Roof" },
        { TagID.OuterWall, "OuterWall" },
        { TagID.Room, "Room" },
        { TagID.Structure, "Structure"}

    };

    // TagIDから文字列を取得
    public static string Get(TagID tagID)
    {
        return Names[tagID];
    }
}
