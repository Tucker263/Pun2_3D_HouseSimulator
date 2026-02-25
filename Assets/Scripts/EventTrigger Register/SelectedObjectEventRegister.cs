using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine;


// SelectedObjectのイベントを登録するクラス
public static class SelectedObjectEventRegister
{

    public static void Register(string registerTag)
    {
        List<GameObject> objList = PhotonObjectFinder.FindObjectsWithTag(registerTag);

        foreach (GameObject obj in objList)
        {
            AddPointerClick(obj);
        }

    }


    public static void RegisterFromObj(GameObject obj)
    {
        AddPointerClick(obj);
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
        SelectedObjectClickHandler selectedObjectClickHandler = obj.GetComponent<SelectedObjectClickHandler>();

        if (selectedObjectClickHandler != null) selectedObjectClickHandler.Trigger();
        else Debug.LogWarning($"{obj.name}: SelectedObjectClickHandler が見つかりませんでした");
        
    }
    
}

