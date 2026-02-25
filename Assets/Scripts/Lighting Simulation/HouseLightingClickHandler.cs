using UnityEngine;
using System.Collections;


// 家の照明がクリックされた時に実行されるクラス
// アタッチ対象: 家の照明

[RequireComponent(typeof(ObjectSelector))]
[RequireComponent(typeof(MenuBarTransition))]
[RequireComponent(typeof(LightOnOff))]
public class HouseLightingClickHandler : MonoBehaviour
{
    // クリック、ダブルクリックの排他的処理用
    private float _lastClickTime = 0f;                
    [SerializeField] private float _doubleClickThreshold = 0.3f; // ダブルクリックと判定する時間間隔
    private bool _isSingleClick = false;

    private ObjectSelector _objectSelector;
    private MenuBarTransition _menuBarTransition;
    private LightOnOff _lightOnOff; 


    private void Awake()
    {
        _objectSelector = GetComponent<ObjectSelector>();
        _menuBarTransition = GetComponent<MenuBarTransition>();
        _lightOnOff = GetComponent<LightOnOff>();
    }


    public void Trigger()
    {
        float timeSinceLastClick = Time.time - _lastClickTime;
        
        if (timeSinceLastClick < _doubleClickThreshold)
        {
            _isSingleClick = false;
            // ダブルクリック処理
            OnDoubleClick();
        }
        else
        {
            _isSingleClick = true;
            // _doubleClickThreshold秒後に実行、シングルクリックも少し反応が遅くなる
            Invoke(nameof(HandleSingleClick), _doubleClickThreshold);
        }

        // クリック時間を更新
        _lastClickTime = Time.time;

    }


    private void HandleSingleClick()
    {
        if (_isSingleClick)
        {
            OnSingleClick();
        }

    }


    private void OnSingleClick()
    {
        // オブジェクトを選択状態にする
        _objectSelector.Select();
        // 照明のオンオフ
        _lightOnOff.ToggleLight();

    }


    private void OnDoubleClick()
    {
        // オブジェクトを選択状態にする
        _objectSelector.Select();
        // メニューバーの表示
        _menuBarTransition.ShowUI();

    }
    
}
