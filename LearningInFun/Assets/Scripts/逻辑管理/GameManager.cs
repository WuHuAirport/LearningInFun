﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

public class GameManager : MonoBehaviour
{
    public GameType curGame;

    //游戏方面
    public float leftBorder;
    public float rightBorder;
    public float highBorder;
    public float lowBorder;

    public Text scoreText;
    public Text timesText;

    public Text endScoreText;
    public Text endRightHitText;
    public Text endTimesText;
    public GameObject gamePanel;
    public GameObject gameSetting;
    public GameObject gameOver;
    private GameObject ChooseTrigger;


    //GameSetting
    private int wordsNumber=4; //每波单词数
    public float reactionSeconds=5;//反应秒数
    private int times=5;      //波数

    //record

    private float timer;              //计时器
    private int curWordNumber=0;      //当前单词数
    [SerializeField]
    private int curTimes=0;           //当前波数
    private int curScore = 0;
    private int curRightNum = 0;

    public bool canHit = false;             //可以射击气球
    private bool canRiseNewBallons=true;     //可以再次生成气球
    [SerializeField]
    private GameObject[] ballonsList=new GameObject[4]; //保留创建的ballon

    public string curWordString;            //当前语音读的单词
    private int rightBallonIndex;           //对的单词所放的气球序号
    private string otherWordString;

    [SerializeField]
    private List<string> choseWordList = new List<string>();
    private List<string> tempWords = new List<string> { "apple", "peach", "banana", "pear","orange", "grape" };

    //private float gameTime=60.0f;

    //GameSource
    public GameObject ballonPrefab;
    public GameObject rightParticle;
    public GameObject errorParticle;

    public static GameManager Instance { get; private set; }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (GameManager)this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        choseWordList = IOMgr.GetInstance().GetChooseWord();
        //choseWordList = tempWords;
    }

    // Update is called once per frame
    void Update()
    {
        GameUpdate();
    }


    public void GameUpdate()
    {
        switch(curGame)
        {
            case GameType.BallonHit:
                if (SoftWareManager.Instance.curScene == SceneName.BallonHitGame)
                {
                    //Debug.Log("hit game");
                    ChooseTrigger = GameObject.Find("ChooseTrigger");
                    BallonHitGameUpdate();
                }
                    
                break;
        }
    }

    public void BallonHitGameUpdate()
    {
        if (curTimes == times&& !canHit)
        {
            Invoke("GameEnd",5.0f);
        }
        else
        {
            //timer += Time.deltaTime;
            if (canRiseNewBallons)
            {
                canRiseNewBallons = false;
                Debug.Log("create ballon");
                curTimes++;
                timesText.text = ("Times:" + curTimes) as string;
                //创建气球
                for (int i = 0; i < 4; i++)
                {
                    ballonsList[i] = Instantiate(ballonPrefab, new Vector3(((rightBorder - leftBorder) / 4) * (i+1) + leftBorder, lowBorder, 0), Quaternion.identity);
                }
                //ballonsList[0] = Instantiate(ballonPrefab, new Vector3(((rightBorder - leftBorder) / 4) * 1 + leftBorder, lowBorder, 0), Quaternion.identity);

                //分配气球上的单词
                curWordString = choseWordList[Random.Range(0, choseWordList.Count)];

                rightBallonIndex = Random.Range(0, 3);
                //Debug.Log("right index:" + rightBallonIndex);
                MusicMgr.GetInstance().SpeakVoice(curWordString);

                //curWordString = tempWords[rightBallonIndex];
                //Debug.Log("curWord:"+curWordString);
                ballonsList[rightBallonIndex].GetComponent<Ballon>().wordString = curWordString;
                for (int i=0;i<4;i++)
                {
                    if(i!=rightBallonIndex)
                    {
                        otherWordString = choseWordList[Random.Range(0, choseWordList.Count)];
                        while (otherWordString== curWordString)
                        {
                            otherWordString = choseWordList[Random.Range(0, choseWordList.Count)];
                        }
                        //Debug.Log(i+"Word:" + tempWords[i]);
                        ballonsList[i].GetComponent<Ballon>().wordString = otherWordString;
                        //Debug.Log(i + "ballon:" + tempWords[i]);
                    }
                }
            }
                
        }

    }

    //分数更新
    public void AfterHitUpdate(bool isRright,GameObject hittedBallon,bool hasHit=true)
    {
        if(hasHit)
        {
            ChooseTrigger.GetComponent<ChooseTrigger>().ResetTimer();
        }
        //如果正确，更新分数
        if(isRright)
        {
            curRightNum++;
            curScore= curRightNum*5;
            scoreText.text = ("Score:" + curScore) as string;
        }
        else
        {
            ballonsList[rightBallonIndex].GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }


        
        for (int i = 0; i < 4; i++)
        {
            if (ballonsList[i] != hittedBallon)
            {
                //Debug.Log(i + "Word:" + tempWords[i]);
                ballonsList[i].GetComponent<Ballon>().canRise = true;
                //Debug.Log(i + "ballon:" + tempWords[i]);
            }
        }
        CreateNewBallon();
    }

    public void CreateNewBallon()
    {
        canRiseNewBallons = true;
    }

    //游戏设置
    public void GameSetting()
    {
        gamePanel.SetActive(true);
        gameSetting.SetActive(true);

    }

    public void GameEnd()
    {
        gamePanel.SetActive(true);
        gameOver.SetActive(true);
        endScoreText.text = ("Score:" + curScore) as string;
        endRightHitText.text = ("Right Hit:" + curRightNum) as string;
        endTimesText.text = ("Times:" + times) as string;
    }

}
