using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// マテリアルを変更するクラス
// アタッチ対象: UI
public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private string _materialName;
    private MaterialSynchronizer _materialSynchronizer;
  

    private void Start()
    {
        TextMeshProUGUI buttonText = DescendantFinder.FindComponentInDescendants<TextMeshProUGUI>(this.gameObject);
        if (buttonText != null) _materialName = buttonText.text.Trim();
        else Debug.LogError($"{name}: TMP_Text が見つかりません");

        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _materialSynchronizer = uiOperationSynchronizer.GetComponentInChildren<MaterialSynchronizer>();

    }


    public void OnClickChangeMaterial()
    {
        // selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        // 他のクライアントに同期
        _materialSynchronizer.SynchronizeOthers(_materialName);

        // ローカル反映
        _materialSynchronizer.SynchronizeLocal(_materialName);
      
    }

}
    