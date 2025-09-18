using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class MovedObject_Generator : MonoBehaviourPunCallbacks
{
    private string objName;

    private GameObject vrCamera;
    private Transform vrCamera_tf;
    
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        objName = buttonTMP.text;

        vrCamera = NetworkObject_Search.GetObjectFromTag("vr_camera");
        vrCamera_tf = vrCamera.GetComponent<Transform>();
    }


    public void Generate()
    {
        //VR用カメラの目の前に生成
        var position = new Vector3(vrCamera_tf.position.x, 2, vrCamera_tf.position.z + 3);
        Quaternion rotation = Quaternion.Euler(0, 90, 0);

        PhotonNetwork.Instantiate(objName, position, rotation);

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }
}
