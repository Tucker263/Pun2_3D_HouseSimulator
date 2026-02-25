using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 視点回転
// アタッチ対象: OVRInput Rotaterオブジェクト
public class OVRInputRotater : MonoBehaviour
{
    // 回転速度（度/秒)
    [SerializeField] private float _speed = 60f;

    // CameraRig
    private GameObject _cameraRig;
    private Transform _cameraRigTransform;


    void Start()
    {
        _cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (_cameraRig == null) Debug.LogError($"{Tags.Get(TagID.CameraRig)}が見つかりませんでした");

        _cameraRigTransform = _cameraRig.transform;

    }


    void Update()
    {
        Rotate();

    }


    public void Rotate()
    {
        float rotationAmount = _speed * Time.deltaTime;

        // 長押しで視点回転
        if (Input.GetKey(KeyCode.T))
            _cameraRigTransform.Rotate(0, -rotationAmount, 0);
        if (Input.GetKey(KeyCode.Y))
            _cameraRigTransform.Rotate(0, rotationAmount, 0);
    
    }

}