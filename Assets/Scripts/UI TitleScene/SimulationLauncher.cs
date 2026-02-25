using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// シミュレーションの起動を行うクラス
// アタッチ対象: UIのボタン
public class SimulationLauncher : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputFieldRoomName;
    [SerializeField] private TMP_InputField _inputFieldDirectoryName;

    [SerializeField] private Toggle _toggleInitialStart;
    [SerializeField] private Toggle _toggleOfflineMode;


    private void Start()
    {
        // 初期のルーム名を設定
        Config.RoomName = "SampleRoom";
        _inputFieldRoomName.text = Config.RoomName;

        // 初期のセーブデータ名を設定;
        Config.DirectoryName = "SampleDirectory";
        _inputFieldDirectoryName.text = Config.DirectoryName;

        // 初期を「始めから」に設定
        Config.IsInitialStart = true;
        _toggleInitialStart.isOn = true;

        // 初期のモードを設定
        Config.IsOfflineMode = false;
        _toggleOfflineMode.isOn = false;

    }


    public void OnClickLaunchSimulation()
    {
        // inputbuttonからルーム名を取得
        Config.RoomName = _inputFieldRoomName.text;

        // inputbuttonからセーブデータ名を取得
        Config.DirectoryName = _inputFieldDirectoryName.text;

        // 入力が空欄だと中断
        if (Config.RoomName == "" || Config.DirectoryName == "")
        {
            return;
        }

        Debug.Log("入力されたルーム名:" + Config.RoomName);
        Debug.Log("入力されたセーブデータ名:" + Config.DirectoryName);

        // mainsceneの読み込み
        Debug.Log("MainSceneへの移行します");
        SceneManager.LoadScene("MainScene");

    }

}
