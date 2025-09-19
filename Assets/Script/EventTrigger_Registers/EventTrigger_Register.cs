using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//イベントトリガーを登録するクラス
public static class EventTrigger_Register
{

    public static void register()
    {
        //ドアのイベントトリガーを登録
        Door_EventTrigger_Register.register();
        //照明のイベントトリガーを登録
        Lighting_EventTrigger_Register.register();

        //家の天井、内壁、床、外壁、屋根のイベントトリガーを登録
        List<string> selectedTag = new List<string>()
        {
            "ceiling", 
            "innerWall", 
            "floor",
            "outerWall",
            "roof",
        };
        foreach(string tag in selectedTag)
        {
            SelectedObject_EventTrigger_Register.register(tag);
        }

    }
    
}
