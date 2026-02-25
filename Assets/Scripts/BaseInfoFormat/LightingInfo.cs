using UnityEngine;
using TMPro;


// 照明の情報を保持するクラス
[System.Serializable]
public class LightingInfo: BaseInfo
{
    public bool isEnabled;    // 照明のオン/オフ
    public float intensity;   // 明るさ
    public string lightKind;  // 照明の種類（常夜灯、白熱灯、LEDなど）


    // GameObjectから必要な情報を抽出
    public override void ExtractFrom(GameObject obj)
    {
        base.ExtractFrom(obj);

        // ライト
        var light = obj.GetComponent<Light>();
        if (light != null)
        {
            isEnabled = light.enabled;
            intensity = light.intensity;
        }
        else Debug.LogWarning($"{obj.name}: Lightが見つかりませんでした。");

        // 種類
        var _tmpText = obj.GetComponent<TMP_Text>();
        if (_tmpText != null) lightKind = _tmpText.text;
        else Debug.LogWarning($"{obj.name}: TMP_Textが見つかりませんでした。");

    }


    // LightingInfoをGameObject に適用
    public override void ApplyTo(GameObject obj)
    {
        base.ApplyTo(obj);
        
        //照明を取得
        GameObject sourceObj = LightingCache.GetValue(lightKind);
        if(sourceObj == null)
        {
            Debug.LogWarning($"{lightKind}が見つかりませんでした");
            return;
        }

        //照明のプロパティ
        var sourceLight = sourceObj.GetComponent<Light>();
        var targetLight = obj.GetComponent<Light>();
        if (sourceLight != null && targetLight != null)
        {
            LightCopier.CopyLightProperties(sourceLight, targetLight);

            // プロパティ適用
            targetLight.enabled = isEnabled;
            targetLight.intensity = intensity;
        }
        else Debug.LogWarning($"Lightが見つかりませんでした。");

        // ラベルに種類を表示
        var targetTMP = obj.GetComponent<TMP_Text>();
        if (targetTMP != null) targetTMP.text = lightKind;

    }
}
