using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// Photonオブジェクトを検索するクラス
// SpawnedObjectは動的に変わるので、キャッシュする際は注意!
public static class PhotonObjectFinder
{
    // タグ別のキャッシュ
    private static Dictionary<string, List<GameObject>> _cache = new Dictionary<string, List<GameObject>>();

    // キャッシュの有効期間（秒） 0以下で無期限
    private static float _cacheTTL = 30f;
    private static float _lastCacheTime = -999f;


    public static GameObject FindWithTag(string tag)
    {
        // キャッシュ更新チェック
        RefreshCacheIfExpired();

        // キャッシュを検索
        var cached = GetValue(tag);
        if(cached != null && cached.Count > 0) return cached[0];

        // オブジェクトを全探索
        var list = FindObjects(obj => obj.CompareTag(tag));
        if(list.Count == 0) return null;

        // キャッシュ登録
        _cache[tag] = list;
        
        return list[0];
    }


    public static List<GameObject> FindObjectsWithTag(string tag)
    {
        // キャッシュ更新チェック
        RefreshCacheIfExpired();

         // キャッシュを検索
        var cached = GetValue(tag);
        if(cached != null && cached.Count > 0) return cached;

        // オブジェクトを全探索
        var list = FindObjects(obj => obj.CompareTag(tag));
        if(list.Count == 0) return new List<GameObject>();

       // キャッシュ登録
        _cache[tag] = list;

        return list;
    }


    public static void ClearCache()
    {
        _cache.Clear();
        _lastCacheTime = Time.time;
    }


    public static void SetCacheTTL(float seconds)
    {
        _cacheTTL = seconds;
    }


    private static List<GameObject> GetValue(string key)
    {
        return _cache.TryGetValue(key, out var list) ? list : null;
    }


    private static List<GameObject> FindObjects(Func<GameObject, bool> predicate)
    {
        List<GameObject> result = new List<GameObject>();

        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (predicate(obj))
                result.Add(obj);
        }

        return result;
    }


    // TTL経過によるキャッシュクリア
    private static void RefreshCacheIfExpired()
    {
        if (_cacheTTL <= 0f) return; // 無期限
        if (Time.time - _lastCacheTime > _cacheTTL)
            ClearCache();
    }

}
