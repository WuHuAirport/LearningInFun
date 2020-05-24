using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//游戏难度等级
public enum DifficultyLevel
{
    level1=1,
    level2,
    level3
}

//游戏类型
public enum GameType
{
    BallonHit//打气球
}

public class GameManager : MonoBaseManager<GameManager>
{
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

    //游戏设置
    public void GameSetting()
    {

    }

    //分数更新
    public void ScoreUpdate()
    {

    }



}
