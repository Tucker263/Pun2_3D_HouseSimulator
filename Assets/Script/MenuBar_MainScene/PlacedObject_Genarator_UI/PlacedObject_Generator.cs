using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
//using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class PlacedObject_Generator : MonoBehaviour
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
        //自分の正面にオブジェクトの生成を指定
        var position = DesignateFront();
        //オブジェクトの生成
        GameObject obj = PhotonNetwork.Instantiate(objName, position, Quaternion.identity); 
        //オブジェクトをy軸だけ自分方向に向ける
        LookAtCamera(obj);

        //クリックイベントを登録
        SelectedObject_EventTrigger_Register.registerFromObj(obj);

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }


    private Vector3 DesignateFront()
    {
        double RadianY = Math.PI * cameraRig_tf.eulerAngles.y / 180.0;
        float LengthX = (float)(Length * Math.Sin(RadianY));
        float LengthZ = (float)(Length * Math.Cos(RadianY));
        var position = new Vector3(cameraRig_tf.position.x + LengthX, cameraRig_tf.position.y + 3, cameraRig_tf.position.z + LengthZ);

        return position;

    }


    private void LookAtCamera(GameObject obj)
    {
        Vector3 direction = cameraRig_tf.position - obj.transform.position;
        //Y軸だけを考慮
        direction.y = 0;

        if(direction != Vector3.zero)
        {
            //回転させるQuaternionを計算
            Quaternion rotation = Quaternion.LookRotation(direction);
            obj.transform.rotation = rotation;
        }

    }
}
