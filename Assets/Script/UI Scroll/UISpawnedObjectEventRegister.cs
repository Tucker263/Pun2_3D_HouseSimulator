using UnityEngine;
using UnityEngine.UI;


// 生成するオブジェクトのボタンのイベント登録を行うクラス
public static class UISpawnedObjectEventRegister
{
    
    public static void Register(GameObject buttonObj, SpawnedObjectData spawnedObjectData)
    {
        if (buttonObj == null || spawnedObjectData == null) return;

        // ボタン取得
        Button btn = buttonObj.GetComponent<Button>();
        if (btn == null) return;

        // Factory 取得（ボタンにアタッチされていること前提）
        UISpawnedObjectFactory factory = buttonObj.GetComponent<UISpawnedObjectFactory>();
        if (factory == null)
        {
            Debug.LogError($"{buttonObj.name} に UISpawnedObjectFactory がアタッチされていません");
            return;
        }

        // クロージャでイベント登録
        btn.onClick.AddListener(() => factory.OnClickCreate(spawnedObjectData));
    }

}
