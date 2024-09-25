using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// 试着写的一个加密代码可以选择使用或是删除
/// </summary>
public class JsonEncryption
{
    // 密钥应为32字节长度（256位）
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("这是一个中文的Key");
    // 初始化向量必须为16字节长度（128位）
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("这是一个加密向量");
    //加密Json字符串
    public static string EncryptJson(string json)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(json);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    //解密Json字符串
    public static string DecryptJson(string encryptedJson)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedJson)))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
    //保存加密后的Json文件(自动加密)
    public static void SaveEncryptedJsonToFile(string path, string json)
    {
        string encryptedJson = EncryptJson(json);
        File.WriteAllText(path, encryptedJson);
    }
    //加载加密后的Json文件(自动解密)
    public static string LoadEncryptedJsonFromFile(string path)
    {
        string encryptedJson = File.ReadAllText(path);
        return DecryptJson(encryptedJson);
    }
}