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
        List<GameObject> objList = NetworkObject_Search.GetListFromTag("lighting");
        foreach(GameObject obj in objList)
        {
            //LightingInfoに変換
            LightingInfo lighting = LightingInfo_Format.Convert(obj);
            //JSONに変換
            string jsonData = JsonUtility.ToJson(lighting);
            photonView.RPC("ReflectLighting", RpcTarget.Others, jsonData);

        }

    }

    [PunRPC]
    public void ReflectLighting(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        LightingInfo info = JsonUtility.FromJson<LightingInfo>(jsonData);
        //lightingタグと名前の両方が一致しているオブジェクトを探す、名前が被るとうまく反映できなくなるので注意
        GameObject targetObj = NetworkObject_Search.GetObjectFromTagAndName("lighting", info.placeName);


        Light targetLight = targetObj.GetComponent<Light>();
        TMP_Text targetTMP = targetObj.GetComponent<TMP_Text>();

        //Resourcesフォルダ内のコピー元のオブジェクトとlightをロード
        GameObject sourceObj = Resources.Load<GameObject>("Lights/"+ info.lightKind);
        Light sourceLight = sourceObj.GetComponent<Light>();

        //照明を反映
        LightCopier.CopyLightProperties(sourceLight, targetLight);
        targetLight.name = info.placeName;
        targetLight.enabled = info.enabled;
        targetLight.intensity = info.intensity;
        targetTMP.text = info.lightKind;
    }
    
}