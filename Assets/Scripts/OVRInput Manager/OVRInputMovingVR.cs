using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 移動処理、VR想定
// アタッチ対象: OVRInput Movingオブジェクト
public class OVRInputMovingVR : MonoBehaviour
{/*
    // 移動速度
    [SerializeField] private float _speed = 2f;
    // スティック感度、強すぎるため10分の1に
    private float _stickSensitivity = 0.1f;
    // Transform
    private Transform _cameraRigTransform;
    private Transform _centerEyeTransform;


    void Start()
    {
        GameObject cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (cameraRig == null) Debug.LogError($"{Tags.Get(TagID.CameraRig)}が見つかりませんでした");
        GameObject centerEyeAnchor = GameObject.Find(Tags.Get(TagID.CenterEyeAnchor));
        if (centerEyeAnchor == null) Debug.LogError($"{Tags.Get(TagID.CenterEyeAnchor)}が見つかりませんでした");

        _cameraRigTransform = cameraRig.transform;
        _centerEyeTransform = centerEyeAnchor.transform;

    }


    void Update()
    {
        // 左スティックで移動
        Move();
    }


    void Move()
    {   
        // 左手のアナログスティックの向きを取得
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector3 changePosition = new Vector3((stickL.x * _speed * _stickSensitivity), 0, (stickL.y * _speed * _stickSensitivity));

        // cameraRigの角度の誤差を修正
        float angleY = _cameraRigTransform.eulerAngles.y;
        // HMDのY軸の角度取得
        Vector3 changeRotation = new Vector3(0, _centerEyeTransform.eulerAngles.y - angleY, 0);
        // CameraRigの位置変更
        _cameraRigTransform.position += _cameraRigTransform.rotation * (Quaternion.Euler(changeRotation) * changePosition);

    }
    */
}
