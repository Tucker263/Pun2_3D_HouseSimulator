using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class Light_Intensity_Change : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }


    public void Change()
    {
        string targetName = Lighting_Selected.placeName;
        float sliderValue = slider.value;
        //RPC通信
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReflectIntensity", RpcTarget.All, targetName, sliderValue);
    }


    [PunRPC]
    public void ReflectIntensity(string targetName, float sliderValue)
    {
        GameObject obj = GameObject.Find(targetName);
        if(obj != null)
        {
            Light light = obj.GetComponent<Light>();
            light.intensity = sliderValue;
        }

    }
}
