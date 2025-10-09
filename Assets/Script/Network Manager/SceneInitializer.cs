using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// シーン内の初期化を担当するクラス
// アタッチ対象: Connect Manager
public class SceneInitializer : MonoBehaviour
{
    private const string _directionalLightName = "DirectionalLight";
    private const string _defaultUIName = "Panel_Default";

    private GameObject _directionalLight;
    private GameObject _menuObj;
    private GameObject _ovrObj;


    public void Initialize()
    {
        _menuObj = GameObject.FindWithTag(Tags.Get(TagID.MenuBarCanvas));
        _ovrObj = GameObject.FindWithTag(Tags.Get(TagID.OVRInputManager));
        _directionalLight = GameObject.Find(_directionalLightName);

        if (_menuObj == null) Debug.LogError($"{name}: {Tags.Get(TagID.MenuBarCanvas)}が見つかりませんでした。");
        if (_ovrObj == null) Debug.LogError($"{name}: {Tags.Get(TagID.OVRInputManager)}が見つかりませんでした。");
        if (_directionalLight == null) Debug.LogError($"{name}: {_directionalLightName}が見つかりませんでした。");

        // アクティブ関連の処理
        _menuObj.SetActive(false);
        _ovrObj.SetActive(false);
        _directionalLight.SetActive(true);

    }


    public void SetOfflineMode()
    {
        _directionalLight.SetActive(false);
    }


    public void SetOnlineMode()
    {
        _ovrObj.SetActive(true);
        _directionalLight.SetActive(false);
    }


    public void SetCameraRigStartPosition()
    {
        GameObject cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (cameraRig == null)
        {
            Debug.LogError($"{Tags.Get(TagID.CameraRig)} が見つかりませんでした。");
            return;
        }

        Vector3 position = new Vector3(Random.Range(-8f, 8f), 2f, Random.Range(-17f, -10f));
        cameraRig.transform.position = position;

    }
    

    public void SetUpMenuBar()
    {
        if (_menuObj == null) return;

        _menuObj.SetActive(true);

        MenuBarDictionary menuBarDictionary = _menuObj.GetComponent<MenuBarDictionary>();
        if (menuBarDictionary == null)
        {
            Debug.LogError("MenuBarDictionaryが見つかりませんでした");
            return;
        }

        menuBarDictionary.Initialize();

        foreach (var uiElement in MenuBarDictionary.UiTable.Values)
        {
            uiElement.SetActive(true);
        }

        foreach (var uiElement in MenuBarDictionary.UiTable.Values)
        {
            if (MenuBarDictionary.UiTable[_defaultUIName] != uiElement)
            {
                uiElement.SetActive(false);
            }
        }

    }

}
