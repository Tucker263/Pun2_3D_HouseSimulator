using System;
using System.Collections.Generic;
using UnityEngine;


// 初期状態のBaseInfo型の差分を抽出するクラス
public static class InitialBaseInfoDiffExtractor
{
    // 差分リストを取得
    // currentObjects: 現在のオブジェクト(key: "オブジェクト名", value: BaseInfo)
    public static List<BaseInfo> ExtractDiffList(Dictionary<string, BaseInfo> currentObjDict)
    {
        List<BaseInfo> diffList = new List<BaseInfo>();

        foreach (var kv in currentObjDict)
        {
            // 現在の状態をJSON化
            BaseInfo currentInfo = kv.Value;
            string currentJson = JsonUtility.ToJson(currentInfo);

            // 初期状態キャッシュを取得
            string key = kv.Key;
            string initialJson = FixedBaseInfoInitCache.GetValue(key);

            if (initialJson == null) continue;

            // JSONが異なる場合のみ差分として追加
            if (currentJson != initialJson)
            {
                diffList.Add(currentInfo);
            }
        }

        return diffList;
    }
}
