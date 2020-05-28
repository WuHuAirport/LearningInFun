using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordUsedUI : MonoBehaviour
{
    public string chooingWord;//选中的单词
    public string inputWord;//输入框内容
    private IOMgr IOMgr;//词库
    private WordScrollContral WSC;//滚动条
    private UsedScrollContral USC;
    bool needWLoad = false;//true：需要刷新滚动条
    bool needULoad = false;
    InputField inputField;//输入框
    UIPoint jumpToSence;
    private void Start()
    {
        IOMgr = BaseManager<IOMgr>.GetInstance();
        WSC = GameObject.Find("WordsView").GetComponentInChildren<WordScrollContral>();
        USC = GameObject.Find("UsedView").GetComponentInChildren<UsedScrollContral>();
        jumpToSence = GetComponent<UIPoint>();
        LoadThesaurusls(inputWord);
        LoadChooseWord();
        //监听输入框输入
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        inputField.GetComponent<InputField>().onEndEdit.AddListener(EndEdit);
        jumpToSence.jumpSceneName = SceneName.GameChooseScene;
    }

    /// <summary>
    /// 当焦点离开输入框时设置输入框内容并刷新滚动条
    /// </summary>
    /// <param name="str">输入框内容</param>
    void EndEdit(string str)
    {
        inputWord = str;
        WSC.ClearWordButton();
        needWLoad = true;
    }

    /// <summary>
    /// 当不在清理滚动条且需要刷新滚动条时根据输入框刷新滚动条
    /// </summary>
    void Update()
    {
        if (!WSC.isClearing && needWLoad)
        {
            LoadThesaurusls(inputWord);
            needWLoad = false;
        }
        if (!USC.isClearing && needULoad)
        {
            LoadChooseWord();
            needULoad = false;
        }
    }

    /// <summary>
    /// 点击添加单词按钮时添加输入框中的单词到词库并刷新滚动条
    /// </summary>
    public void AddWord()
    {
        if (chooingWord != null)
        {
            IOMgr.StorageCWord(chooingWord);
            chooingWord = null;
        }
        USC.ClearWordButton();
        needULoad = true;
    }

    /// <summary>
    /// 点击删除按钮时删除选择的单词
    /// </summary>
    public void DeleteWord(string deleteWord)
    {
        if (deleteWord != null)
        {
            IOMgr.DeleteCWord(deleteWord);
            deleteWord = null;
        }
        USC.ClearWordButton();
        needULoad = true;
    }

    /// <summary>
    /// 将当前词库保存到硬盘中并刷新滚动条
    /// </summary>
    public void FinishWork()
    {
        IOMgr.SaveChooseWord();
        USC.ClearWordButton();
        needULoad = true;
        if (IOMgr.GetChooseWord().Count != 0)
        {
            jumpToSence.JumpToScene();
        }
    }



    /// <summary>
    /// 根据生成正则生成滚动条
    /// </summary>
    /// <param name="rx">生成正则</param>
    public void LoadThesaurusls(string rx = null)
    {
        List<string> rxList;
        rxList = IOMgr.SearchWords(rx);
        foreach (string item in rxList)
        {
            WSC.BuildWordButton(item);
        }
    }

    public void LoadChooseWord()
    {
        foreach (string item in IOMgr.GetChooseWord())
        {
            USC.BuildWordButton(item);
        }
    }
}
