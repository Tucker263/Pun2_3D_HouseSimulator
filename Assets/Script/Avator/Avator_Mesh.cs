using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Avator_Mesh : MonoBehaviourPunCallbacks
{
    private int creatorID;
    private bool isMine;
   
    private GameObject cameraRig;
    private Transform cameraRig_tf;
    private Transform avator_tf;

    void Start()
    {
        SetInitProperty();

    }

    void Update()
    {
        if(isMine)
        {
            //アバターのメッシュをVR用カメラの動きと同期させる
            avator_tf = GetComponent<Transform>();
            avator_tf.position = cameraRig_tf.position;
            avator_tf.rotation = cameraRig_tf.rotation;
        }

    }


    public void SetInitProperty()
    {
        creatorID = photonView.CreatorActorNr;
        isMine =  photonView.IsMine;

        //CameraRigを取得
        cameraRig = GameObject.FindWithTag("camera_rig");
        cameraRig_tf = cameraRig.GetComponent<Transform>();

        avator_tf = GetComponent<Transform>();

    }

    public int GetCreatorID()
    {
        return creatorID;
    }


    public bool IsMine()
    {
        return isMine;
    }

}