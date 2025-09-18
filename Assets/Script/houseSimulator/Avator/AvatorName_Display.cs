using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class AvatorName_Display : MonoBehaviourPunCallbacks
{

    void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // プレイヤー名の表示
        nameLabel.text = $"{photonView.Owner.NickName}";
        if(photonView.Owner.NickName == "Guest")
        {
            nameLabel.text += $"({photonView.OwnerActorNr - 1})";
        }

        nameLabel.color = Color.black;
        nameLabel.fontSize = 50;
        nameLabel.transform.Rotate(30, 0, 0);
    }

}
