using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


public class Transform_Position : MonoBehaviour
{
    public Vector3 position;

    private GameObject vrCamera;
    private Transform vrCamera_tf;
  

    public void SetInitProperty()
    {      
        vrCamera = NetworkObject_Search.GetObjectFromTag("vr_camera");
        vrCamera_tf = vrCamera.GetComponent<Transform>();

    }


    public void Move()
    {
        if(vrCamera == null)
        {
            SetInitProperty();
        }
        
        vrCamera_tf.position = position;
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
