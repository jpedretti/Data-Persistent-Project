using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _userName;

    // Update is called once per frame
    void Update()
    {
        _startButton.interactable = !string.IsNullOrWhiteSpace(_userName.text);
    }

    public void StartGame()
    {
        GameController.Instance.SetUserName(_userName.text);
        SceneManager.LoadScene(1);
    }
}
