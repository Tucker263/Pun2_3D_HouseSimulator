using UnityEngine;

public class Lighting_Select : MonoBehaviour
{
    public void Select()
    {
        Lighting_Selected.placeName = this.gameObject.name;
    }
}
