using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 窓の開閉を行うクラス
// 使う時は必ずアニメーションを外すこと！！
// アタッチ対象: 窓本体

[RequireComponent(typeof(PhotonView))]
public class WindowSliding : MonoBehaviourPunCallbacks
{   
    [SerializeField] private Vector3 _slideDirection = new Vector3(1, 0, 0); // スライドする方向
    [SerializeField] private float _slideDistance = 2.0f; // スライドする距離
    [SerializeField] private float _slideSpeed = 2.0f; // スライド速度

    private Vector3 _initialLocalPosition; // 初期ローカル位置
    private Vector3 _slideLocalPosition; // スライドのローカル位置
    private bool _isSliding = false; // スライド中かどうか


    private void Start()
    {
        if(_initialLocalPosition == Vector3.zero)
        {
            Initialize();
        }

    }


    private void Update()
    {
        Vector3 targetPosition = _isSliding ? _slideLocalPosition : _initialLocalPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, _slideSpeed * Time.deltaTime);

    }

    public void Initialize()
    {
        _initialLocalPosition = transform.localPosition;
        _slideLocalPosition = _initialLocalPosition + _slideDirection.normalized * _slideDistance; //目標位置の計算

    }


    public bool IsSliding
    {
        get => _isSliding;
        set => _isSliding = value;
    }


    public void ToggleWindow()
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // 自分のローカルに反映
        RPC_ToggleWindow(targetView.ViewID);

        // 他クライアントの同期
        photonView.RPC(nameof(RPC_ToggleWindow), RpcTarget.Others, targetView.ViewID);

    }


    [PunRPC]
    private void RPC_ToggleWindow(int viewID)
    {
        // WindowSlidingを取得
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if (obj == null) return;
        WindowSliding windowSliding = obj.GetComponent<WindowSliding>();
        if(windowSliding == null) return;

        // 状態を切り替える
        windowSliding.IsSliding = !windowSliding.IsSliding;

    }

}
