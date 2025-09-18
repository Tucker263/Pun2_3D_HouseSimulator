using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


//照明のイベントトリガーを登録するクラス
public static class Lighting_EventTrigger_Register
{

    public static void register()
    {
        string registerTag = "lighting";
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(registerTag);
       
        foreach (GameObject obj in objList)
        {
            AddPointerClick(obj);
        }

    }

    //PointerClickイベントを追加
    private static void AddPointerClick(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        //PointerClickイベントを追加
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { OnPointerClick(obj); });
        trigger.triggers.Add(entry);

    }


    private static void OnPointerClick(GameObject obj)
    {     
        Lighting_ClickEvent l_ce = obj.GetComponent<Lighting_ClickEvent>();
        l_ce.Trigger();

    }

}
