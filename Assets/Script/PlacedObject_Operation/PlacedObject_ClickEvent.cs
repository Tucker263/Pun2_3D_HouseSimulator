using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlacedObjectのクリックイベント
public class PlacedObject_ClickEvent : MonoBehaviour
{
    //ダブルクリック判別用
    private static float lastClickTime = 0f;
    private static float doubleClickThreshold = 0.3f;

    public void Trigger()
    {
        //ダブルクリックでメニューバーの表示
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            //オブジェクトを選択
            GameObject obj = this.gameObject;
            Object_Select o_s = obj.GetComponent<Object_Select>();
            o_s.Select();

            //メニューバーの遷移
            MenuBar_Transition m_t = obj.GetComponent<MenuBar_Transition>();
            m_t.Transition();

        }
        lastClickTime = Time.time;

    }

}
