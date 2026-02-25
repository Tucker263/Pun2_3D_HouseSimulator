using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine;


// 窓のイベントを登録するクラス
public static class WindowEventRegister
{

    public static void Register(string registerTag)
    {
        List<GameObject> objList = PhotonObjectFinder.FindObjectsWithTag(registerTag);

        foreach (GameObject obj in objList)
        {
            AddPointerClick(obj);
        }

    }


    // PointerClickイベントを追加
    private static void AddPointerClick(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { OnPointerClick(obj); });
        trigger.triggers.Add(entry);

    }


    private static void OnPointerClick(GameObject obj)
    {
        WindowClickHandler windowClickHandler = obj.GetComponent<WindowClickHandler>();

        if (windowClickHandler != null) windowClickHandler.Trigger();
        else Debug.LogWarning($"{obj.name}: WindowClickHandler が見つかりませんでした");    
        
    }

}
