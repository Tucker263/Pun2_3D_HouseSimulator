using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DistanceGrabの再現、VR想定
// アタッチ対象: SpawnedObject(家具、など)
public class SpawnedObjectDistanceGrab : MonoBehaviour
{
    // CameraRig
    private GameObject _cameraRig;
    // FPP Camera
    private GameObject _fppCamera;
    private const string _fppCameraName = "FPP_Camera";


    private void Start()
    {
        _cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (_cameraRig == null) Debug.LogError($"{Tags.Get(TagID.CameraRig)}が見つかりませんでした");
        
        _fppCamera = _cameraRig.transform.Find(_fppCameraName).gameObject;
        if (_fppCamera == null) Debug.LogError($"{_fppCameraName}が見つかりませんでした");
    }


    // ドラッグ中に、オブジェクトを移動
    public void Grab()
    {
        Vector3 mousePos = Input.mousePosition;
        // 奥行指定、FPPカメラから4.5ユニット先
        mousePos.z = _fppCamera.activeSelf ? 4.5f : 20.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;

    }

}