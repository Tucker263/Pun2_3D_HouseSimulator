using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class MenuBar_IsActive : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        GameObject obj = this.gameObject;
        // 自身のUIオブジェクトの時はアクティブ化
        bool isActive = photonView.IsMine ? true : false;
        obj.SetActive(isActive);

    }

}
