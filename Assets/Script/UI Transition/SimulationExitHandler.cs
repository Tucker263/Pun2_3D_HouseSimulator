using System;
using UnityEngine;


// シミュレーションの終了処理を行うクラス
// アタッチ対象: UIのボタン
public class SimulationExitHandler : MonoBehaviour
{
    private const string _networkManagerName = "Network_Manager";
    private DisconnectManager _disconnectManager;

    private void Start()
    {
        GameObject networkManager = GameObject.Find(_networkManagerName);
        if(networkManager == null) Debug.LogError($"{_networkManagerName}オブジェクトが見つかりませんでした");

        _disconnectManager = networkManager.GetComponent<DisconnectManager>();
        if(_disconnectManager== null) Debug.LogError($"{name} : DisconnectManagerコンポーネントが見つかりませんでした");

    }
    
    public void OnClickExitSimulation()
    {
        // 通信の終了
        _disconnectManager.DisconnectProcess();

    }

}
