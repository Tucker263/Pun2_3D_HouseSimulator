using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// 壁に当たっているかを判定するクラス、改良中!!!!!!!!!!
// アタッチ対象: Avatar
public class WallCollision : MonoBehaviour
{
    public static bool isCollision;

    private void Start()
    {
        isCollision = false;
    }
    
    //物体が当たった時の処理
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("当たった");
        isCollision = collision.gameObject.CompareTag("outerWall") || collision.gameObject.CompareTag("innerWall");
  
    }

    //物体が触れ続けている時の処理
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("続けている");
        isCollision = collision.gameObject.CompareTag("outerWall") || collision.gameObject.CompareTag("innerWall");

    }

    //物体が離れた時の処理
    private void OnCollisionExit(Collision other)
    {
        //Debug.Log("離れた");
        isCollision = false;

    }

}