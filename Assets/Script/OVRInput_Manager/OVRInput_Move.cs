using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//移動処理、VR想定
public class OVRInput_Move : MonoBehaviour
{
    //移動速度
    private float speed;

    //VR用カメラ
    private GameObject vrCamera;
    private Transform vrCamera_tf;


    void Start()
    {
        speed = 6f;

        vrCamera = NetworkObject_Search.GetObjectFromTag("vr_camera");
        vrCamera_tf = vrCamera.GetComponent<Transform>();

    }


    void Update()
    {
        Move();

    }


    public void Move()
    {    

        Transform obj_tf = vrCamera_tf;

        //移動処理
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        speed = Wall_Collision.isCollision ? 1f : 6f;
        obj_tf.transform.Translate(speed * Time.deltaTime * input.normalized);
 
    }

}