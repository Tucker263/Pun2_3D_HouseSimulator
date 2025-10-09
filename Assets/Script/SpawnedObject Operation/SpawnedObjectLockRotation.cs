using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 生成したオブジェクトの回転を固定するクラス
// アタッチ対象: SpawnedObject(家具、など)
public class SpawnedObjectLockRotation : MonoBehaviour
{

    private void Update()
    {
        //X軸、Z軸だけ固定
        //DistanceGrabで掴むとrigitBodyが機能しないので、Transformで指定
        Vector3 currentRotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);
    }

}