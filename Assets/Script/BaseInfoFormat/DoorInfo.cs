using System;
using UnityEngine;


// ドアの情報を保持
[System.Serializable]
public  class DoorInfo : BaseInfo
{
    public bool isOpen;            // 開閉状態
    public string materialName;    // マテリアル名


    //オブジェクトから必要な情報を抽出
    public override void ExtractFrom(GameObject obj)
    {
        base.ExtractFrom(obj);

        // 開閉状態
        var doorOpenClose = obj.GetComponent<DoorOpenClose>();
        if (doorOpenClose == null)
        {
            Debug.LogWarning($"{obj.name}: DoorOpenCloseコンポーネントが見つかりません");
            return;
        }
        isOpen = doorOpenClose.IsOpen;

        // マテリアル名
        var renderer = obj.GetComponent<Renderer>();
        materialName = renderer.material.name;
        materialName = materialName.Replace(" (Instance)", "");

    }


    // DoorInfo を GameObject に適用
    public override void ApplyTo(GameObject obj)
    {
        // 開閉状態
        var doorOpenClose = obj.GetComponent<DoorOpenClose>();
        if (doorOpenClose == null)
        {
            Debug.LogWarning($"{obj.name}: DoorOpenCloseコンポーネントが見つかりません");
            return;
        }
        doorOpenClose.IsOpen= isOpen;

        base.ApplyTo(obj);

        // マテリアル
        Material material = MaterialCache.GetValue(materialName);
        if(material == null)
        {
            Debug.LogWarning($"{materialName}が見つかりませんでした");
            return;
        }
        
        var renderer = obj.GetComponent<Renderer>();
        renderer.material = material;


        // ドアフレームのマテリアルを適用
        var parent = obj.transform.parent;
        if (parent != null)
        {
            var parentRenderer = parent.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                parentRenderer.material = material;
            }

        }

    }

}