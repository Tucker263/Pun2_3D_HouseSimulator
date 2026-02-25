using System;
using System.Collections.Generic;
using UnityEngine;


// BaseInfoの初期状態を保持するクラス、固定オブジェクト
public static class FixedBaseInfoInitCache
{
    // key: "オブジェクト名", value: JSON文字列
    private static Dictionary<string, string> _cache;


    public static void Initialize()
    {
        _cache = new Dictionary<string, string>();

        // InfoTypeごとにタグを取得してキャッシュ
        foreach (var kv in InitialSyncTagMap.FixedInfoTagMap)
        {
            Type infoType = kv.Key;
            List<TagID> tags = kv.Value;

            string[] tagStrings = tags.ConvertAll(t => Tags.Get(t)).ToArray();;
            // タグのオブジェクトをキャッシュ
            CacheObjectsByTags(tagStrings, infoType);
        }
    }


    private static void CacheObjectsByTags(string[] tagList, Type infoType)
    {

        foreach (string tag in tagList)
        {
            List<GameObject> objList = PhotonObjectFinder.FindObjectsWithTag(tag);
            foreach (var obj in objList)
            {
                // BaseInfo生成してオブジェクトから情報抽出
                BaseInfo info = (BaseInfo)Activator.CreateInstance(infoType);
                info.ExtractFrom(obj);

                 // オブジェクト名がキー
                string key = obj.name;
                string json = JsonUtility.ToJson(info);
                // キャッシュ保存
                _cache[key] = json;
            }
        }

    }


    public static string GetValue(string key)
    {
        return _cache.TryGetValue(key, out var json) ? json : null;
    }


    public static void ClearCache()
    {
        _cache.Clear();
    }

}
