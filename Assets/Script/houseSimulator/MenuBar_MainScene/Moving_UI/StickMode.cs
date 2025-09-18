using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class StickMode_Button : MonoBehaviour
{

    public void Change()
    {
        Debug.Log("スティック移動に切り替わりました");

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
