using UnityEngine;

public class Door_Select : MonoBehaviour
{
    public void Select()
    {
        Door_SelectedObj.obj = this.gameObject;
    }
}
