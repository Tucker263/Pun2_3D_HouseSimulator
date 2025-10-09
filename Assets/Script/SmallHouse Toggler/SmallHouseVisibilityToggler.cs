using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// SmallHouseの表示・非表示を切り替えるクラス
// アタッチ対象: UIのボタン
public class SmallHouseVisibilityToggler : MonoBehaviour
{
    private SmallHouseVisibilitySynchronizer _smallHouseVisibilitySynchronizer;

    private void Start()
    {
        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _smallHouseVisibilitySynchronizer = uiOperationSynchronizer.GetComponentInChildren<SmallHouseVisibilitySynchronizer>();

    }

    public void OnClickToggleVisibility(bool isActive)
    {
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        // ローカル反映
        _smallHouseVisibilitySynchronizer.SynchronizeLocal(isActive);

        // 他クライアントの同期
        _smallHouseVisibilitySynchronizer.SynchronizeOthers(isActive);

    }
    
}
