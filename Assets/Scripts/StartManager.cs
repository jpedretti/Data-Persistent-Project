using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _highScoreButton;
    [SerializeField] private TMP_InputField _playerNameInputField;

    public void Start()
    {
        if (GameController.Instance.LastPlayerName != null)
        {
            _playerNameInputField.text = GameController.Instance.LastPlayerName;
        }
        if (GameController.Instance.HighScores.Count > 0)
        {
            _highScoreButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update() => _startButton.interactable = !string.IsNullOrWhiteSpace(_playerNameInputField.text);

    public void StartGame()
    {
        GameController.Instance.SetUserName(_playerNameInputField.text);
        SceneManager.LoadScene(1);
    }

    public void GoToHighScore() => GameController.Instance.GoToHighScore();

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
