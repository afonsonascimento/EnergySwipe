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
    [SerializeField] private GameObject _closeButton;

    private bool _levelSelectionOpened;
    
    
    /// <summary>
    /// Called by UI menu button pressed, activates level selection
    /// </summary>
    public void MenuButtonPressed()
    {
        if (!_levelSelectionOpened){
            _levelSelectionOpened = true;
            _levelNumberText.enabled = true;
            _nextButton.gameObject.SetActive(true);
            _previousButton.gameObject.SetActive(true);
            UpdateLevelSelectionUI();
            _closeButton.SetActive(true);
        } else{
            CloseLevelSelection();
        }
    }

    /// <summary>
    /// Updates level selection buttons and texts
    /// </summary>
    private void UpdateLevelSelectionUI()
    {
        GetCurrentLevel();
        CheckNextLevelButtonStatus();
        CheckPreviousLevelButtonStatus();
    }

    private void GetCurrentLevel()
    {
        _levelNumberText.text = "#" + _gameManager.GetCurrentLevel();
    }

    /// <summary>
    /// Selects next level
    /// </summary>
    public void NextLevel()
    {
        _gameManager.MoveToNextLevel();
        UpdateLevelSelectionUI();
    }

    /// <summary>
    /// Selects previous level
    /// </summary>
    public void PreviousLevel()
    {
        _gameManager.MoveToPreviousLevel();
        UpdateLevelSelectionUI();
    }

    /// <summary>
    /// Checks if the button should be interactable or not, depending on the current level 
    /// </summary>
    private void CheckNextLevelButtonStatus()
    {
        _nextButton.interactable = _gameManager.CheckNextLevelUnlockState();
    }
    
    /// <summary>
    /// Checks if the button should be interactable or not, depending on the current level 
    /// </summary>
    private void CheckPreviousLevelButtonStatus()
    {
        _previousButton.interactable = _gameManager.CheckIfThereIsAPreviousLevel();
    }

    /// <summary>
    /// Closes level selection
    /// </summary>
    public void CloseLevelSelection()
    {
        _levelSelectionOpened = false;
        _levelNumberText.enabled = false;
        _nextButton.gameObject.SetActive(false);
        _previousButton.gameObject.SetActive(false);
        _closeButton.SetActive(false);
    }
}
