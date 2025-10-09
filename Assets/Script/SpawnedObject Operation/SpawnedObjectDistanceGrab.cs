using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DistanceGrabの再現、VR想定
// アタッチ対象: SpawnedObject(家具、など)
public class SpawnedObjectDistanceGrab : MonoBehaviour
{
    // ドラッグ中に、オブジェクトを移動
    public void Grab()
    {
        Vector3 mousePos = Input.mousePosition;
        // 奥行指定、カメラから20ユニット先
        mousePos.z = 20.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;

    }

}