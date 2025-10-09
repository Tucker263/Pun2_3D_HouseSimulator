using System;  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// 子孫オブジェクトを検索するクラス
// DFS探索 + タグ別キャッシュ + TTL管理
public static class DescendantFinder
{
    // 再利用スタックでGC削減
    private static readonly Stack<Transform> _stack = new Stack<Transform>(128);

    // 全子孫キャッシュ（親Transform単位）
    private static Dictionary<Transform, List<Transform>> _cache = new Dictionary<Transform, List<Transform>>();

    // タグ付きキャッシュ（親, タグ）単位
    private static Dictionary<(Transform parent, string tag), List<Transform>> _tagCache = new Dictionary<(Transform, string), List<Transform>>();

    // キャッシュTTL（秒）、0以下で無期限
    private static float _cacheTTL = 30f;

    // キャッシュ最終更新時間
    private static float _lastCacheTime = -999f;


    // 全子孫を取得（キャッシュ利用）
    public static List<Transform> FindDescendants(Transform parent)
    {
        if (parent == null) return new List<Transform>(0);

        RefreshCacheIfExpired();

        if (_cache.TryGetValue(parent, out var cached) && cached.Count > 0)
            return cached;

        var list = SearchDFS(parent);
        _cache[parent] = list;
        _lastCacheTime = Time.time;

        return list;
    }


    // タグが一致する子孫を取得（タグ別キャッシュ利用）
    public static List<Transform> FindDescendantsWithTag(Transform parent, string targetTag)
    {
        if (parent == null || string.IsNullOrEmpty(targetTag))
            return new List<Transform>(0);

        RefreshCacheIfExpired();

        var key = (parent, targetTag);
        if (_tagCache.TryGetValue(key, out var cached) && cached.Count > 0)
            return cached;

        var list = SearchDFS(parent, t => t.CompareTag(targetTag));
        _tagCache[key] = list;
        _lastCacheTime = Time.time;

        return list;
    }


    // 子孫から最初に見つかったコンポーネントを取得、Start()のテキスト取得で使うためキャッシュする必要なし
    public static T FindComponentInDescendants<T>(GameObject obj) where T : Component
    {
        if (obj == null) return null;

        var descendants = FindDescendants(obj.transform);
        foreach (var t in descendants)
        {
            if (t.TryGetComponent<T>(out var comp))
                return comp;
        }

        return null;
    }


    // キャッシュ全削除
    public static void ClearCache()
    {
        _cache.Clear();
        _tagCache.Clear();
        _lastCacheTime = Time.time;
    }


    // キャッシュTTL設定
    public static void SetCacheTTL(float seconds)
    {
        _cacheTTL = seconds;
    }


    // DFS探索（子孫を再帰的に取得）
    private static List<Transform> SearchDFS(Transform parent, Func<Transform, bool> predicate = null)
    {
        var result = new List<Transform>(32);
        _stack.Clear();
        _stack.Push(parent);

        while (_stack.Count > 0)
        {
            var current = _stack.Pop();
            foreach (Transform child in current)
            {
                if (predicate == null || predicate(child))
                    result.Add(child);

                _stack.Push(child);
            }
        }

        return result;
    }


    // TTL経過でキャッシュをクリア
    private static void RefreshCacheIfExpired()
    {
        if (_cacheTTL <= 0f) return;
        if (Time.time - _lastCacheTime > _cacheTTL)
            ClearCache();
    }

}
