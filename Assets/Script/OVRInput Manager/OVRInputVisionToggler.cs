using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 視点の切り替え処理、固定カメラ、VR用視点の切り替え
// アタッチ対象: OVRInput Vision Toggler
public class OVRInputVisionToggler : MonoBehaviour
{
    private GameObject _mainCamera;
    private GameObject _fppCamera;

    private const string _mainCameraName = "MainCamera";
    private const string _fppCameraName = "FPP_Camera";

    private void Start()
    {
        _mainCamera = GameObject.Find(_mainCameraName);
        _fppCamera = GameObject.Find(_fppCameraName);
        if (_mainCamera == null) Debug.LogError($"{_mainCameraName}が見つかりませんでした");
        if (_fppCamera == null) Debug.LogError($"{_fppCameraName}が見つかりませんでした");

        _mainCamera.SetActive(false);
        _fppCamera.SetActive(true);

    }

    private void Update()
    {
        ToggleVision();
    }
    

    public void ToggleVision()
    {
        // Enterで視点の切り替え
        if(Input.GetKeyDown(KeyCode.Return))
        {
            _mainCamera.SetActive(!_mainCamera.activeSelf);
            _fppCamera.SetActive(!_fppCamera.activeSelf);

        }
        
    }

}