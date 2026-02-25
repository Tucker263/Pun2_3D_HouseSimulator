using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 街灯をリアルタイム(太陽の位置)と連動させるクラス
public class RealTimeStreetLighting : MonoBehaviour, IRealTimeUpdatable
{
    private SunSunsetChecker _sunSunsetChecker;
    private List<Light> _lightList;


    public void Initialize()
    {
        if(_sunSunsetChecker != null) return;

        GameObject sun = PhotonObjectFinder.FindWithTag(Tags.Get(TagID.Sun));
        if (sun == null){
            Debug.LogWarning($"{name} :{Tags.Get(TagID.Sun)}が見つかりません");
            return;
        }
        _sunSunsetChecker = sun.GetComponent<SunSunsetChecker>();
        if(_sunSunsetChecker == null)
        {
            Debug.LogWarning($"{name} :SunSunsetCheckerが見つかりません");
            return;
        }

        _lightList = new List<Light>();
        var objList = PhotonObjectFinder.FindObjectsWithTag(Tags.Get(TagID.StreetLighting));
        foreach(GameObject obj in objList)
        {
            var light = obj.GetComponent<Light>();
            if (light != null) _lightList.Add(light);
        }

        if (_lightList.Count == 0) Debug.LogWarning($"{name} :{Tags.Get(TagID.StreetLighting)}が見つかりませんでした。");

    }
    

    public void LinkedRealTime()
    {
        if(_sunSunsetChecker == null) Initialize();
        if(_sunSunsetChecker == null || _lightList.Count == 0) return;

        // 日中と日没の街灯の切り替え
        bool shouldBeOn = _sunSunsetChecker.IsSunset();
        foreach (var light in _lightList)
        {
            if (light.enabled != shouldBeOn) 
            {
                light.enabled = shouldBeOn;
            }

        }

    }

}
