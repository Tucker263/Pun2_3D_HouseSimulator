using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ドアの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class DoorInfo
{
    public string placeName; //場所の名前
    public bool isOpen; //開閉状態
    public Quaternion rotation; //回転
    public string materialName; //マテリアル
}


public static class DoorInfo_Format
{

    public static DoorInfo Convert(GameObject obj)
    {
        DoorInfo info = new DoorInfo();

        info.placeName = obj.name;
        //開閉状態
        Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
        info.isOpen = d_o.GetIsOpen();
        //rotation
        info.rotation = obj.GetComponent<Transform>().rotation;
        //マテリアル
        Renderer renderer = obj.GetComponent<Renderer>();
        string materialName = renderer.material.name;
        info.materialName = materialName.Replace(" (Instance)", "");

        return info;

    }


    public static void ApplyObject(GameObject obj, DoorInfo info)
    {
        obj.name = info.placeName;
        //ドアの開閉状態を適用
        Door_OpenClose d_o = obj.GetComponent<Door_OpenClose>();
        d_o.SetIsOpen(info.isOpen);
        //ドアの回転を適用
        obj.GetComponent<Transform>().rotation = info.rotation;
        //Resourcesフォルダ内のマテリアルをロード
        Renderer renderer = obj.GetComponent<Renderer>();
        Material material = Resources.Load<Material>("Materials/" + info.materialName);
        //ロードしたマテリアルをオブジェクトに適用
        renderer.material = material;

    }

}
