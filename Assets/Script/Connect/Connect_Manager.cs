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

    //自分が退出した時の処理
    public override void OnLeftRoom()
    {
        Debug.Log("部屋から退出しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
        
    }


    //他の人が退出した時に、avator(ネットワークオブジェクト)を破棄
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        List<GameObject> objList = NetworkObject_Search.GetListFromTag("avator");
        //avatorの配列から退出するプレイヤーのavatorを削除
        foreach (GameObject obj in objList)
        {
            Avator_Mesh a_m = obj.GetComponent<Avator_Mesh>();
            //他の人が退出すると、所有権がマスタークライアントに移るため、マスタークライアントが削除できる
            //自分がマスタークライアントで、マスタークライアント以外の生産者だったら、プレイヤーオブジェクトを削除
            if (PhotonNetwork.IsMasterClient && a_m.GetCreatorID() != 1)
            {
                PhotonNetwork.Destroy(obj);
            }

        }

    }


    //他のプレイヤーがルームに入室した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //自分がマスタークライアントの時、全クライアントに向けて最新の状態を同期する処理を行う
        //この処理がないと、今の状況を途中参加者に反映できない
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("他プレイヤーが参加しました。最新の状況を反映させます");
            GameObject obj = NetworkObject_Search.GetObjectFromName("CurrentState_Synchronize");
            CurrentState_Synchronize c_s = obj.GetComponent<CurrentState_Synchronize>();
            c_s.Synchronize();
            Debug.Log("他プレイヤーの同期完了");
        }

    }

}