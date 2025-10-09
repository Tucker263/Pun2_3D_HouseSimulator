using UnityEngine;
using Photon.Pun;


// 基底クラス: 全てのBaseInfoファイルが対象
// name、tansform、viewIDが共通情報
[System.Serializable]
public class BaseInfo
{
    public string name; // オブジェクト名

    public Vector3 position; // 座標
    public Quaternion rotation; // 回転
    public Vector3 scale;    // スケール

    public int viewID; // PhotonViewID

    // GameObjectから共通情報を抽出
    public virtual void ExtractFrom(GameObject obj)
    {
        name = obj.name;
        name = name.Replace("(Clone)", "");
        position = obj.transform.position;
        rotation = obj.transform.rotation;
        scale = obj.transform.localScale;

        // PhotonViewID
        PhotonView targetView = obj.GetPhotonView();
        viewID = targetView != null ? targetView.ViewID : -1;
    }

    // GameObjectに共通情報を適用
    public virtual void ApplyTo(GameObject obj)
    {
        obj.name = name;
        obj.name = obj.name.Replace("(Clone)", "");
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
    }
}
