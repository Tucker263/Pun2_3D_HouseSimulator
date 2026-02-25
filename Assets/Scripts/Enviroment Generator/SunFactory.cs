using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// 太陽を生成するファクトリークラス
public static class SunFactory
{
    private const string SunPrefabName = "Sun_Prefab"; // プレハブ名
    
    public static GameObject Create()
    {
        // デフォルト位置と回転
        Vector3 position = new Vector3(0, 100, 0);
        Quaternion rotation =  Quaternion.Euler(90, 0, 0);

        GameObject sun = PhotonNetwork.Instantiate(SunPrefabName, position, rotation);
        sun.name = sun.name.Replace("(Clone)", "");

        return sun;
        
    }

}
