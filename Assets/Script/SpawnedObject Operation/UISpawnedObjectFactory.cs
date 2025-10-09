using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// オブジェクトを生成するクラス
// アタッチ対象: UIのボタン
public class UISpawnedObjectFactory : MonoBehaviour
{

    private SpawnedObjectEventSynchronizer _spawnedObjectEventSynchronizer;


    private void Initialize()
    {
         GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _spawnedObjectEventSynchronizer = uiOperationSynchronizer.GetComponentInChildren<SpawnedObjectEventSynchronizer>();

    }


    public void OnClickCreate(SpawnedObjectData spawnedObjectData)
    {
        if(_spawnedObjectEventSynchronizer == null) Initialize();

        // selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        // オブジェクトの生成
        GameObject obj = SpawnedObjectFactory.Create(spawnedObjectData);

        // ローカル反映、イベント登録
        _spawnedObjectEventSynchronizer.SynchronizeLocal(obj);

        // 他のクライアントに同期
        _spawnedObjectEventSynchronizer.SynchronizeOthers(obj);

    }
    
}
