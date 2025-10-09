using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LightingDestroy__Camera : MonoBehaviour
{
    void Update()
    {
        //lighting_OnOff関数を使うと、なぜか「__Camera」が生成されるため、常に破棄する処理を行う
        //Intensity_SliderのSliderの下に「__Camera」が生成される
        //Meta Questの仕様？？？？？？
        GameObject obj = GameObject.Find("__Camera");
        if (obj != null)
        {
            Destroy(obj);
        }
        
    }

}