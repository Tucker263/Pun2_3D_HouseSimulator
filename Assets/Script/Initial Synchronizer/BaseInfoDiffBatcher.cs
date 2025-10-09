using System.Collections.Generic;
using UnityEngine;


// オブジェクトBaseInfo型の差分リストをバッチ化するクラス
public static class BaseInfoDiffBatcher
{
    private static int _batchSize = 50; // 初期は50

    // バッチサイズを設定
    public static void SetBatchSize(int size)
    {
        _batchSize = size;
    }

    // 差分リストをバッチ化
    public static List<string[]> ConvertBatches(List<BaseInfo> diffList)
    {
        List<string[]> batches = new List<string[]>();
        List<string> currentBatch = new List<string>();

        foreach (var info in diffList)
        {
            string typeName = info.GetType().AssemblyQualifiedName;
            string json = JsonUtility.ToJson(info);
            currentBatch.Add($"{typeName}:{json}");

            if (currentBatch.Count >= _batchSize)
            {
                batches.Add(currentBatch.ToArray());
                currentBatch.Clear();
            }
        }

        if (currentBatch.Count > 0)
            batches.Add(currentBatch.ToArray());

        return batches;
    }
}
