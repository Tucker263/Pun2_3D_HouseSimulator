using UnityEngine;


// SmallHouse の情報を保持するクラス
[System.Serializable]
public class SmallHouseInfo : BaseInfo
{
    public bool isActive;  // アクティブ状態（表示／非表示）


    // GameObjectから必要な情報を抽出
    public override void ExtractFrom(GameObject obj)
    {
        base.ExtractFrom(obj);

        isActive = obj.activeSelf;

    }


    // SmallHouseInfoをGameObjectに適用
    public override void ApplyTo(GameObject obj)
    {
        base.ApplyTo(obj);
        
        obj.SetActive(isActive);

    }

}
