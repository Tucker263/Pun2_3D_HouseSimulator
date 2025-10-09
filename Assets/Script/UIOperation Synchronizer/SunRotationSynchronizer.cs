using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 太陽の回転を同期させるクラス
// アタッチ対象: Sun Synchronizer

[RequireComponent(typeof(PhotonView))]
public class SunRotationSynchronizer : MonoBehaviourPunCallbacks
{
    private GameObject _sun;
    private Transform _sunTransform;
    private SunOwnership _sunOwnership;

  
    private void Initialize()
    {
        _sun = PhotonObjectFinder.FindWithTag(Tags.Get(TagID.Sun));
        if(_sun == null)
        {
            Debug.LogWarning($"{name} :{Tags.Get(TagID.Sun)}が見つかりませんでした。");
            return;
        }

        _sunTransform = _sun.transform;
        _sunOwnership = _sun.GetComponent<SunOwnership>();

    }

    public void Synchronize(float xAngle)
    {
        if(_sun == null) Initialize();

        // 所有権を取得
        // 所有権の譲渡で、他の人が触っても位置の同期が可能
        if (!_sunOwnership.IsMine) _sunOwnership.RequestOwnership();

        _sunTransform.eulerAngles = new Vector3(xAngle, 0f, 0f);

    }

}
