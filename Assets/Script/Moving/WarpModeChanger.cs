using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// 移動手段をワープに切り替えるクラス
// アタッチ対象: UIのボタン
public class WarpModeChanger : MonoBehaviour
{

    public void OnClickChangeWarpMode()
    {
        Debug.Log("ワープ移動に切り替わりました");

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
