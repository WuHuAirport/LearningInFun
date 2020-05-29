using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTrigger : MonoBehaviour
{
    //[SerializeField]
    private float reactionSeconds;//反应秒数
    [SerializeField]
    private float timer;
    private bool startRecord;

    void Start()
    {
        reactionSeconds = GameManager.Instance.reactionSeconds;
        timer = reactionSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if(startRecord&&timer>=0)
        {
            timer -= Time.deltaTime;
        }
        JudgeTimerIsOver();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag=="Ballon")
        {
            startRecord = true;
            GameManager.Instance.canHit = true;
            collision.GetComponent<Ballon>().canRise = false;
            Debug.Log("can hit");
        }
    }

    public void JudgeTimerIsOver()
    {
        if (timer < 0)
        {
            startRecord = false;
            timer = reactionSeconds;
            GameManager.Instance.canHit = false;
            GameManager.Instance.AfterHitUpdate(false, null,false);
            Debug.Log("choose make");
            
        }
    }

    public void ResetTimer()
    {
        startRecord = false;
        timer = reactionSeconds;
    }

    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    //if(timer>1) Debug.Log("bigger than 1");
    //    Debug.Log("on"+timer+"and"+reactionSeconds);
    //    if (timer<0f)
    //    {
    //        //Debug.Log(timer+"and"+reactionSeconds);
    //        if (collision.tag == "Ballon")
    //        {
    //            //collision.GetComponent<Ballon>().canRise = true;
    //            GameManager.Instance.AfterHitUpdate(false, null);
    //            //GameManager.Instance.CreateNewBallon();
    //            startRecord = false;
    //            timer = 0;
    //        }
    //    }

    //}
}
