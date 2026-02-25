using UnityEngine;
using System.Collections;


// ドアがクリックされた時に実行されるクラス
// アタッチ対象: ドア本体

[RequireComponent(typeof(DoorOpenClose))]
[RequireComponent(typeof(ObjectSelector))]
[RequireComponent(typeof(MenuBarTransition))]
public class DoorClickHandler : MonoBehaviour
{
    // クリック、ダブルクリックの排他的処理用
    private float _lastClickTime = 0f;
    [SerializeField] private float _doubleClickThreshold = 0.25f; // ダブルクリックと判定する時間間隔
    private bool _isSingleClick = false;

    private DoorOpenClose _doorOpenClose;
    private ObjectSelector _objectSelector;
    private MenuBarTransition _menuBarTransition;


    private void Awake()
    {
        _doorOpenClose = GetComponent<DoorOpenClose>();
        _objectSelector = GetComponent<ObjectSelector>();
        _menuBarTransition = GetComponent<MenuBarTransition>();

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
        // ドアの開閉
        _doorOpenClose.OnClickToggleDoor();

    }


    private void OnDoubleClick()
    {
        // オブジェクトを選択状態にする
        _objectSelector.Select();
        // メニューバーの表示
        _menuBarTransition.ShowUI();

    }

}
