using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database _instance;

    public static Database Instance
    {
        get
        {
            if (_instance == null) return null;
            return _instance;
        }
    }
    public int Hp = 3;
    public int Stage = 0;
    public int Score = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }

    public void Reset()
    { 
        Hp = 3; 
        Stage = 0; 
        Score = 0;
    }
    

}