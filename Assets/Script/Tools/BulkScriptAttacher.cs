#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


// スクリプト自動付与ツール: エディタ拡張
// タグ、付与するスクリプトが必要
public class BulkScriptAttacher : EditorWindow
{
    [Serializable]
    public class TagScriptPair
    {
        public string Tag;
        public MonoScript Script; // 付与するスクリプト
    }

    public List<TagScriptPair> tagScriptPairs = new List<TagScriptPair>();

    [MenuItem("Tools/Bulk Script Attacher")]
    public static void ShowWindow()
    {
        GetWindow<BulkScriptAttacher>("Bulk Script Attacher");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("タグごとにスクリプトを自動付与", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Tag-Script Pair"))
        {
            tagScriptPairs.Add(new TagScriptPair());
        }

        for (int i = 0; i < tagScriptPairs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            tagScriptPairs[i].Tag = EditorGUILayout.TagField("Tag", tagScriptPairs[i].Tag);
            tagScriptPairs[i].Script = (MonoScript)EditorGUILayout.ObjectField(tagScriptPairs[i].Script, typeof(MonoScript), false);
            if (GUILayout.Button("Remove")) tagScriptPairs.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Attach Scripts to Scene and Prefabs"))
        {
            AttachScripts();
        }
    }

    private void AttachScripts()
    {
        foreach (var pair in tagScriptPairs)
        {
            if (pair.Script == null || string.IsNullOrEmpty(pair.Tag)) continue;

            Type scriptType = pair.Script.GetClass();
            if (scriptType == null || !scriptType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                Debug.LogWarning($"{pair.Script.name} は MonoBehaviour ではありません");
                continue;
            }

            // シーン上のオブジェクト
            GameObject[] sceneObjects = GameObject.FindGameObjectsWithTag(pair.Tag);
            foreach (var obj in sceneObjects)
            {
                if (obj.GetComponent(scriptType) == null)
                {
                    obj.AddComponent(scriptType);
                    Debug.Log($"[Scene] Added {pair.Script.name} to {obj.name}");
                }
            }

            // プロジェクト内の Prefab
            string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab");
            foreach (string guid in prefabGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null) continue;

                bool modified = false;
                foreach (Transform child in prefab.GetComponentsInChildren<Transform>(true))
                {
                    if (child.CompareTag(pair.Tag) && child.GetComponent(scriptType) == null)
                    {
                        child.gameObject.AddComponent(scriptType);
                        modified = true;
                        Debug.Log($"[Prefab] Added {pair.Script.name} to {child.name} in {prefab.name}");
                    }
                }
                if (modified)
                {
                    PrefabUtility.SavePrefabAsset(prefab);
                }
            }
        }

        Debug.Log("Bulk script attachment completed!");
    }
}
#endif
