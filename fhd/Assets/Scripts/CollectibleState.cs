using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleState : MonoBehaviour
{
    private static CollectibleState _instance;
    public static CollectibleState Instance
    {
        get { return _instance; }
    }
    public int collectablesCollected = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
