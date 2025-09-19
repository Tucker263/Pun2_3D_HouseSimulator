using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlacedObject_RotateY : MonoBehaviour
{
    private Slider slider;


    public void SetInitProperty()
    {
        slider = GetComponent<Slider>();
    }


    public void RotateY()
    {
        if(slider == null)
        {
            SetInitProperty();
        }

        GameObject obj = SelectedObject.obj;

        //所有権の変更
        PlacedObject_Ownership p_o =  obj.GetComponent<PlacedObject_Ownership>();
        p_o.Change();

        //オブジェクトのy軸の回転
        float rotateY = slider.value;
        Transform obj_tf = obj.GetComponent<Transform>();
        obj_tf.eulerAngles = new Vector3(0, rotateY, 0);

    }
}
