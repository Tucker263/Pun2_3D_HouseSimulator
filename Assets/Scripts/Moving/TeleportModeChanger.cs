using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// 移動方式をテレポート移動に切り替えるクラス
// アタッチ対象: UIのボタン
public class TeleportModeChanger : MonoBehaviour
{

    public void OnClickChangeTeleportMode()
    {
        Debug.Log("テレポート移動に切り替わりました");

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
