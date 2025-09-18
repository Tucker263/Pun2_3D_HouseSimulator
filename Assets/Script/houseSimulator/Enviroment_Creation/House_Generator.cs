using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//houseを生成するクラス
public static class House_Generator
{
   
    public static GameObject Generate()
    {
        var position = new Vector3(0, 0, 0);
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        GameObject house = PhotonNetwork.Instantiate("house", position, rotation);

        NameAllObject(house);

        return house;
    }


    public static void NameAllObject(GameObject house)
    {
        //番号を振る処理
        List<Transform> house_children = DescendantObject_Search.GetList(house.GetComponent<Transform>());
        for(int i = 0; i < house_children.Count; i++)
        {
            house_children[i].name += (i+1);
        }
        
        //階層を振る処理、オブジェクトの下から順に、_1F
        List<Transform> structureList = DescendantObject_Search.GetListFromTag(house.GetComponent<Transform>(), "structure");
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

    }        
    
}
