using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 生成されたオブジェクトをY軸に回転させるクラス
// アタッチ対象: UIのスライダー
public class SpawnedObjectRotaterY : MonoBehaviour
{
    private Slider _slider;


    public void Initialize()
    {
        _slider = GetComponent<Slider>();
    }


    public void OnSliderRotateY()
    {
        if(_slider == null)
        {
            Initialize();
        }

        GameObject obj = SelectedObject.obj;
        if(obj == null) return;

        //所有権の変更
        SpawnedObjectOwnership spawnedObjectOwnership =  obj.GetComponent<SpawnedObjectOwnership>();
        if(spawnedObjectOwnership == null) Debug.LogWarning($"{obj.name} :SpawnedObjectOwnershipが見つかりません");
        else spawnedObjectOwnership.RequestOwnership();

        //オブジェクトのy軸の回転
        float rotateY = _slider.value;
        obj.transform.eulerAngles = new Vector3(0, rotateY, 0);

    }
}
