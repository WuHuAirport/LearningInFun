using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ThesauruslsSetUI : MonoBehaviour
{
    public string chooingWord;//选中的单词
    public string inputWord;//输入框内容
    private IOMgr thesaurusls;//词库
    private ScrollViewContral SVC;//滚动条
    bool needLoad = false;//true：需要刷新滚动条
    InputField inputField;//输入框
    private void Start()
    {
        thesaurusls = BaseManager<IOMgr>.GetInstance();
        SVC = GetComponentInChildren<ScrollViewContral>();
        LoadThesaurusls(inputWord);

        //监听输入框输入
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        inputField.GetComponent<InputField>().onEndEdit.AddListener(EndEdit);
    }

    /// <summary>
    /// 当焦点离开输入框时设置输入框内容并刷新滚动条
    /// </summary>
    /// <param name="str">输入框内容</param>
    void EndEdit(string str)
    {
        inputWord = str;
        SVC.ClearWordButton();
        needLoad = true;
    }

    /// <summary>
    /// 当不在清理滚动条且需要刷新滚动条时根据输入框刷新滚动条
    /// </summary>
    void Update()
    {
        if (!SVC.isClearing && needLoad)
        {
            LoadThesaurusls(inputWord);
            needLoad = false;
        }
    }

    /// <summary>
    /// 点击添加单词按钮时添加输入框中的单词到词库并刷新滚动条
    /// </summary>
    public void AddWord()
    {
        if(Regex.IsMatch(inputWord, "^[a-z]*"))
        {
            thesaurusls.StorageWord(inputWord);
            SVC.ClearWordButton();
            needLoad = true;
        }
        
    }
    
    /// <summary>
    /// 点击删除按钮时删除选择的单词
    /// </summary>
    public void DeleteWord()
    {
        if (chooingWord != null)
        {
            thesaurusls.DeleteWord(chooingWord);
            chooingWord = null;
        }
        SVC.ClearWordButton();
        needLoad = true;
    }

    /// <summary>
    /// 将当前词库保存到硬盘中并刷新滚动条
    /// </summary>
    public void FinishWork()
    {
        thesaurusls.SaveThesaurus();
        SVC.ClearWordButton();
        needLoad = true;
    }

    //public void BackHome()
    //{
    //    BackSence();
    //}

    /// <summary>
    /// 根据生成正则生成滚动条
    /// </summary>
    /// <param name="rx">生成正则</param>
    public void LoadThesaurusls(string rx = null)
    {
        List<string> rxList;
        rxList = thesaurusls.SearchWords(rx);
        foreach (string item in rxList)
        {
            SVC.BuildWordButton(item);
        }
    }

}
