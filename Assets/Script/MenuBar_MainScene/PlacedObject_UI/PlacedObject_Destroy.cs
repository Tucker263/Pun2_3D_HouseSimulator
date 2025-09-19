using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class PlacedObject_Destroy : MonoBehaviour
{

    public void Destroy()
    {
        GameObject obj = SelectedObject.obj;

        if(obj != null)
        {
            //所有権の変更
            PlacedObject_Ownership p_o = obj.GetComponent<PlacedObject_Ownership>();
            p_o.Change();

            //オブジェクトの破棄
            PhotonNetwork.Destroy(obj);
            SelectedObject.obj = null;
        }

        //メニューバーの遷移
        MenuBar_Transition m_t = GetComponent<MenuBar_Transition>();
        m_t.Transition();

    }

}