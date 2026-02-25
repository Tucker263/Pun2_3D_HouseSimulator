using Photon.Pun;
using TMPro;
using UnityEngine;


// アバター名をラベリングするクラス
// アタッチ対象: Avatorオブジェクト
public class AvatarNameLabel : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // TextMeshPro コンポーネントを取得
        var nameLabel = GetComponent<TextMeshPro>();
        if (nameLabel == null)
        {
            Debug.LogError($"{gameObject.name}: TextMeshPro が見つかりません");
            return;
        }

        // プレイヤー名を設定
        string displayName = photonView.Owner.NickName;
        if (displayName == "Guest")
        {
            displayName += $"({photonView.OwnerActorNr - 1})";
        }
        nameLabel.text = displayName;

        // 見た目の設定
        nameLabel.color = Color.black;
        nameLabel.fontSize = 50;
        nameLabel.alignment = TextAlignmentOptions.Center;

        // 角度の設定
        nameLabel.transform.localRotation = Quaternion.Euler(30f, 0f, 0f);

    }
    
}
