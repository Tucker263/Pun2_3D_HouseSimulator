using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Light_Kind_Change : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI buttonTMP;
    private string lightKind;


    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        lightKind = buttonTMP.text;
        lightKind = lightKind.Replace(" ", "");

    }


    public void Change()
    {
        string targetName = Lighting_Selected.placeName;
        if(targetName != null)
        {
            //RPC通信で照明の種類を変える
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ReflectLightKind", RpcTarget.All, targetName, lightKind);
        }

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    public void ReflectLightKind(string targetName, string lightKind)
    {
        //Resourcesフォルダ内のコピー元のオブジェクトをロード
        GameObject sourceObj = Resources.Load<GameObject>("Lights/"+ lightKind);
        //コピー元のlight
        Light sourceLight = sourceObj.GetComponent<Light>();

        //コピー先のオブジェクトとLight
        GameObject targetObj = GameObject.Find(targetName);
        Light targetLight = targetObj.GetComponent<Light>();

        //Lightのプロパティーを明るさ以外を全てコピー
        float lightIntensity = targetLight.intensity;
        LightCopier.CopyLightProperties(sourceLight, targetLight);
        targetLight.intensity = lightIntensity;

        //targetObjのtextの名前も変更
        TMP_Text targetTMP = targetObj.GetComponent<TMP_Text>();
        targetTMP.text = lightKind;

    }

}
