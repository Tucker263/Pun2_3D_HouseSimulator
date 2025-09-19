using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Connect_Manager : MonoBehaviourPunCallbacks
{
    
    public void Start()
    {
        //接続開始
        Connect_Starting c_s = GetComponent<Connect_Starting>();
        c_s.Connect();
        
    }

    //自分が部屋から退出した時の処理
    public override void OnLeftRoom()
    {
        Debug.Log("部屋から退出しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
        
    }


    //他のプレイヤーが退出した時の処理
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        //退出したプレイヤーのavatorを破棄する処理
        DestroyLeftAvatar();

    }


    private void DestroyLeftAvatar()
    {
        List<GameObject> objList = NetworkObject_Search.GetListFromTag("avator");
        foreach (GameObject obj in objList)
        {
            Avator_Mesh a_m = obj.GetComponent<Avator_Mesh>();
            //他のプレイヤーが退出すると所有権が移る
            bool canDestoryAvator = PhotonNetwork.IsMasterClient && a_m.GetCreatorID() != 1;
            if (canDestoryAvator)
            {
                PhotonNetwork.Destroy(obj);
            }

        }

    }


    //他のプレイヤーがルームに参加したときの処理
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //他のプレイヤーの同期処理
            SyncStateForNewPlayer();
        }

    }


    private void SyncStateForNewPlayer()
    {
        Debug.Log("他プレイヤーが参加しました。最新の状況を反映させます");
        GameObject obj = NetworkObject_Search.GetObjectFromName("CurrentState_Synchronize");
        CurrentState_Synchronize c_s = obj.GetComponent<CurrentState_Synchronize>();
        c_s.Synchronize();
        Debug.Log("他プレイヤーの同期完了");

    }

}