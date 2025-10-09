using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// ドアの開閉を行うクラス
// アタッチ対象: ドア本体

[RequireComponent(typeof(PhotonView))]
public class DoorOpenClose : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private float _openAngle = 90f; // 開く角度
    [SerializeField] private float _closeAngle = 0f; // 閉じる角度
    [SerializeField] private float _speed = 3f; // 開閉速度

    private bool _isOpen = false; // ドアが開いているかどうか


    private void Awake()
    {
        // 忘れ対策
        if(_doorTransform == null) _doorTransform = transform;

    }


    private void Update()
    {
        float targetAngle = _isOpen ? _openAngle : _closeAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        _doorTransform.localRotation = Quaternion.Lerp(_doorTransform.localRotation, targetRotation, Time.deltaTime * _speed);

    }


    public bool IsOpen
    {
        get => _isOpen;
        set => _isOpen = value;
    }


    public void OnClickToggleDoor()
    {
        _isOpen = !_isOpen;
        // 他クライアントの同期
        photonView.RPC(nameof(RPC_ToggleDoor), RpcTarget.Others, _isOpen);
        
    }


    [PunRPC]
    public void RPC_ToggleDoor(bool isOpen)
    {
        _isOpen = isOpen;
    }
    
}
