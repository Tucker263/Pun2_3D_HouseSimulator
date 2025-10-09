using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 周辺環境をリアルタイム(太陽の位置)と連動させるクラス
// アタッチ対象: RealTimeEnvironmentManager

[RequireComponent(typeof(RealTimeStreetLighting))]
public class RealTimeEnvironmentManager : MonoBehaviourPunCallbacks
{
    private IRealTimeUpdatable[] _updatables;

    // 研究メモ
    // 現地に行かないとわからない情報を反映させたい
    // 将来的には、交通量、人通り、生活音、天候、海風などをリアルタイムで連動させる
    // 実現には衛星データやIOT、車のナビなどを活用する方法を検討中

    // 現在は,街灯の連動処理が可能

    private void Awake()
    {
        // 全てのリアルタイム制御クラスを取得
        _updatables = GetComponents<IRealTimeUpdatable>();

    }


    private void Update()
    {
        // ルームに入っていない場合、処理を中断
        if(!PhotonNetwork.InRoom) return;

        // リアルタイム連動
        foreach (var updatable in _updatables)
        {
            updatable.LinkedRealTime();
        }

    }

}
