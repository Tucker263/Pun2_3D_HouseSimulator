using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


// ドアのイベントを登録するクラス
public static class DoorEventRegister
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

        // PointerClickイベントを追加
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { OnPointerClick(obj); });
        trigger.triggers.Add(entry);

    }


    private static void OnPointerClick(GameObject obj)
    {
        var doorClickHandler = obj.GetComponent<DoorClickHandler>();

        if (doorClickHandler != null) doorClickHandler.Trigger();
        else Debug.LogWarning($"{obj.name}: DoorClickHandler が見つかりませんでした");

    }
    
}
