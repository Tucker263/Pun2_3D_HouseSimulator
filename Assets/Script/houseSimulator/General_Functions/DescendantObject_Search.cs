using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//子孫オブジェクトを探すクラス
public static class DescendantObject_Search
{

    public static List<Transform> GetList(Transform parent)
    {
        List<Transform> descendant = new List<Transform>();

        foreach (Transform child in parent)
        {
            descendant.Add(child);
            // 再帰的に孫オブジェクトも取得
            descendant.AddRange(GetList(child));
        }

        return descendant;
    }


    public static List<Transform> GetListFromTag(Transform parent, string targetTag)
    {
        List<Transform> descendant= new List<Transform>();

        foreach (Transform child in parent)
        {
            if(child.CompareTag(targetTag))
            {
                descendant.Add(child);
            }
            // 再帰的に孫オブジェクトも取得
            descendant.AddRange(GetListFromTag(child, targetTag));
        }

        return descendant;
    }
    
}
