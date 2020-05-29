using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.Windows.Speech;

public class Speak : MonoBehaviour
{
    public string[] keyWords = new string[] { "确认", "开始", "返回", "暂停" };
    public ConfidenceLevel confidenLevel = ConfidenceLevel.Medium;
    PhraseRecognizer recognizer;
    void Start()
    {
        recognizer = new KeywordRecognizer(keyWords, confidenLevel);
        recognizer.OnPhraseRecognized += Display;  // 注册事件  
        recognizer.Start();
    }

    public void Display(PhraseRecognizedEventArgs args)
    {
        string str = args.text;
        Debug.Log(str.ToString());
    }
}  

