using UnityEngine;

// オフラインモードを切り替えるクラス
// アタッチ対象: UIのボタン
public class OfflineModeToggler : MonoBehaviour
{
    public void OnClickToggleOfflineMode(bool isOfflineMode)
    {
        Config.IsOfflineMode = isOfflineMode;
    }
}
