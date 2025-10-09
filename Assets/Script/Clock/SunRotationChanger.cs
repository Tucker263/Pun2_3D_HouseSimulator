using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 太陽を回転させるクラス
// アタッチ対象: UIのスライダー
public class SunRotationChanger : MonoBehaviour
{
    private Slider _slider;
    private SunRotationSynchronizer _sunRotationSynchronizer;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _sunRotationSynchronizer = uiOperationSynchronizer.GetComponentInChildren<SunRotationSynchronizer>();
    }


    public void OnSliderRotationChanged()
    {
        // 太陽の回転の同期
        float xAngle = _slider.value;
        _sunRotationSynchronizer.Synchronize(xAngle);

    }

}
