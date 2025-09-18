using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//avatorを生成するクラス
public static class Avator_Generator
{
   
    public static GameObject Generate()
    {
        //avatorの位置はVR用カメラと同期させるので、何でもいい
        return PhotonNetwork.Instantiate("avator", new Vector3(0, 0, 0), Quaternion.identity);
    }

}