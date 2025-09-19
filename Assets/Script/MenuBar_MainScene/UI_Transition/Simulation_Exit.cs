using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Simulation_Exit : MonoBehaviourPunCallbacks
{

    public void Exit()
    {
        //マスタークライアントのみ
        if (PhotonNetwork.IsMasterClient)
        {
            //データのセーブ処理
            Debug.Log("マスタークライアントのみ、データのセーブ処理開始");
            Save_Manager.Save();
            Debug.Log("データのセーブ処理完了");
            //プレイヤー全員をキック処理
            KickOtherAllClients();
        }
        if(Config.isOfflineMode)
        {
            PhotonNetwork.Disconnect();
            return;
        }
        PhotonNetwork.LeaveRoom();
    }


    private void KickOtherAllClients()
    {
        Debug.Log("マスタークライアントのみ、他クライアントのキック処理開始");
        //自分以外のプレイヤーオブジェクトを取得し、キック処理
        var otherPlayers = PhotonNetwork.PlayerListOthers;
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            PhotonNetwork.CloseConnection(otherPlayers[i]);
            Debug.Log(otherPlayers[i] + "をキックしました");
        }
        Debug.Log("他クライアントのキック処理完了");
    }
    
}
