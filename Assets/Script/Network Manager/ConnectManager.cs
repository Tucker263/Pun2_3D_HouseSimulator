using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// 通信の開始を行うクラス
// アタッチ対象: Network Manager

[RequireComponent(typeof(SceneInitializer))]
public class ConnectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int _maxPlayers = 5; // 参加可能な人数
    private SceneInitializer _sceneInitializer;


    private void Awake()
    {
        _sceneInitializer = GetComponent<SceneInitializer>();

        // Resourcesファルダ内のキャッシュ情報を初期化(ライティング、マテリアル)
        LightingCache.Initialize();
        MaterialCache.Initialize();
    }


    private void Start()
    {
        _sceneInitializer.Initialize();

        if (Config.IsOfflineMode)
        {
            Debug.Log("オフラインモード");
            PhotonNetwork.OfflineMode = true;
            _sceneInitializer.SetOfflineMode();
            return;
        }

        Debug.Log("オンラインモード：マスターサーバーへの接続開始");
        PhotonNetwork.ConnectUsingSettings();

    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーへの接続成功");

        // 参加可能な人数などを設定
        var roomOptions = new RoomOptions
        {
            MaxPlayers = _maxPlayers,
            CleanupCacheOnLeave = false
        };

        PhotonNetwork.EnableCloseConnection = true;

        string roomName = Config.RoomName;
        Debug.Log($"{roomName} ルームへの参加または作成を開始");
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);

    }


    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功");
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへの参加に成功");

        PhotonNetwork.NickName = PhotonNetwork.IsMasterClient ? "Host" : "Guest";

        _sceneInitializer.SetCameraRigStartPosition();

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("マスタークライアント: データのロードを開始");
            EnvironmentGenerator.Generate();
            // 新規プレイヤー同期用のBaseInfoキャッシュを初期化
            FixedBaseInfoInitCache.Initialize();

            SaveLoadManager.LoadAll();
            EventTriggerRegister.Register();
            //RayInteractableRegister.Register();
            Debug.Log("マスタークライアント： データのロードが完了");
        }
        else
        {
            AvatarFactory.Create();
        }

        _sceneInitializer.SetUpMenuBar();
        _sceneInitializer.SetOnlineMode();

    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("ルームへの参加に失敗しました。");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleScene に戻ります。");
        SceneManager.LoadScene("TitleScene");

    }

}
