using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Sunの日没を判断を行うクラス
// アタッチ対象: Sunオブジェクト
public class SunSunsetChecker: MonoBehaviour
{

    [SerializeField] private float _angleMin = -5f;
    [SerializeField] private float _angleMax = 185f;
    private bool _isSunset;

    [SerializeField] private float _checkInterval = 0.1f;
    private float _lastCheckTime = 0f;

    private void Update()
    {
        // 0.1秒ごとに日没かを計算、負荷軽減
        if (Time.time - _lastCheckTime >= _checkInterval)
        {
            float xAngle = transform.eulerAngles.x;
            _isSunset = xAngle < _angleMin || xAngle > _angleMax;
            _lastCheckTime = Time.time;
        
        }
    }


    public bool IsSunset()
    {
        return _isSunset;
    }

}