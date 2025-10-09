using System;
using System.Collections.Generic;
using UnityEngine;


// 環境を生成するクラス
public static class EnvironmentGenerator
{

    public static void Generate()
    {
        //Avatarを生成
        AvatarFactory.Create();
        //Sunを生成
        SunFactory.Create();
        //StreetLightsを生成
        StreetLightsFactory.Create();
        //Houseを生成
        HouseFactory.Create();
        //SmallHouseを生成
        SmallHouseFactory.Create();
        
    }

}