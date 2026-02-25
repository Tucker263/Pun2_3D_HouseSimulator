using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// メニューバーのUIの遷移を行うクラス
// アタッチ対象: UIのボタン、オブジェクト本体
public class MenuBarTransition : MonoBehaviour
{
    [SerializeField] private string _targetUIName;


    public void ShowUI()
    {
        //すべてのUIを非表示
        foreach(var ui in MenuBarDictionary.UiTable.Values)
        {
            ui.SetActive(false);
        }

        //指定したUIを表示
        if (MenuBarDictionary.UiTable.TryGetValue(_targetUIName, out var targetUI))
        {
            targetUI.SetActive(true);
        }
        else Debug.LogWarning($"{name}: {_targetUIName} が見つかりませんでした");

    }


    public void ShowUI(string targetUIName)
    {
        _targetUIName = targetUIName;
        ShowUI();
    }

}
