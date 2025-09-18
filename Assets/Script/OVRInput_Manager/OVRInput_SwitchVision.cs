using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//視点の切り替え処理、固定カメラ、VR用視点の切り替え
public class OVRInput_SwitchVision : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject vrCamera;

    void Start()
    {
        //非アクティブ状態だとオブジェクトを取得できないので、Transformで取得
        GameObject parentObj = GameObject.Find("Tracking_Camera");
        Transform parent_tf= parentObj.transform;

        //MainCameraを取得
        Transform main_tf = parent_tf.Find("Main_Camera");
        mainCamera = main_tf.gameObject;
        //VRCameraを取得
        Transform vr_tf = parent_tf.Find("VR_Camera");
        vrCamera = vr_tf.gameObject;

    }

    void Update()
    {
        SwitchVision();
    }
    

    public void SwitchVision()
    {
        //視点の切り替え処理、Enter
        if(Input.GetKeyDown(KeyCode.Return))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            vrCamera.SetActive(!vrCamera.activeSelf);

        }
        
    }

}