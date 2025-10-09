using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 照明の強さを変更するクラス
// アタッチ対象: UIのスライダー
public class LightIntensityChanger : MonoBehaviour
{
    private Slider _slider;
    private LightIntensitySynchronizer _lightIntensitySynchronizer;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null) Debug.LogError($"{name} :Slider が見つかりません");

    }


    private void Start()
    {
        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _lightIntensitySynchronizer = uiOperationSynchronizer.GetComponentInChildren<LightIntensitySynchronizer>();
    }


    public void OnSliderChangeIntensity()
    {
        float intensity = _slider.value;

        // 他のクライアントに同期
        _lightIntensitySynchronizer.SynchronizeOthers(intensity);

        // ローカル反映
        _lightIntensitySynchronizer.SynchronizeLocal(intensity);

    }

}
