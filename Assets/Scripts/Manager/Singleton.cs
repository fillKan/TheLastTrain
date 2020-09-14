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
/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }


    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}