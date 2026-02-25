using UnityEngine;


// オブジェクトを選択するクラス
// アタッチ対象: オブジェクト本体、UI
public class ObjectSelector : MonoBehaviour
{
    public void Select()
    {
        SelectedObject.obj = this.gameObject;
    }

    public void Unselect()
    {
        SelectedObject.obj = null;
    }
}
