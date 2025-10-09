using UnityEngine;
using UnityEngine.UI;
using TMPro;


// 生成オブジェクトの生成ボタンを生成するクラス
public static class SpawnedObjectButtonFactory
{   

    public static GameObject Create(GameObject buttonPrefab, Transform contentParent, SpawnedObjectData data)
    {
        GameObject buttonObj = Object.Instantiate(buttonPrefab, contentParent);
        buttonObj.name = $"Btn_{data.name}";

        // ラベル
        TextMeshProUGUI tmp = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null) tmp.text = data.displayName;

        // アイコン
        Image img = buttonObj.GetComponentInChildren<Image>();
        if (img != null) img.sprite = data.icon;

        return buttonObj;

    }

}
