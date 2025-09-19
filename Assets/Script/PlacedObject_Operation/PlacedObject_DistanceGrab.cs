using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//DistanceGrabの再現、VR想定
public class PlacedObject_DistanceGrab : MonoBehaviour
{
    Transform placedObj_tf;

    void Start()
    {
        placedObj_tf = GetComponent<Transform>();
    }

    //ドラッグ中に、オブジェクトを移動
    public void Grab()
    {
        Vector3 mousePos = Input.mousePosition;
        //奥行指定、カメラから20ユニット先
        mousePos.z = 20.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        placedObj_tf.position = worldPos;

    }

}