using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuBar_Dictionary : MonoBehaviour
{
    public static Dictionary<string, GameObject> UI_table;

    public void Start()
    {
        UI_table = new Dictionary<string, GameObject>();

        //menubar_uiを全て取得して、連想配列にメニューを全て登録
        List<Transform> descendantList = DescendantObject_Search.GetListFromTag(this.gameObject.transform, "menubar_ui");
        foreach(Transform descendant in descendantList)
        {
            UI_table.Add(descendant.name, descendant.gameObject);
        }

    }

}
