using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// 照明の種類を変更するクラス
// アタッチ対象: Light Synchronizer

[RequireComponent(typeof(PhotonView))]
public class LightKindSynchronizer : MonoBehaviourPunCallbacks
{

    public void SynchronizeLocal(string lightKind)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // ローカルに反映
        RPC_ApplyLightKind(targetView.ViewID, lightKind);

    }

    
    public void SynchronizeOthers(string lightKind)
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        // 種類の名前が一致していたら処理を中断、重複送信をなくす
        TMP_Text objTMP = obj.GetComponent<TMP_Text>();
        if (objTMP == null) return;
        if(objTMP.text == lightKind) return;

        PhotonView targetView = obj.GetPhotonView();
        if(targetView == null) return;

        // 他クライアントに同期
        photonView.RPC(nameof(RPC_ApplyLightKind), RpcTarget.Others, targetView.ViewID, lightKind);

    }


    [PunRPC]
    private void RPC_ApplyLightKind(int viewID, string lightKind)
    {
        // コピー先のオブジェクトとLight
        GameObject targetObj = PhotonView.Find(viewID)?.gameObject;
        if (targetObj == null) return;
        Light targetLight = targetObj.GetComponent<Light>();

        // Resourcesフォルダ内のコピー元のオブジェクトをロード
        GameObject sourceObj = LightingCache.GetValue(lightKind);
        if(sourceObj == null)
        {
            Debug.LogWarning($"LightingTable:  {lightKind} が見つかりません");
            return;
        }

        // コピー元のlight
        Light sourceLight = sourceObj.GetComponent<Light>();

        // Lightのプロパティーを明るさ以外を全てコピー
        float lightIntensity = targetLight.intensity;
        LightCopier.CopyLightProperties(sourceLight, targetLight);
        targetLight.intensity = lightIntensity;

        // targetObjのtextの名前も変更
        TMP_Text targetTMP = targetObj.GetComponent<TMP_Text>();
        targetTMP.text = lightKind;

    }
    
}
