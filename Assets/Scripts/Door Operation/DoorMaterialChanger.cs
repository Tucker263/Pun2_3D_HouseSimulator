using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// ドアのマテリアルの変更を行うクラス
// アタッチ対象: UI
public class DoorMaterialChanger : MonoBehaviour
{
    [SerializeField] private string _materialName;
    private DoorMaterialSynchronizer _doorMaterialSynchronizer;


    private void Start()
    {
        TextMeshProUGUI buttonText = DescendantFinder.FindComponentInDescendants<TextMeshProUGUI>(this.gameObject);
        if (buttonText != null) _materialName = buttonText.text;
        else Debug.LogError($"{name}: TMP_Text が見つかりません");

        GameObject uiOperationSynchronizer = GameObject.Find(Tags.Get(TagID.UIOperationSynchronizer));
        if(uiOperationSynchronizer == null) Debug.LogError($"{name}: {Tags.Get(TagID.UIOperationSynchronizer)}が見つかりません");

        _doorMaterialSynchronizer = uiOperationSynchronizer.GetComponentInChildren<DoorMaterialSynchronizer>();
        
    }


    public void OnClickChangeMaterial()
    {
        // selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

        // 他のクライアントに同期
        _doorMaterialSynchronizer.SynchronizeOthers(_materialName);

        // ローカル反映
        _doorMaterialSynchronizer.SynchronizeLocal(_materialName);
        
    }

}