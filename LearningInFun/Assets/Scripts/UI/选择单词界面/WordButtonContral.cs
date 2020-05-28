using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordButtonContral : MonoBehaviour
{
    WordUsedUI WUU;//当前场景UI管理器
    Text text;//按钮上的文字
    Text tipText;//提示框上的文字
    private void Start()
    {
        WUU = GameObject.Find("WUUIMgr").GetComponent<WordUsedUI>();
        tipText = GameObject.Find("TipsText").GetComponent<Text>();
        text = GetComponentInChildren<Text>();
    }

    public void SetChooseWord()
    {
        WUU.chooingWord = text.text;
        tipText.text = text.text;
    }
}
