using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPoint : MonoBehaviour
{
    public SceneName jumpSceneName; //设置想跳转的场景

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpToScene()
    {
        SoftWareManager.Instance.JumpToScene(jumpSceneName);
    }

    public void QuitSoftware()
    {
        SoftWareManager.Instance.QuitSoftware();
    }
}
