using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_InputField _playerNameInputField;

    public void Start()
    {
        if (GameController.Instance.LastPlayerName != null)
        {
            _playerNameInputField.text = GameController.Instance.LastPlayerName;
        }
    }

    // Update is called once per frame
    void Update() => _startButton.interactable = !string.IsNullOrWhiteSpace(_playerNameInputField.text);

    public void StartGame()
    {
        GameController.Instance.SetUserName(_playerNameInputField.text);
        SceneManager.LoadScene(1);
    }
}
