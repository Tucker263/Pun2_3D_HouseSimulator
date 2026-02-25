using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 先祖オブジェクトを探すクラス
public static class AncestorFinder
{

    // 子オブジェクトから親をたどって、特定のタグを持つ先祖Transformを探す。
    public static Transform FindAncestorWithTag(Transform child, string targetTag)
    {

        Transform current = child.parent;

        while (current != null)
        {
            if (current.CompareTag(targetTag))
            {
                return current;
            }
            current = current.parent;
        }

        return null;
        
    }

}