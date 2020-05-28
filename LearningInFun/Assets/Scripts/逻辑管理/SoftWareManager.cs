using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SceneName
{
    startScene,
    ThesaurusIsSetScene,    //词库设置场景
    WordUsedScene,          //单词选择场景
    GameChooseScene,        //游戏选择场景
    BallonHitGame           //打气球游戏场景

}
public class SoftWareManager : MonoBaseManager<SoftWareManager>
{
    //软件逻辑方面
    private SceneName curScene;
    private int sceneNumber;

    //游戏设置
    [SerializeField]
    private GameObject gameSettingPanel;
    //[SerializeField]
    //private Slider BKMusicSlider;//Slider 对象
    //[SerializeField]
    //private Slider SoundMusicSlider;
    private Animator panelAnim;

    void Start()
    {
        gameSettingPanel = GameObject.Find("GameSettingPanel");
        //BKMusicSlider = GameObject.Find("BKMusicSlider").GetComponent<Slider>();
        //SoundMusicSlider = GameObject.Find("SoundMusicSlider").GetComponent<Slider>();
        panelAnim = gameSettingPanel.GetComponent<Animator>();
    }

    //public void OpenGameSetting()
    //{
    //    Time.timeScale = 0;
    //    panelAnim.SetBool("isRise", false);
    //    panelAnim.SetTrigger("Move");
    //}

    //public void CloseGameSetting()
    //{
    //    Time.timeScale = 1f;
    //    panelAnim.SetBool("isRise", true);
    //}

    public void OnRestart()//点击“重新开始”时执行此方法
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }


    //public void BKMusicSetting()
    //{
    //    //Debug.Log(BKMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeValue(BKMusicSlider.value / 1);
    //}

    //public void SoundMusicSetting()
    //{
    //    //Debug.Log(SoundMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeSoundValue(SoundMusicSlider.value / 1);
    //}


    ////跳转到下个场景
    //public void NextScene()
    //{

    //}


    ////返回上个场景
    //public void BackScene()
    //{

    //}

    //跳转到特定场景
    public void  JumpToScene(SceneName sceneName)
    {
        Debug.Log("Scenes/"+sceneName.ToString());
        SceneMgr.GetInstance().LoadScene("Scenes/" + sceneName.ToString());

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
