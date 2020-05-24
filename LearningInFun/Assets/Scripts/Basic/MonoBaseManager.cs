using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBaseManager<T> : MonoBehaviour where T : MonoBaseManager<T>
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
