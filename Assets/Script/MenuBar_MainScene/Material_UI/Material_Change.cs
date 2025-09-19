using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Material_Change : MonoBehaviourPunCallbacks
{
    public string targetTag;

    private TextMeshProUGUI buttonTMP;
    private string materialName;
    
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        materialName = buttonTMP.text;
    }


    public void Change()
    {
        GameObject obj = SelectedObject.obj;
        if(obj != null)
        {
                ChangeMaterial(targetTag);
        }
      
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

    private void ChangeMaterial(string targetTag)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReflectMaterial", RpcTarget.All, targetTag, materialName);
        
    }


    [PunRPC]
    public void ReflectMaterial(string targetTag, string materialName)
    {
        List<GameObject> objList = NetworkObject_Search.GetListFromTag(targetTag);
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/"+ materialName);
        foreach(GameObject obj in objList)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            //マテリアルを変更
            renderer.material = material;
        }

    }

}
