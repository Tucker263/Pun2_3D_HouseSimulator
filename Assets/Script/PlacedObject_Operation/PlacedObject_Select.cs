using UnityEngine;

public class PlacedObject_Select : MonoBehaviour
{
    public void Select()
    {
        Placed_SelectedObj.obj = this.gameObject;
    }
}
