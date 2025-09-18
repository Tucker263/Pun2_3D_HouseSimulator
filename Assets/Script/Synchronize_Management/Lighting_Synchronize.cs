using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

//照明の同期を行うクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Lighting_Synchronize : MonoBehaviourPunCallbacks
{

    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);
        List<GameObject> lightingList = NetworkObject_Search.GetListFromTag("lighting");
        foreach(GameObject obj in lightingList)
        {
            Light light = obj.GetComponent<Light>();
            TMP_Text tmp = obj.GetComponent<TMP_Text>();
            //照明の情報を取得
            LightingInfo lighting = new LightingInfo();
            lighting.name = obj.name;
            lighting.enabled = light.enabled;
            lighting.intensity = light.intensity;
            lighting.lightKind = tmp.text;
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(lighting);
            photonView.RPC("ReflectLighting", RpcTarget.Others, jsonData);

        }

    }

    [PunRPC]
    public void ReflectLighting(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
        //lightingタグと名前の両方が一致しているオブジェクトを探す、名前が被るとうまく反映できなくなるので注意
        GameObject targetObj = NetworkObject_Search.GetObjectFromTagAndName("lighting", lighting.name);
        Light targetLight = targetObj.GetComponent<Light>();
        TMP_Text targetTMP = targetObj.GetComponent<TMP_Text>();

        //Resourcesフォルダ内のコピー元のオブジェクトとlightをロード
        GameObject sourceObj = Resources.Load<GameObject>("Lights/"+ lighting.lightKind);
        Light sourceLight = sourceObj.GetComponent<Light>();

        //照明を反映
        LightCopier.CopyLightProperties(sourceLight, targetLight);
        targetLight.name = lighting.name;
        targetLight.enabled = lighting.enabled;
        targetLight.intensity = lighting.intensity;
        targetTMP.text = lighting.lightKind;
    }
    
}