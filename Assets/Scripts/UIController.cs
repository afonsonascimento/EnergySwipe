using DG.Tweening;
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
    [SerializeField] private GameObject _gameFrame;
    [SerializeField] private GameObject _lineHolder;

    private bool _levelSelectionOpened;
    
    
    /// <summary>
    /// Called by UI menu button pressed, activates level selection
    /// </summary>
    public void MenuButtonPressed()
    {
        if (!_levelSelectionOpened){
            _levelSelectionOpened = true;
            
            //Menu button anims
            _menuButton.gameObject.transform.DORotate(new Vector3(0, 0, -180), 0.25f);
            _menuButton.gameObject.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.25f);
            
            //Game frame anims
            _gameFrame.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.25f);

            //Level selection anims
            _levelNumberText.gameObject.transform.DOLocalMove(Vector3.zero, 0.25f);
            _nextButton.gameObject.transform.DOLocalMove(new Vector3(150, 0, 0), 0.25f);
            _previousButton.gameObject.transform.DOLocalMove(new Vector3(-150, 0, 0), 0.25f);
            UpdateLevelSelectionUI();
            
            //Line holder anims
            _lineHolder.transform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.25f);
            _lineHolder.transform.DOLocalMove(new Vector3(0, 0.3f, 0), 0.25f);
            
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
        
        //Level selection anims
        _levelNumberText.gameObject.transform.DOLocalMove(new Vector3(0, 250,0), 0.25f);
        _nextButton.gameObject.transform.DOLocalMove(new Vector3(150, 250, 0), 0.25f);
        _previousButton.gameObject.transform.DOLocalMove(new Vector3(-150, 250, 0), 0.25f);
        
        _closeButton.SetActive(false);
        
        //menu button anims
        _menuButton.gameObject.transform.DOScale(Vector3.one, 0.25f);
        _menuButton.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.25f);
        
        //game frame anim
        _gameFrame.transform.DOScale(Vector3.one, 0.25f);
        
        //Line holder anims
        _lineHolder.transform.DOScale(new Vector3(1f, 1f, 1), 0.25f);
        _lineHolder.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f);
    }
}
