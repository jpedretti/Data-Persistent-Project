using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{

    [SerializeField] private GameObject _highScoreVerticalLayout;
    [SerializeField] private GameObject _highScorePrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var score in GameController.Instance.HighScores)
        {
            var highScoreComponent = Instantiate(_highScorePrefab, _highScoreVerticalLayout.transform);
            var texts = highScoreComponent.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = score.UserName;
            texts[1].text = score.Value.ToString();
        }
    }

    public void Restart() => GameController.Instance.Restart();

    public void GoToMenu() => GameController.Instance.GoToMenu();

    public void ClearData()
    {
        GameController.Instance.ClearData();
        GameController.Instance.GoToMenu();
    }
}
