using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;

public class IOMgr :BaseManager<IOMgr>
{

    const string fileName = "/thesaursSave.save";//存储文件名

    ThesaursSave thesaursSave;//存储结构

    /// <summary>
    /// 构造函数，初始化词库和载入词库
    /// </summary>
    public IOMgr()
    {
        thesaursSave = new ThesaursSave();
        LoadThesaurus();
    }

    /// <summary>
    /// 添加单词到词库中
    /// </summary>
    /// <param name="targetWord">添加的单词</param>
    /// <returns>是否执行了添加操作，若单词存在则返回false</returns>
    public bool StorageWord(string targetWord)
    {
        targetWord = targetWord.ToLower();
        if (!SearchWord(targetWord))
        {
            thesaursSave.thesaurus.Add(targetWord);
            thesaursSave.thesaurus.Sort();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 删除词库中的一个单词
    /// </summary>
    /// <param name="targetWord">删除的单词</param>
    /// <returns>是否执行了删除操作，若单词不存在则返回false</returns>
    public bool DeleteWord(string targetWord)
    {
        targetWord = targetWord.ToLower();
        if (SearchWord(targetWord))
        {
            thesaursSave.thesaurus.Remove(targetWord);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 清空词库
    /// </summary>
    /// <returns>成功清空</returns>
    public bool DeleteAllWords()
    {
        if (thesaursSave.thesaurus.Count != 0)
        {
            thesaursSave.thesaurus.Clear();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 查找词库中的一个单词
    /// </summary>
    /// <param name="targetWord">目标单词</param>
    /// <returns>是否存在该单词</returns>
    public bool SearchWord(string targetWord)
    {
        targetWord = targetWord.ToLower();
        if (thesaursSave.thesaurus.IndexOf(targetWord) < 0)
            return false;
        return true;
    }

    /// <summary>
    /// 正则查找单词
    /// </summary>
    /// <param name="targetWord">目标模式</param>
    /// <returns>查找到的所有单词</returns>
    public List<string> SearchWords(string targetWord)
    {
        targetWord = targetWord.ToLower();
        List<string> targetWords = new List<string>();
        string rx = "^" + targetWord + "[a-z]*";
        targetWords = thesaursSave.thesaurus.FindAll(
            delegate (string s)
            {
                return Regex.IsMatch(s, rx);
            });
        return targetWords;
    }

    /// <summary>
    /// 将词库保存到硬盘上
    /// </summary>
    public void SaveThesaurus()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, thesaursSave);
        file.Close();
    }

    /// <summary>
    /// 读取硬盘上的词库
    /// </summary>
    public void LoadThesaurus()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            thesaursSave = (ThesaursSave)bf.Deserialize(file);
            file.Close();
        }
    }

}
