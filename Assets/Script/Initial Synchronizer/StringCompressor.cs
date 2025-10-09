using System;
using System.IO;
using System.IO.Compression;
using System.Text;


// 文字列の圧縮・展開を行うクラス
// gzip + base64 を使用して圧縮
public static class StringCompressor
{
    // 単一文字列の圧縮
    public static string Compress(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        byte[] rawData = Encoding.UTF8.GetBytes(input);
        using (var output = new MemoryStream())
        {
            using (var gzip = new GZipStream(output, CompressionMode.Compress))
            {
                gzip.Write(rawData, 0, rawData.Length);
            }
            return Convert.ToBase64String(output.ToArray());
        }
    }

    // 単一文字列の展開
    public static string Decompress(string compressed)
    {
        if (string.IsNullOrEmpty(compressed)) return compressed;

        byte[] compressedData = Convert.FromBase64String(compressed);
        using (var input = new MemoryStream(compressedData))
        using (var gzip = new GZipStream(input, CompressionMode.Decompress))
        using (var reader = new StreamReader(gzip, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    // 複数データ（バッチ）の圧縮
    public static string CompressBatch(string[] dataList)
    {
        if (dataList == null || dataList.Length == 0) return string.Empty;

        // 区切り文字で連結して圧縮 (例: 特殊記号で分割可能にする)
        string joined = string.Join("\u001F", dataList); // 0x1F (Unit Separator)
        return Compress(joined);
    }

    // 複数データ（バッチ）の展開
    public static string[] DecompressBatch(string compressedBatch)
    {
        if (string.IsNullOrEmpty(compressedBatch)) return Array.Empty<string>();

        string decompressed = Decompress(compressedBatch);
        return decompressed.Split(new[] { "\u001F" }, StringSplitOptions.None);
    }
}
