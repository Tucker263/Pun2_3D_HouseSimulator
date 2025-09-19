using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class DisConnect : MonoBehaviourPunCallbacks
{
    public void Update()
    {
        PhotonNetwork.Disconnect();

    }

}