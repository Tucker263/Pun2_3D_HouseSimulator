using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Connect_Starting : MonoBehaviourPunCallbacks
{
    private GameObject directionalLight;

    private GameObject menuObj;
    private GameObject ovrObj;

    private GameObject CameraRig;
    private Transform CameraRig_tf;


    public void Connect()
    {
        SetProperty();

        //どのクライアントも、キックされる処理ができるように設定
        PhotonNetwork.EnableCloseConnection = true;
        //オフラインモードかオンラインモードか、どちらか設定
        PhotonNetwork.OfflineMode = Config.isOfflineMode;
        //オンラインモードの場合
        if (!PhotonNetwork.OfflineMode)
        {
            // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
            Debug.Log("オンラインモード");
            Debug.Log("マスターサーバーへの接続開始");
            PhotonNetwork.ConnectUsingSettings();
            
        }
        else
        {
            Debug.Log("オフラインモード");
            //元からあるライトを非アクティブ化
            directionalLight.SetActive(false);
        }
    }


    private void SetProperty()
    {
        menuObj = GameObject.Find("MenuBar");
        ovrObj = GameObject.Find("OVRInput_Manager");
        directionalLight = GameObject.Find("Directional_Light");

        CameraRig = GameObject.FindWithTag("camera_rig");
        CameraRig_tf = CameraRig.GetComponent<Transform>();

        //メニューバーを非アクティブ化
        menuObj.SetActive(false);
        //OVRInput_Managerを非アクティブ化、何も操作ができないようにする
        ovrObj.SetActive(false);
        //元からあるライトをアクティブ化
        directionalLight.SetActive(true);

    }


    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーの接続成功");
        //参加可能人数を3人に設定
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        //プレイヤーが退出しても、ネットワークオブジェクトを消えないように設定
        roomOptions.CleanupCacheOnLeave = false;

        // roomNameというルームに参加する（ルームが存在しなければ作成して参加する）
        string roomName = Config.roomName;
        Debug.Log(roomName + "への接続開始");
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    //ルームが作成された時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成成功");
    }

    //ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへの参加成功");
        //ニックネームを、ホスト、ゲストに分ける
        PhotonNetwork.NickName = PhotonNetwork.IsMasterClient ? "Host" : "Guest";
        //VR用カメラを初期位置に設置
        var position = new Vector3(Random.Range(-8, 8), 2, Random.Range(-17, -10));
        CameraRig_tf.position = position;


        //マスタークライアントのみ、データのロード処理
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("マスタークライアントのみ");
            Debug.Log("データのロード処理開始");
            Environment_Creator.Create();
            Load_Manager.Load();
            EventTrigger_Register.register();
            Debug.Log("データのロード処理完了");

        }
        else
        {
            //ゲストは、アバターを生成
            Avator_Generator.Generate();
        }

        //メニューバーを全て表示して、photonViewIDを一旦全部割り当てる
        menuObj.SetActive(true);
        foreach(var value in MenuBar_Dictionary.UI_table.Values)
        {
            value.SetActive(true);
        }

        //デフォルトのメニューバー以外を全て非表示にする
        foreach(var value in MenuBar_Dictionary.UI_table.Values)
        {
            if(MenuBar_Dictionary.UI_table["Default_UI"] != value)
            {
                value.SetActive(false);
            }
        }

        //OVRInput_Managerをアクティブ化
        ovrObj.SetActive(true);


        //元からあるライトを非アクティブ化
        directionalLight.SetActive(false);

    }

    //ルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("ルームへの参加が失敗しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
    }

}