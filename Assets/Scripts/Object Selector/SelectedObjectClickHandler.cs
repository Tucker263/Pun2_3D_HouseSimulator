using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// SelctedObjectの汎用クリックイベント
// アタッチ対象: オブジェクト本体

[RequireComponent(typeof(ObjectSelector))]
[RequireComponent(typeof(MenuBarTransition))]
public class SelectedObjectClickHandler : MonoBehaviour
{
    // ダブルクリック簡易判別用
    private float _lastClickTime = 0f;
    [SerializeField] private float _doubleClickThreshold = 0.3f;

    private ObjectSelector _objectSelector;
    private MenuBarTransition _menuBarTransition;

    private void Awake()
    {
        _objectSelector = GetComponent<ObjectSelector>();
        _menuBarTransition = GetComponent<MenuBarTransition>();

    }


    public void Trigger()
    {   
       
        // ダブルクリックでメニューバーの表示
        float timeSinceLastClick = Time.time - _lastClickTime;
        if (timeSinceLastClick <= _doubleClickThreshold)
        {
            // オブジェクトを選択状態にする
            _objectSelector.Select();

            // メニューバーの遷移
            _menuBarTransition.ShowUI();

        }
        _lastClickTime = Time.time;

    }

}
