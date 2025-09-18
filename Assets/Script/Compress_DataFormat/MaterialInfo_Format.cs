using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//マテリアルの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class MaterialInfo
{
    public string placeName; //場所の名前
    public string materialName; //マテリアルの名前
}


public static class MaterialInfo_Format
{

    public static MaterialInfo Convert(GameObject obj)
    {
        MaterialInfo info = new MaterialInfo();
        info.placeName = obj.name;

        Renderer renderer = obj.GetComponent<Renderer>();
        string materialName = renderer.material.name;
        info.materialName = materialName.Replace(" (Instance)", "");

        return info;
    }

    public static void ApplyObject(GameObject obj, MaterialInfo info)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Material material = Resources.Load<Material>("Materials/" + info.materialName);
        renderer.material = material;

    }
    
}
