using UnityEngine;


// 窓のクリック時のイベント処理を行うクラス
// アタッチ対象: 窓本体

[RequireComponent(typeof(WindowSliding))]
[RequireComponent(typeof(ObjectSelector))]
[RequireComponent(typeof(MenuBarTransition))]
public class WindowClickHandler : MonoBehaviour
{
    private WindowSliding _windowSliding;
    private ObjectSelector _objectSelector;
    private MenuBarTransition _menuBarTransition;

    private void Awake()
    {
        _windowSliding = GetComponent<WindowSliding>();
        _objectSelector = GetComponent<ObjectSelector>();
        _menuBarTransition = GetComponent<MenuBarTransition>();

    }


    public void Trigger()
    {
        // オブジェクトを選択状態にする
        _objectSelector.Select();
        // 画面の遷移
        _menuBarTransition.ShowUI();
        // 窓の開閉
        _windowSliding.ToggleWindow();
        
    }
    
}
