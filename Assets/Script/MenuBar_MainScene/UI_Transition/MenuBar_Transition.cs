using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//メニューバーのUIの遷移を行うクラス
public class MenuBar_Transition : MonoBehaviour
{
    public string nextName;

    public void Transition()
    {
        //全てのUIを閉じる
        foreach(var value in MenuBar_Dictionary.UI_table.Values)
        {
            value.SetActive(false);
        }

       //次のUIを開く
        GameObject obj = MenuBar_Dictionary.UI_table[nextName];
        obj.SetActive(true);
    
    }

}