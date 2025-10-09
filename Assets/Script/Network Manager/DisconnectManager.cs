using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// 通信の終了を行うクラス
// アタッチ対象: Network Manager
public class DisconnectManager : MonoBehaviourPunCallbacks
{ 

    public void DisconnectProcess()
    {
        if (PhotonNetwork.IsMasterClient)
        {   
            Debug.Log("マスタークライアント: データのセーブ処理開始");
            SaveLoadManager.SaveAll();
            Debug.Log("マスタークライアント: データのセーブ処理完了");

            // 自分以外のクライアント接続をすべて切断
            KickAllOtherClients();

            // 新規プレイヤー同期用のキャッシュを消去
            FixedBaseInfoInitCache.ClearCache();
        }

        if (Config.IsOfflineMode)
        {
            PhotonNetwork.Disconnect();
            return;
        }

        PhotonNetwork.LeaveRoom();
    }


    private void KickAllOtherClients()
    {
        Debug.Log("マスタークライアント: 他クライアントのキック処理開始");

        foreach (Player otherPlayer in PhotonNetwork.PlayerListOthers)
        {
            PhotonNetwork.CloseConnection(otherPlayer);
            Debug.Log($"{otherPlayer.NickName} をキックしました");
        }

        Debug.Log("マスタークライアント: 他クライアントのキック処理完了");

    }


    public override void OnLeftRoom()
    {
        // 検索キャッシュを削除
        PhotonObjectFinder.ClearCache();
        DescendantFinder.ClearCache();

        // Resource内の情報のキャッシュを削除
        SpawnedObjectDataCache.ClearCache();
        LightingCache.ClearCache();
        MaterialCache.ClearCache();

        Debug.Log("ルームから退出しました。");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleScene に戻ります。");
        SceneManager.LoadScene("TitleScene");
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        DestroyLeftAvatar();
    }


    private void DestroyLeftAvatar()
    {
        List<GameObject> avatars = PhotonObjectFinder.FindObjectsWithTag(Tags.Get(TagID.Avatar));
        foreach (GameObject avatar in avatars)
        {
            var avatarMesh = avatar.GetComponent<AvatarMesh>();
            bool canDestroy = PhotonNetwork.IsMasterClient && avatarMesh.CreatorId != 1;
            if (canDestroy)
            {
                PhotonNetwork.Destroy(avatar);
            }
        }
    }

}
