using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//現在の状態に同期するクラス
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class CurrentState_Synchronize : MonoBehaviourPunCallbacks
{
 
    public void Synchronize()
    {
        //場所の名前が被るとうまく反映できないので、注意!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //オブジェクトの場所の名前の同期、これをしないとゲスト側の同期がうまくいかない
        ObjectName_Synchronize on_s = GetComponent<ObjectName_Synchronize>();
        on_s.Synchronize();

        //イベントトリガー登録の同期
        EventTrigger_Synchronize e_r = GetComponent<EventTrigger_Synchronize>();
        e_r.Synchronize();

        //天井、内壁、床、外壁、屋根の同期
        Material_Synchronize m_s = GetComponent<Material_Synchronize>();
        List<string> materialTag = new List<string>()
        {
            "ceiling", 
            "innerWall", 
            "floor",
            "outerWall",
            "roof"
        };
        foreach(string tag in materialTag)
        {
            m_s.Synchronize(tag);
        }


        //ドアの同期
        Door_Synchronize d_s = GetComponent<Door_Synchronize>();
        d_s.Synchronize();

        //照明の同期
        Lighting_Synchronize l_s = GetComponent<Lighting_Synchronize>();
        l_s.Synchronize();

        //house_smallの情報の同期
        HouseSmall_Synchronize hs_s = GetComponent<HouseSmall_Synchronize>();
        hs_s.Synchronize();
        
    }

}