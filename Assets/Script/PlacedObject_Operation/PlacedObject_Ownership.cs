using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


//オブジェクトが動かされた瞬間、つまり速度が変わった時にオーナーシップが変わるようにする
//これがないと、オブジェクトが衝突した時などに所有権の関係で位置を同期できない
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class PlacedObject_Ownership : MonoBehaviourPunCallbacks
{
    private Vector3 prevPosition;

    void Start()
    {
        prevPosition = transform.position;

    }


    void Update()
    {
        bool isMove = IsMove();
        if(isMove)
        {
            //リクエストを送り合って、位置を同期させる
            photonView.RequestOwnership();
        }


    }

    
    private bool IsMove()
    {
        //0で割れないため
        if(Mathf.Approximately(Time.deltaTime, 0))
            return false;

        //現在の位置を取得
        var position = transform.position;

        //現在の速度を計算
        var velocity = (position - prevPosition) / Time.deltaTime;

        //前フレームの位置を更新
        prevPosition = position;

        //3次元の速度が0であるかを判定
        return velocity != new Vector3(0, 0, 0);

    }

}