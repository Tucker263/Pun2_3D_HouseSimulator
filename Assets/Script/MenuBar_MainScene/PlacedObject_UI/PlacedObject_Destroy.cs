using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class PlacedObject_Destroy : MonoBehaviourPunCallbacks
{

    public void Destroy()
    {
        GameObject obj = SelectedObject.obj;
        PlacedObject_Ownership p_o = obj.GetComponent<PlacedObject_Ownership>();
        p_o.Change();
        PhotonNetwork.Destroy(obj);
        SelectedObject.obj = null;

    }

}