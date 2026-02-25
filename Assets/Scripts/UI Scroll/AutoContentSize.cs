using UnityEngine;
using UnityEngine.UI;


// contentの縦サイズを自動調整
// アタッチ対象: content

[RequireComponent(typeof(RectTransform))]
public class AutoContentSize : MonoBehaviour
{
    [Header("横に並べる列数")]
    [SerializeField] private int columns = 3;

    [Header("ボタン間のスペース (X, Y)")]
    [SerializeField] private Vector2 spacing = new Vector2(15f, 25f);

    private RectTransform _contentRect;

    private void Awake()
    {
        _contentRect = GetComponent<RectTransform>();
    }


    // Content の高さを自動計算して設定
    public void UpdateContentHeight()
    {
        int childCount = _contentRect.childCount;
        if (childCount == 0) return;

        // ボタンサイズは最初の子から取得
        RectTransform firstChild = _contentRect.GetChild(0) as RectTransform;
        float cellHeight = firstChild.rect.height;

        // 必要な行数を計算（切り上げ）
        int rows = Mathf.CeilToInt((float)childCount / columns);

        // 高さ = 行数 * (ボタン高さ + Y間隔)
        float height = rows * cellHeight + rows * spacing.y;

        // Content の高さに反映
        _contentRect.sizeDelta = new Vector2(_contentRect.sizeDelta.x, height);
    }
}
