using System.Collections.Generic;
using UnityEngine;


// メニューバーのUIをディクショナリで管理するクラス
// アタッチ対象: Canvas MenuBar
public class MenuBarDictionary : MonoBehaviour
{
    public static Dictionary<string, GameObject> UiTable;


    private void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        UiTable = new Dictionary<string, GameObject>();

        // "MenuBarPanel" タグがついたすべての子孫Transformを取得
        List<Transform> descendantList = DescendantFinder.FindDescendantsWithTag(this.transform, Tags.Get(TagID.MenuBarPanel));

        foreach (Transform descendant in descendantList)
        {
            if (!UiTable.ContainsKey(descendant.name))
            {
                UiTable.Add(descendant.name, descendant.gameObject);
            }
            else
            {
                Debug.LogWarning($"UI名 '{descendant.name}' が既に登録されています。");
            }

        }

    }

}
