using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//house_smallの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class HouseSmallInfo
{
    public string name;
    public bool isActive;
}


public static class HouseSmallInfo_Format
{

    public static HouseSmallInfo Convert(GameObject obj)
    {
        HouseSmallInfo info = new HouseSmallInfo();
        info.name = obj.name;
        info.isActive = obj.activeSelf;

        return info;

    }


    public static void ApplyObject(GameObject obj, HouseSmallInfo info)
    {
        obj.name = info.name;
        obj.SetActive(info.isActive); 

    }

}