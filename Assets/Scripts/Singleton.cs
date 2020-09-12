using System;
using System.Reflection;
using UnityEngine;

// Thread Safe Singleton
public class Singleton<T> where T : class
{
    private static object _syncobj = new object();
    private static volatile T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                CreateInstance();
            }
            return _instance;
        }
    }
    private static void CreateInstance()
    {
        lock (_syncobj)
        {
            if (_instance == null)
            {
                Type t = typeof(T);

                ConstructorInfo[] ctors = t.GetConstructors();
                if (ctors.Length > 0)
                {
                    throw new InvalidOperationException($"{t.Name} : impossible to enforce singleton behaviour");
                }
                _instance = (T)Activator.CreateInstance(t, true);
            }
        }
    }
}

//MonoBehavior Singleton
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj;

                obj = GameObject.Find(typeof(T).Name);

                if (obj == null)
                {
                    obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }

                else
                {
                    instance = obj.GetComponent<T>();
                }

            }
            return instance;
        }
    }

    public void Start()
    {
        
    }
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}