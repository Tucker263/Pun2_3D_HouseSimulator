using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SunTime_Change : MonoBehaviourPunCallbacks
{
    private GameObject sun;
    private Slider slider;
    private Transform sun_tf;
    private Sun_Ownership s_o;


    public void SetInitProperty()
    {
        //ネットワークオブジェクトからsunを取得
        sun = NetworkObject_Search.GetObjectFromTag("sun");

        slider = GetComponent<Slider>();
        sun_tf = sun.GetComponent<Transform>();
        s_o = sun.GetComponent<Sun_Ownership>();

    }


    public void Change()
    {
        if(sun == null)
        {
            SetInitProperty();
        }
        //所有権の譲渡がないと、他の人が触っても位置の同期できない
        s_o.Change();
        float rotateX = slider.value;
        sun_tf.eulerAngles = new Vector3(rotateX, 0, 0);
    }
}
