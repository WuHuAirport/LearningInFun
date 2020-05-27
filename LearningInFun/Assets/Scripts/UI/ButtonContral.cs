using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContral : MonoBehaviour
{
    ThesauruslsSetUI TSU;//当前场景UI管理器
    Text text;//按钮上的文字
    Text tipText;//提示框上的文字
    private void Start()
    {
        TSU = GameObject.Find("Scroll View").GetComponent<ThesauruslsSetUI>();
        tipText = GameObject.Find("TipsText").GetComponent<Text>();
        text = GetComponentInChildren<Text>();
    }

    public void SetChooseWord()
    {
        TSU.chooingWord = text.text;
        tipText.text = text.text;
    }
}
