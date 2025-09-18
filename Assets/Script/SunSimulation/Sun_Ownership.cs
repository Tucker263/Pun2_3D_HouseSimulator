using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Sun_Ownership : MonoBehaviourPunCallbacks
{
    public void Change()
    {
        photonView.RequestOwnership();
    }
}