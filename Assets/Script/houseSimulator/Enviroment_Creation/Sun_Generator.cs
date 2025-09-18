using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//sunを生成するクラス
public static class Sun_Generator
{
    
    public static GameObject Generate()
    {
        var position = new Vector3(0, 100, 0);
        Quaternion rotation = Quaternion.Euler(90, 0, 0);

        return PhotonNetwork.Instantiate("sun", position, rotation);
    }

}