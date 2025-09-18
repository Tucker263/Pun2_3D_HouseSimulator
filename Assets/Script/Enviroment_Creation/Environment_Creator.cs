using UnityEngine;

public static class Environment_Creator
{
    public static void Create()
    {
        //avatorの生成
        Avator_Generator.Generate();
        //sunの生成
        Sun_Generator.Generate();
        //groundの生成
        Ground_Generator.Generate();
        //houseの生成
        House_Generator.Generate();
        //house_smallの生成
        HouseSmall_Generator.Generate();

    }
    
}