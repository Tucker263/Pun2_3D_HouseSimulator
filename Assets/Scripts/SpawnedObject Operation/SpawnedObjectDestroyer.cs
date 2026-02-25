using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


// 生成されたオブジェクトを破棄するクラス
// アタッチ対象: UIのボタン

[RequireComponent(typeof(ObjectSelector))]
public class SpawnedObjectDestroyer : MonoBehaviour
{
    private ObjectSelector _objectSelector;

    private void Awake()
    {
        _objectSelector = GetComponent<ObjectSelector>();
    }

    public void OnClickDestroySelectedObject()
    {
        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        // 所有権の変更
        var spawnedObjectOwnership = obj.GetComponent<SpawnedObjectOwnership>();
        if(spawnedObjectOwnership != null) spawnedObjectOwnership.RequestOwnership();
        else Debug.LogWarning($"{obj.name} :SpawnedObjectOwnershipが見つかりません");

        // オブジェクトの破棄
        PhotonNetwork.Destroy(obj);

        // オブジェクトの選択を解除
        _objectSelector.Unselect();

    }

}