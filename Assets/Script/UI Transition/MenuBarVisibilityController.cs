using Photon.Pun;
using UnityEngine;


// 自身が所有している UI オブジェクトのみをアクティブにするクラス
// アタッチ対象: UIのメニューバーのCanvas本体

[RequireComponent(typeof(PhotonView))]
public class MenuBarVisibilityController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // 自分のUIは表示、他の人のUIは非表示に設定
        gameObject.SetActive(photonView.IsMine);
    }
}
