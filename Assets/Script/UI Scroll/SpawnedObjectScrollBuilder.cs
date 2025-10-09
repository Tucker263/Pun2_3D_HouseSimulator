using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


// 生成するオブジェクトのスクロールコンテンツを生成するクラス
// アタッチ対象: Content

[RequireComponent(typeof(AutoContentSize))]
public class SpawnedObjectScrollBuilder : MonoBehaviour
{
    [Header("カテゴリ名 (例: FurnitureData, BuildingData)")]
    [SerializeField] private string _categoryKey;

    [Header("UIプレハブ参照")]
    [SerializeField] private GameObject _buttonPrefab;  // ボタンプレハブ

    private List<SpawnedObjectData> _spawnedObjectDataList; // ScriptableObjectのリスト
    private Transform _contentParent;  // ScrollView > Viewport > Content
    private AutoContentSize _autoContentSize;


    private void Awake()
    {
        _contentParent = transform;
        _autoContentSize = GetComponent<AutoContentSize>();
    }


    private void Start()
    {
        _spawnedObjectDataList = SpawnedObjectDataCache.GetValue(_categoryKey);

        if (_spawnedObjectDataList == null || _spawnedObjectDataList.Count == 0)
        {
            Debug.LogWarning($"{name}: {_categoryKey} のデータが見つかりません。");
            return;
        }

        BuildScrollContent();

    }


    private void BuildScrollContent()
    {
        foreach (var spawnedObjectData in _spawnedObjectDataList)
        {
            // ボタン生成
            GameObject buttonObj = SpawnedObjectButtonFactory.Create(_buttonPrefab, _contentParent, spawnedObjectData);
            // ボタンイベント登録
            UISpawnedObjectEventRegister.Register(buttonObj, spawnedObjectData);
        }

        // contentのサイズを更新
        _autoContentSize.UpdateContentHeight();

    }

}
