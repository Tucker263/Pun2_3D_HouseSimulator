using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


// 照明の強さを変更するクラス
// アタッチ対象: Light Synchronizer

[RequireComponent(typeof(PhotonView))]
public class LightIntensitySynchronizer : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _changeThreshold = 0.01f; // 変化がこの値以上でRPC送信
    [SerializeField] private float _sendInterval = 0.05f;    // 最低送信間隔（秒）
    private float _lastSentValue;
    private float _lastSendTime;


    public void SynchronizeLocal(float intensity)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // ローカル反映
        RPC_ApplyIntensity(targetView.ViewID, intensity);

    }


    public void SynchronizeOthers(float intensity)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;
        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;


        float currentValue = intensity;
        float delta = Mathf.Abs(currentValue - _lastSentValue);
        float timeSinceLastSend = Time.time - _lastSendTime;
        // 変化量が閾値以上か、送信間隔が経過している場合のみRPC送信
        if (delta >= _changeThreshold && timeSinceLastSend >= _sendInterval)
        {
            // 他クライアントの同期
            photonView.RPC(nameof(RPC_ApplyIntensity), RpcTarget.Others, targetView.ViewID,  intensity);

            _lastSentValue = currentValue;
            _lastSendTime = Time.time;
        }

    }


    [PunRPC]
    private void RPC_ApplyIntensity(int viewID, float intensity)
    {
        // Lightの取得
        GameObject obj = PhotonView.Find(viewID)?.gameObject;
        if(obj == null) return;
        Light light = obj.GetComponent<Light>();
        if(light == null) return;

        // Lightの強さの変更
        light.intensity = intensity;

    }

}
