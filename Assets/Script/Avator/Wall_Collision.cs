using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;



//壁に当たっているかを判定するクラス
public class Wall_Collision : MonoBehaviour
{
    public static bool isCollision;

    void Start()
    {
        isCollision = false;
    }

    void Update()
    {
        
    }
    
    //物体が当たった時の処理
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("当たった");
        isCollision = collision.gameObject.CompareTag("outerWall") || collision.gameObject.CompareTag("innerWall");
  
    }

    //物体が触れ続けている時の処理
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("続けている");
        isCollision = collision.gameObject.CompareTag("outerWall") || collision.gameObject.CompareTag("innerWall");

    }

    //物体が離れた時の処理
    void OnCollisionExit(Collision other)
    {
        //Debug.Log("離れた");
        isCollision = false;

    }

}