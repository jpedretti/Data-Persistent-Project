using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [System.Serializable]
    class Gamedata
    {
        public List<Score> Scores;
        public string LastPlayerName;
    }

    [System.Serializable]
    public class Score
    {
        public string UserName;
        public float Value;
    }

    public static GameController Instance { get; private set; }

    public string CurrentPlayerName { get; private set; }

    private Gamedata _gameData;
    public Score HighScore { get { return _gameData?.Scores?.FirstOrDefault(); } }

    public List<Score> HighScores { get { return _gameData?.Scores ?? new(); } }

    public string LastPlayerName { get { return _gameData?.LastPlayerName ?? ""; } }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            string path = GetPath();
            LoadGameData(path);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadGameData(string path)
    {
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            _gameData = JsonUtility.FromJson<Gamedata>(json);
        }
    }

    public void SetUserName(string userName)
    {
        CurrentPlayerName = userName;
    }

    public void SaveScore(float value)
    {
        string path = GetPath();
        if (_gameData != null)
        {
            _gameData.Scores.Add(new Score { UserName = CurrentPlayerName, Value = value });
            _gameData.Scores = _gameData.Scores.OrderByDescending(score => score.Value).ToList();
        }
        else
        {
            _gameData = new Gamedata
            {
                Scores = new List<Score>() {
                    new Score { UserName = CurrentPlayerName, Value=value }
                }
            };
        }
        _gameData.LastPlayerName = CurrentPlayerName;
        File.WriteAllText(GetPath(), JsonUtility.ToJson(_gameData));
    }

    private static string GetPath() => $"{Application.persistentDataPath} /savefile.json";

    public void Restart() => SceneManager.LoadScene(1);

    public void GoToMenu() => SceneManager.LoadScene(0);

    public void GoToHighScore() => SceneManager.LoadScene(2);

    public void ClearData()
    {
        if (File.Exists(GetPath()))
        {
            File.Delete(GetPath());
            _gameData = null;
        }
    }
}
