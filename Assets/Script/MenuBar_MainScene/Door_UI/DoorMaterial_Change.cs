using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class DoorMaterial_Change : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI buttonTMP;
    private string materialName;

    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        materialName = buttonTMP.text;
  
    }


    public void Change()
    {
        GameObject obj = Door_SelectedObj.obj;
        if(obj != null)
        {
            ChangeDoorMaterial();

        }
      
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }


    private void ChangeDoorMaterial()
    {
        GameObject obj = Door_SelectedObj.obj;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChangeMaterialD", RpcTarget.All, obj.name, materialName);

    }
    

    [PunRPC]
    public void ChangeMaterialD(string objName, string materialName)
    {
        GameObject obj = NetworkObject_Search.GetObjectFromTagAndName("door", objName);
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/"+ materialName);

        Renderer renderer = obj.GetComponent<Renderer>();
        //マテリアルを変更
        renderer.material = material;  

    }

}