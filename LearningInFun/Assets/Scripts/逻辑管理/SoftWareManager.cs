using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneTypes
{
    startScene,
    ThesaurusIsSetScene,    //词库设置场景
    WordUsedScene,          //单词选择场景
    GameChooseScene,        //游戏选择场景
    BallonHitGame           //打气球游戏场景

}
public class SoftWareManager : MonoBehaviour
{
    //软件逻辑方面
    private SceneTypes curScene;
    private int sceneNumber;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //跳转到下个场景
    public void NextScene()
    {

    }


    //返回上个场景
    public void BackScene()
    {

    }

    //跳转到特定场景
    public void  JumpToScene()
    {

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
