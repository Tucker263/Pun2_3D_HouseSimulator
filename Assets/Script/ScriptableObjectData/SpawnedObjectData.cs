using UnityEngine;

// 親クラス: 生成オブジェクト
public class SpawnedObjectData : ScriptableObject
{
    [Header("基本情報")]
    public string id;                   // 識別用
    public string name;                 // 名前
    public string displayName;          // UI表示用
    public Sprite icon;                 // UIアイコン
    public string photonResourcePath;   // Photon用パス: "Furniture/Chair"
}
