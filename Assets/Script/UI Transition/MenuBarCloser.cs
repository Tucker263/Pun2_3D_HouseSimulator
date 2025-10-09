using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// メニューバーを閉じるクラス
// アタッチ対象: UIのボタン

[RequireComponent(typeof(MenuBarTransition))]
[RequireComponent(typeof(ObjectSelector))]
public class MenuBarCloser : MonoBehaviour
{
    private const string _defaultUIName = "Panel_Default";

    private MenuBarTransition _menuBarTransition;
    private ObjectSelector _objectSelector;


    private void Awake()
    {
        _menuBarTransition = GetComponent<MenuBarTransition>();
        _objectSelector = GetComponent<ObjectSelector>();
    }


    public void OnClickClosePanel()
    {
        // オブジェクトの選択を解除
        _objectSelector.Unselect();
        // デフォルトメニューに戻る
        _menuBarTransition.ShowUI(_defaultUIName);

    }

}
