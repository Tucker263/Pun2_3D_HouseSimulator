using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//house_smallを生成するクラス
public static class HouseSmall_Generator
{
    
    public static GameObject Generate()
    {
        var houseSmallPosition = new Vector3(15, 0, 0);
        Quaternion houseSmallRotation = Quaternion.Euler(0, 90, 0);
        GameObject houseSmall = PhotonNetwork.Instantiate("house_small", houseSmallPosition, houseSmallRotation);

        //名前が被ると、オブジェクトの操作などが上手くいかない
        NameAllObject(houseSmall);

        return houseSmall;
    }


    public static void NameAllObject(GameObject houseSmall)
    {
        //番号を振る処理
        List<Transform> houseSmall_children = DescendantObject_Search.GetList(houseSmall.GetComponent<Transform>());
        for(int i = 0; i < houseSmall_children.Count; i++)
        {
            houseSmall_children[i].name += (i+1);
        }

        //階層を振る処理、オブジェクトの下から順に、_1F
        List<Transform> structureList = DescendantObject_Search.GetListFromTag(houseSmall.GetComponent<Transform>(), "structure");
        for(int i = 0; i < structureList.Count; i++)
        {
            Transform structure = structureList[i];
            int numFloor = structureList.Count - i;

            List<Transform> structure_children = DescendantObject_Search.GetList(structure);
            foreach(Transform child in structure_children)
            {
                child.name += "_" + numFloor + "F";
            }
            structure.name += "_" + numFloor + "F";
        }

        //接尾語を振る処理、_small
        foreach(Transform child in houseSmall_children)
        {
            child.name += "_small";
        }

    }
  
}
