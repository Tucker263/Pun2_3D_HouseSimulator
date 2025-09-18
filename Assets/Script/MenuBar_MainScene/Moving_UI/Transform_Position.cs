using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


public class Transform_Position : MonoBehaviour
{
    public Vector3 position;

    private GameObject cameraRig;
    private Transform cameraRig_tf;
  

    public void SetInitProperty()
    {      
        cameraRig = GameObject.FindWithTag("camera_rig");
        cameraRig_tf = cameraRig.GetComponent<Transform>();

    }


    public void Move()
    {
        if(cameraRig == null)
        {
            SetInitProperty();
        }
        
        cameraRig_tf.position = position;
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
