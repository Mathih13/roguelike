using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : UnityEngine.Object
{
    public static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }
}
