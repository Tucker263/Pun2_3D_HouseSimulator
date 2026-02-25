using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// Sunの所有権を変更するクラス
// アタッチ対象: Sunオブジェクト
public class SunOwnership : MonoBehaviourPunCallbacks
{
    public bool IsMine => photonView.IsMine;

    public void RequestOwnership()
    {
        photonView.RequestOwnership();
    }

}