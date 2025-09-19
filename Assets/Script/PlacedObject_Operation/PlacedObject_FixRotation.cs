using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacedObject_FixRotation : MonoBehaviour
{

    void Update()
    {
        //X軸、Z軸だけ固定
        //DistanceGrabで掴むとrigitBodyが機能しないので、Transformで指定
        Vector3 currentRotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);
    }

}