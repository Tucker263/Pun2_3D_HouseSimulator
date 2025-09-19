using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//照明の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class LightingInfo
{
    public string placeName; //照明の場所の名前,ここの名前が被るとうまくロードができなくなるので注意
    public bool enabled; //照明のオンオフ
    public float intensity; //光の明るさ
    public string lightKind;//照明の種類、常夜灯、白熱灯、LEDなど
}


public static class LightingInfo_Format
{

    public static LightingInfo Convert(GameObject obj)
    {
        LightingInfo info = new LightingInfo();
        info.placeName = obj.name;

        Light light = obj.GetComponent<Light>();
        info.enabled = light.enabled;
        info.intensity = light.intensity;

        TMP_Text objTMP = obj.GetComponent<TMP_Text>();
        info.lightKind = objTMP.text;

        return info;
    }


    public static void ApplyObject(GameObject obj, LightingInfo info)
    {
        Light targetLight = obj.GetComponent<Light>();

        //Resourcesフォルダ内のコピー元のオブジェクトをロード
        GameObject sourceObj = Resources.Load<GameObject>("Lights/" + info.lightKind);
        //コピー元のlight
        Light sourceLight = sourceObj.GetComponent<Light>();
        //Lightのプロパティーを全てコピー
        LightCopier.CopyLightProperties(sourceLight, targetLight);

        //lightのその他の情報を書き換え
        targetLight.name = info.placeName;
        targetLight.enabled = info.enabled;
        targetLight.intensity = info.intensity;

        TMP_Text targetTMP = obj.GetComponent<TMP_Text>();
        targetTMP.text = info.lightKind;
    }

}
