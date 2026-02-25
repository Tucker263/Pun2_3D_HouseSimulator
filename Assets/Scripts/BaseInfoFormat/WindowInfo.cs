using UnityEngine;


// 窓の情報を保持するクラス
[System.Serializable]
public class WindowInfo: BaseInfo
{
    public bool isSliding;          // 開閉状態
    

    // GameObjectから必要な情報を抽出
    public override void ExtractFrom(GameObject obj)
    {
        base.ExtractFrom(obj);

        // 開閉状態
        var windowSliding = obj.GetComponent<WindowSliding>();
        if (windowSliding != null) isSliding = windowSliding.IsSliding;
        else Debug.LogWarning($"{obj.name}: windowSlidingが見つかりませんでした。");

    }


    // WindowInfo を GameObject に適用
    public override void ApplyTo(GameObject obj)
    {
        var windowSliding = obj.GetComponent<WindowSliding>();
        if (windowSliding == null)
        {
            Debug.LogWarning($"{obj.name}: windowSlidingが見つかりませんでした。");
            return;
        }

        // 初期化処理
        windowSliding.Initialize();

        // 開閉状態の適用
        windowSliding.IsSliding = isSliding;

        base.ApplyTo(obj);

    }
    
}
