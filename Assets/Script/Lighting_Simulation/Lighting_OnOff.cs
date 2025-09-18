using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Lighting_OnOff : MonoBehaviour
{
    public void OnOffLight()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SwitchEnabled", RpcTarget.All);

    }

    [PunRPC]
    public void SwitchEnabled()
    {
        Light light = GetComponent<Light>();
        light.enabled = !light.enabled;
    }
}
