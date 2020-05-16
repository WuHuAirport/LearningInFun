using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//使用方法：需要单例的直接继承即可
public class BaseManager<T> where T:new ()
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
