using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading;

public class SpeechTest : MonoBehaviour
{

    Thread t;
    SpVoice spVoice;
    string DefaultEnglishLangID = "804";//中文 409：英文

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            t = null;
            SpeakVoice("哈哈，正义必胜，hello world");
        }
    }

    public void SpeakVoice(string content)
    {
        try
        {
            if (t == null)
            {
                t = new Thread(() =>
                {
                    string contentStr = "<voice required=\"Language=" + DefaultEnglishLangID + "\">" + content + "</voice>";

                    if (spVoice == null)
                    {
                        spVoice = new SpVoice();
                        //spVoice.Voice = spVoice.GetVoices(string.Empty, string.Empty).Item(0);
                        spVoice.Speak(contentStr, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak | SpeechVoiceSpeakFlags.SVSFlagsAsync);
                    }
                    else
                    {
                        spVoice.Speak(contentStr, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak | SpeechVoiceSpeakFlags.SVSFlagsAsync);
                    }
                });
            }
            t.Start();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}

