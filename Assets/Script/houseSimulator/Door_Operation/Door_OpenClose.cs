using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//ドアの開閉を行うクラス、使う時は必ずアニメーションを外すこと！！
public class Door_OpenClose : MonoBehaviour
{
    
    public Transform door; // ドアのTransform
    public float openAngle = 90f; // 開く角度
    public float closeAngle = 0f; // 閉じる角度
    public float speed = 3f; // 開閉速度

    private bool isOpen = false; // ドアが開いているかどうか


    void Update()
    {
        float targetAngle = isOpen ? openAngle : closeAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        door.localRotation = Quaternion.Lerp(door.localRotation, targetRotation, Time.deltaTime * speed);
    }

    public void SetIsOpen(bool inputIsOpen)
    {
        isOpen = inputIsOpen;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }


    public void ToggleDoor()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SwitchIsOpen", RpcTarget.All);

    }

    [PunRPC]
    public void SwitchIsOpen()
    {
        //状態を切り替える
        isOpen = !isOpen;
    }

}
