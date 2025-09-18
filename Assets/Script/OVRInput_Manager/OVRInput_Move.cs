using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//移動処理、VR想定
public class OVRInput_Move : MonoBehaviour
{
    //移動速度
    private float speed;

    //CameraRig
    private GameObject cameraRig;
    private Transform cameraRig_tf;


    void Start()
    {
        speed = 6f;

        cameraRig = GameObject.FindWithTag("camera_rig");
        cameraRig_tf = cameraRig.GetComponent<Transform>();

    }


    void Update()
    {
        Move();

    }


    public void Move()
    {
        Transform obj_tf = cameraRig_tf;

        //移動処理
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        speed = Wall_Collision.isCollision ? 1f : 6f;
        obj_tf.transform.Translate(speed * Time.deltaTime * input.normalized);

    }

}