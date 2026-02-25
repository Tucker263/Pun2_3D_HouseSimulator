using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// Avatarを生成するファクトリークラス
public static class AvatarFactory
{
    private const string AvatarPrefabName = "Avatar_Prefab";

    public static GameObject Create(Vector3 position = default, Quaternion rotation = default)
    {
        // デフォルト値の設定
        if (position == default) position = Vector3.zero;
        if (rotation == default) rotation = Quaternion.identity;

        GameObject avatar = PhotonNetwork.Instantiate(AvatarPrefabName, position, rotation);
        avatar.name = avatar.name.Replace("(Clone)", "");
        
        return avatar;

    }

}