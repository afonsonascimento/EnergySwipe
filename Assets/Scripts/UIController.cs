using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _menuButton;
    
    
    public void MenuButtonPressed()
    {
        _levelNumberText.enabled = true;
        
        CheckNextLevelButtonStatus();
        CheckPreviousLevelButtonStatus();
    }

    public void NextLevel()
    {
        
    }

    public void PreviousLevel()
    {
        
    }

    private void CheckNextLevelButtonStatus()
    {
        _nextButton.enabled = true;
    }
    
    private void CheckPreviousLevelButtonStatus()
    {
        _previousButton.enabled = true;
    }
}
