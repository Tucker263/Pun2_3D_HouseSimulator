using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 生成されたオブジェクトのオーナーシップを管理するクラス
// アタッチ対象: SpawnedObject(家具、など)
public class SpawnedObjectOwnership : MonoBehaviourPunCallbacks
{
    private Vector3 _prevPosition;

    private void Awake()
    {
        _prevPosition = transform.position;

    }


    private void Update()
    {
        // オブジェクトが動いた瞬間に所有権を変更
        // オブジェクト同士が衝突した時も同期が可能となる
        bool isMove = IsMove();
        if(isMove)
        {
            // リクエストを送り合う
            photonView.RequestOwnership();
        }


    }

    
    private bool IsMove()
    {
        // 0で割れないため
        if(Mathf.Approximately(Time.deltaTime, 0))
            return false;

        // 現在の位置を取得
        var position = transform.position;

        // 現在の速度を計算
        var velocity = (position - _prevPosition) / Time.deltaTime;

        // 前フレームの位置を更新
        _prevPosition = position;

        // 3次元の速度が0であるかを判定、微小な揺れは無視
        return velocity.sqrMagnitude > 0.001f;

    }


    public void RequestOwnership()
    {
        photonView.RequestOwnership();
    }

}
