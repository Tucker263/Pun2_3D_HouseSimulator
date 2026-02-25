using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// アバターのメッシュをカメラの動きと同期させるクラス
// アタッチ対象: Avatarオブジェクト
public class AvatarMesh : MonoBehaviourPunCallbacks
{
    private int _creatorId;
    private bool _isMine;

    private GameObject _cameraRig;
    private Transform _cameraRigTransform;
    private Transform _avatarTransform;


    private void Start()
    {
        Initialize();

    }

    private void Update()
    {
        if (_isMine)
        {
            // アバターのメッシュをVR用カメラの動きと同期させる
            _avatarTransform.position = _cameraRigTransform.position;
            _avatarTransform.rotation = _cameraRigTransform.rotation;
        }

    }

    private void Initialize()
    {
        _creatorId = photonView.CreatorActorNr;
        _isMine = photonView.IsMine;

        // CameraRigを取得
        _cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (_cameraRig == null)
        {
            Debug.LogError($"{Tags.Get(TagID.CameraRig)}が見つかりませんでした。");
            return;
        }
        _cameraRigTransform = _cameraRig.transform;

        _avatarTransform = transform;

    }

    // Getter
    public int CreatorId => _creatorId;
    public bool IsMine => _isMine;

}

