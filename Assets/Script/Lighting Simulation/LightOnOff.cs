using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 照明のオンオフを変更するクラス
// アタッチ対象: 家の照明
public class LightOnOff : MonoBehaviourPunCallbacks
{
    
    public void ToggleLight()
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // 自分のローカルに反映
        RPC_ToggleLight(targetView.ViewID);

        // 他クライアントの同期
        photonView.RPC(nameof(RPC_ToggleLight), RpcTarget.Others, targetView.ViewID);

    }


    [PunRPC]
    private void RPC_ToggleLight(int viewID)
    {
        // lightを取得
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if (obj == null) return;
        Light light = obj.GetComponent<Light>();
        if (light == null) return;

        // 照明のオンオフの切り替え
        light.enabled = !light.enabled;

    }

}
