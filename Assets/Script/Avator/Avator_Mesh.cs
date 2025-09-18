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
   
    private GameObject vrCamera;
    private Transform vrCamera_tf;
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
            avator_tf.position = vrCamera_tf.position;
            avator_tf.rotation = vrCamera_tf.rotation;
        }

    }


    public void SetInitProperty()
    {
        creatorID = photonView.CreatorActorNr;
        isMine =  photonView.IsMine;

        //非アクティブ状態だとオブジェクトを取得できないので、Transformで取得
        GameObject parentObj = GameObject.Find("Tracking_Camera");
        Transform parent_tf= parentObj.transform;
        Transform child_tf = parent_tf.Find("VR_Camera");

        vrCamera = child_tf.gameObject;
        vrCamera_tf = vrCamera.GetComponent<Transform>();

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