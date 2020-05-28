using System.Collections;
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

public class GameManager : MonoBaseManager<GameManager>
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

    private string[] tempWords = { "apple", "peach", "banana", "pear" };

    //private float gameTime=60.0f;

    //GameSource
    public GameObject ballonPrefab;
    public GameObject rightParticle;
    public GameObject errorParticle;
    void Start()
    {
        
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
                BallonHitGameUpdate();
                break;
        }
    }

    public void BallonHitGameUpdate()
    {
        if (curTimes >= times)
        {
            GameEnd();
        }
        else
        {
            timer += Time.deltaTime;
            if (canRiseNewBallons)
            {
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
                rightBallonIndex = Random.Range(0, 3);
                curWordString = tempWords[rightBallonIndex];
                Debug.Log("curWord:"+curWordString);
                ballonsList[rightBallonIndex].GetComponent<Ballon>().wordString = curWordString;
                for (int i=0;i<4;i++)
                {
                    if(i!=rightBallonIndex)
                    {
                        Debug.Log(i+"Word:" + tempWords[i]);
                        ballonsList[i].GetComponent<Ballon>().wordString = tempWords[i];
                        //Debug.Log(i + "ballon:" + tempWords[i]);
                    }
                }
                canRiseNewBallons = false;
            }
                
        }

    }

    //分数更新
    public void AfterHitUpdate(bool isRright,GameObject hittedBallon)
    {
        //如果正确，更新分数
        if(isRright)
        {
            curRightNum++;
            curScore= curRightNum*5;
            scoreText.text = ("Score:" + curScore) as string;
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
