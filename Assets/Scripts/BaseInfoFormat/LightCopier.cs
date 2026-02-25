using UnityEngine;

//Lightのプロパティを全てコピーするクラス、 static
public static class LightCopier
{
    public static void CopyLightProperties(Light source, Light target)
    {
        if (source == null || target == null)
        {
            Debug.Log("Source or Target Light is null!");
            return;
        }

        // コピーするプロパティ
        target.type = source.type;
        target.color = source.color;
        target.intensity = source.intensity;
        target.range = source.range;
        target.spotAngle = source.spotAngle;
        target.shadows = source.shadows;
        target.shadowStrength = source.shadowStrength;
        target.shadowBias = source.shadowBias;
        target.shadowNormalBias = source.shadowNormalBias;
        target.shadowNearPlane = source.shadowNearPlane;
        target.cookie = source.cookie;
        target.cookieSize = source.cookieSize;
        target.renderMode = source.renderMode;
        target.cullingMask = source.cullingMask;
    }
}