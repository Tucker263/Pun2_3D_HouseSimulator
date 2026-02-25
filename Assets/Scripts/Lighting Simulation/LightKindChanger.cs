using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// 照明の種類を変更するクラス
// アタッチ対象: UIのボタン
public class LightKindChanger : MonoBehaviour
{
    private string _lightKind;
    private LightKindSynchronizer _lightKindSynchronizer;


    private void Start()
    {
        TextMeshProUGUI buttonTMP = DescendantFinder.FindComponentInDescendants<TextMeshProUGUI>(this.gameObject);
        if(buttonTMP == null) Debug.LogError($"{name} :TextMeshProUGUIが見つかりません");
        _lightKind = buttonTMP.text;
        _lightKind = _lightKind.Replace(" ", "");

        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _lightKindSynchronizer = uiOperationSynchronizer.GetComponentInChildren<LightKindSynchronizer>();

    }


    public void OnClickChangeLightKind()
    {
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        // 他のクライアントに同期
        _lightKindSynchronizer.SynchronizeOthers(_lightKind);

        // ローカル反映
        _lightKindSynchronizer.SynchronizeLocal(_lightKind);

    }

}
