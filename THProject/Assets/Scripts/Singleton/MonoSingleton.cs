using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T _instance = null;
    public static T Instance{
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    _instance = new GameObject("MonoSingleton"+typeof(T).ToString(),typeof(T)).GetComponent<T>();
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }
    
    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
