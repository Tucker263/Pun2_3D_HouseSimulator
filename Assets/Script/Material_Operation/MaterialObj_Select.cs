using UnityEngine;

public class MaterialObj_Select : MonoBehaviour
{
    public void Select()
    {
        Material_SelectedObj.obj = this.gameObject;
    }
}
