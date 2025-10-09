using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// オブジェクトを生成するクラス
public static class SpawnedObjectFactory
{
    private static float _spawnDistance = 3.0f;


    public static GameObject Create(SpawnedObjectData spawnedObjectData)
    {
        if (spawnedObjectData == null)
        {
            Debug.LogError("SpawnedObjectData が null です");
            return null;
        }

        GameObject cameraRig = GameObject.FindWithTag(Tags.Get(TagID.CameraRig));
        if (cameraRig == null)
        {
            Debug.LogError($"{Tags.Get(TagID.CameraRig)} が見つかりませんでした");
            return null;
        }

        Transform cameraRigTransform = cameraRig.transform;

        // 自分の正面にオブジェクトの生成を指定
        Vector3 position = GetFrontSpawnPosition(cameraRigTransform);
        // オブジェクトの生成
        GameObject obj = PhotonNetwork.Instantiate(spawnedObjectData.photonResourcePath, position, Quaternion.identity);
        // オブジェクトをy軸だけ自分方向に向ける
        FaceCameraYOnly(obj, cameraRigTransform);

        return obj;
    }


    private static Vector3 GetFrontSpawnPosition(Transform cameraRigTransform)
    {
        if (cameraRigTransform == null) return Vector3.zero;

        double radianY = Math.PI * cameraRigTransform.eulerAngles.y / 180.0;
        float offsetX = (float)(_spawnDistance * Math.Sin(radianY));
        float offsetZ = (float)(_spawnDistance * Math.Cos(radianY));

        Vector3 position = new Vector3(
            cameraRigTransform.position.x + offsetX,
            cameraRigTransform.position.y + 3,
            cameraRigTransform.position.z + offsetZ
        );

        return position;
    }


    private static void FaceCameraYOnly(GameObject obj, Transform cameraRigTransform)
    {
        if (obj == null || cameraRigTransform == null) return;

        Vector3 direction = cameraRigTransform.position - obj.transform.position;
        //Y軸だけを考慮
        direction.y = 0;

        if (direction.sqrMagnitude > 0.0001f)
        {
            //回転させるQuaternionを計算
            Quaternion rotation = Quaternion.LookRotation(direction);
            obj.transform.rotation = rotation;
        }
    }
    
}
