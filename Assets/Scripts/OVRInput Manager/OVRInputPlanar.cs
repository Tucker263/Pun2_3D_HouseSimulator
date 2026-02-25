using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 平面移動
// アタッチ対象: OVRInput Planarオブジェクト
public class OVRInputPlanar : MonoBehaviour
{
    // 移動速度
    [SerializeField]private float _speed = 6f;

    // CameraRig
    private GameObject _cameraRig;
    private Transform _cameraRigTramsform;


    private void Start()
    {
        _cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (_cameraRig == null) Debug.LogError($"{Tags.Get(TagID.CameraRig)}が見つかりませんでした");

        _cameraRigTramsform = _cameraRig.transform;

    }


    private void Update()
    {
        Locomote();

    }


    public void Locomote()
    {
        Transform objTransform = _cameraRigTramsform;

        // 平面移動
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _speed = WallCollision.isCollision ? 1f : 6f;
        objTransform.transform.Translate(_speed * Time.deltaTime * input.normalized);

    }

}