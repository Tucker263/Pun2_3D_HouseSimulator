using UnityEngine;


// マテリアルの情報を保持するクラス
[System.Serializable]
public class MaterialInfo: BaseInfo
{
    public string materialName; // マテリアル名

    // GameObject から必要な情報を抽出
    public override void ExtractFrom(GameObject obj)
    {
        base.ExtractFrom(obj);

        // マテリアル
        var renderer = obj.GetComponent<Renderer>();
        materialName = renderer.material.name;
        materialName = materialName.Replace(" (Instance)", "");


    }


    // MaterialInfo を GameObject に適用
    public override void ApplyTo(GameObject obj)
    {
        base.ApplyTo(obj);
        
        // マテリアル
        var renderer = obj.GetComponent<Renderer>();

        Material material = MaterialCache.GetValue(materialName);
        if(material == null)
        {
            Debug.LogWarning($"{materialName}が見つかりませんでした");
            return;
        }

        renderer.material = material;
        
    }

}
