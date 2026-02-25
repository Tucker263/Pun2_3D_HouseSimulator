using UnityEngine;

// 初回起動を切り替えるクラス
// アタッチ対象: UIのボタン
public class InitialStartToggler : MonoBehaviour
{
    public void OnClickToggleInitialStart(bool isInitialStart)
    {
        Config.IsInitialStart = isInitialStart;
    }

}
