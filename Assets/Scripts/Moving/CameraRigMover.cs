using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


// CameraRigを指定した位置に移動させるクラス
// アタッチ対象: UIのボタン
public class CameraRigMover : MonoBehaviour
{
    [SerializeField] private Vector3 _position;

    private GameObject _cameraRig;
    private Transform _cameraRigTransform;
  

    public void Initialize()
    {      
        _cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (_cameraRig == null) Debug.LogError($"{name} : {Tags.Get(TagID.CameraRig)}が見つかりません");

        _cameraRigTransform = _cameraRig.transform;

    }


    public void OnClickMoveCameraRig()
    {
        if(_cameraRig == null)
        {
            Initialize();
        }
        
        _cameraRigTransform.position = _position;
        // selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
