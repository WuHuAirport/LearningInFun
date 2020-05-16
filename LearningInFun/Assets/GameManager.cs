using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneTypes
{
    startScene,

}

public class GameManager : Singleton<GameManager>
{
    //软件逻辑方面
    private int sceneNumber;


    //游戏方面
    private int wordsNumber; //每波单词数
    private int reactionSeconds;//反应秒数
    private int times;      //波数

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterWordUsedScene()
    {
        SceneMgr.GetInstance().LoadSceneAsyn("Scenes/WordUsedScene");
    }


    /// <summary>
    /// 退出整个软件
    /// </summary>
    public void QuitSoftware()
    {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        
#else
         Application.Quit();
#endif
    }
}
