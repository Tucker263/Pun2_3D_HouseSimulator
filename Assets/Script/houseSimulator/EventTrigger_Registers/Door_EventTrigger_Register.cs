using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine;

//ドアのイベントトリガーを登録するクラス
public static class Door_EventTrigger_Register
{

    public static void register()
    {
        string registerTag = "door";
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
        Door_ClickEvent d_cl = obj.GetComponent<Door_ClickEvent>();
        d_cl.Trigger();

    }

}
