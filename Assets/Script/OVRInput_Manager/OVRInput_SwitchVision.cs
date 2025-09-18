using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//視点の切り替え処理、固定カメラ、VR用視点の切り替え
public class OVRInput_SwitchVision : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject fppCamera;

    void Start()
    {
        mainCamera = NetworkObject_Search.GetObjectFromName("Main_Camera");
        fppCamera = NetworkObject_Search.GetObjectFromName("FPP_Camera");

        mainCamera.SetActive(false);

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
            fppCamera.SetActive(!fppCamera.activeSelf);

        }
        
    }

}