using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class PlacedObject_Generator : MonoBehaviourPunCallbacks
{
    private string objName;

    private GameObject cameraRig;
    private Transform cameraRig_tf;
    public float Length;
    
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        objName = buttonTMP.text;

        cameraRig = GameObject.FindWithTag("camera_rig");
        cameraRig_tf = cameraRig.GetComponent<Transform>();
    }


    public void Generate()
    {
        //自分がどんな方向を見ても、自分に対して正面にオブジェクトを生成
        double RadianY = Math.PI * cameraRig_tf.eulerAngles.y / 180.0;
        float LengthX = (float)(Length * Math.Sin(RadianY));
        float LengthZ = (float)(Length * Math.Cos(RadianY));
        var position = new Vector3(cameraRig_tf.position.x + LengthX, cameraRig_tf.position.y + 3, cameraRig_tf.position.z + LengthZ);

        GameObject ObjectClone = PhotonNetwork.Instantiate(objName, position, Quaternion.identity);
        ObjectClone.transform.eulerAngles = new Vector3(0, 0, 0);
        Debug.Log(ObjectClone.transform.rotation);
       ObjectClone.transform.LookAt(cameraRig_tf);
        Debug.Log(ObjectClone.transform.rotation);
        //ObjectClone.transform.eulerAngles = new Vector3(0, ObjectClone.transform.rotation.y, 0);

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        //クリックイベントを登録！！！！！！！！！！！！

    }
}
