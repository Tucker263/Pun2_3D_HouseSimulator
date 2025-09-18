using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


//照明がクリックされた時に実行されるクラス、Invokeで待つことになるので少し反応が遅くなってしまう
public class Lighting_ClickEvent : MonoBehaviour
{
    //クリック、ダブルクリックの排他的処理
    private float clickTime = 0f; // 最後にクリックされた時間
    private float doubleClickThreshold = 0.25f; // ダブルクリックと判定する時間間隔
    private bool isSingleClick = false; // シングルクリックが発生したかどうか
    

    public void Trigger()
    {

        if (Time.time - clickTime < doubleClickThreshold)
        {
            //ダブルクリック処理
            isSingleClick = false; // シングルクリックを無効化
            OnDoubleClick();
        }
        else
        {
            //シングルクリックの可能性を一旦記録
            isSingleClick = true;

            // doubleClickThresholdミリ秒後に実行
            Invoke("HandleSingleClick", doubleClickThreshold);
        }
        clickTime = Time.time; // クリック時間を更新*/

    }

   public void HandleSingleClick()
    {
        if (isSingleClick)
        {
            // シングルクリック処理
            OnSingleClick();
        }
    }


    public void OnSingleClick()
    {
        //シングルクリックで照明のオンオフ
        GameObject obj = this.gameObject;
        Lighting_OnOff l_oo = obj.GetComponent<Lighting_OnOff>();
        l_oo.OnOffLight();

    }


    public void OnDoubleClick()
    {
        //ダブルクリックでメニューバーの表示
        GameObject obj = this.gameObject;
        Lighting_Select l_s = obj.GetComponent<Lighting_Select>();
        l_s.Select();
        MenuBar_Transition m_t = obj.GetComponent<MenuBar_Transition>();
         m_t.Transition();

    }


}
