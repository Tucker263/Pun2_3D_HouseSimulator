using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine;

//MaterialObjのイベントトリガーを登録するクラス
public static class MaterialObj_EventTrigger_Register
{
    //ダブルクリック判別用
    private static float lastClickTime = 0f;
    private static float doubleClickThreshold = 0.3f;

    public static void register(string registerTag)
    {
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
        MaterialObj_ClickEvent m_c = obj.GetComponent<MaterialObj_ClickEvent>();
        m_c.Trigger();

    }

}
