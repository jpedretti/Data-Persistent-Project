using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowHighScore();
        ShowCurrentScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void ShowHighScore()
    {
        var highScore = GameController.Instance.HighScore;
        if (highScore != null)
        {
            BestScoreText.text = $"Best Score : {highScore.UserName} : {highScore.Value}";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameController.Instance.Restart();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ShowCurrentScore();
    }

    private void ShowCurrentScore()
    {
        ScoreText.text = $"{GameController.Instance.CurrentPlayerName} - Score : {m_Points}";
    }

    public void GameOver()
    {
        GameController.Instance.SaveScore(m_Points);
        ShowHighScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void GoToHighScore() => GameController.Instance.GoToHighScore();

    public void GoToMenu() => GameController.Instance.GoToMenu();
}
