using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Transformの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class TransformInfo
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}


public static class TransformInfo_Format
{
    public static TransformInfo Convert(GameObject obj)
    {
        TransformInfo info = new TransformInfo();
        info.name = obj.name;
        info.position = obj.GetComponent<Transform>().position;
        info.rotation = obj.GetComponent<Transform>().rotation;

        return info;
    }

    public static void ApplyObject(GameObject obj, TransformInfo info)
    {
        obj.name = info.name;
        Transform obj_tf = obj.GetComponent<Transform>();
        obj_tf.position = info.position;
        obj_tf.rotation = info.rotation;
    }

}
