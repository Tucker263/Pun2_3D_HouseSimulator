using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MaterialObjのクリックイベント
public class MaterialObj_ClickEvent : MonoBehaviour
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
            MaterialObj_Select m_s = obj.GetComponent<MaterialObj_Select>();
            m_s.Select();

            //メニューバーの遷移
            MenuBar_Transition m_t = obj.GetComponent<MenuBar_Transition>();
            m_t.Transition();

        }
        lastClickTime = Time.time;

    }

}
