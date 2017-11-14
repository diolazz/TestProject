using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.Log(name + ": error: already initialized", this);
            Destroy(gameObject);
        }

        _instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
}
