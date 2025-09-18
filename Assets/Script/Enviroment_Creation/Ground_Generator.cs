using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//地面を生成するクラス
public static class Ground_Generator
{
    
    public static GameObject Generate()
    {
        var position = new Vector3(0, -50, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        return PhotonNetwork.Instantiate("ground", position, rotation);
    }

}