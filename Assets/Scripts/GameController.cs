using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [System.Serializable]
    class Gamedata
    {
        public List<Score> Scores;
    }

    [System.Serializable]
    class Score
    {
        public string UserName;
        public float Value;
    }

    public static GameController Instance { get; private set; }

    public string UserName { get; private set; }

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserName(string userName)
    {
        Instance.UserName = userName;
    }

    public void SaveScore(string userName, float value)
    {

    }
}
