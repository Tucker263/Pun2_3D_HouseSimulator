using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//メニューバーのオンオフ処理
public class OVRInput_SwitchMenuBar : MonoBehaviour
{
    private GameObject menuBar;

    void Start()
    {
        menuBar = GameObject.Find("MenuBar");

    }

    void Update()
    {
        SwitchMenuBar();
    }
    
    public void SwitchMenuBar()
    {
        //メニューバーのオンオフ処理、Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = menuBar;
            obj.SetActive(!obj.activeSelf);
        }
    }

}