using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


// 家の照明のイベントを登録するクラス
public static class HouseLightingEventRegister
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
        HouseLightingClickHandler houseLightingClickHandler = obj.GetComponent<HouseLightingClickHandler>();

        if (houseLightingClickHandler != null) houseLightingClickHandler.Trigger();
        else Debug.LogWarning($"{obj.name}: HouseLightingClickHandler が見つかりませんでした");

    }
}

