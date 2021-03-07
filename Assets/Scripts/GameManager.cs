using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    private int _currentLevel = 0;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        _currentLevel = PlayerPrefs.GetInt("Level", 0);
        InitiateLevel(_currentLevel);
    }

    /// <summary>
    /// Generates level based on saved data
    /// </summary>
    private void InitiateLevel(int level)
    {
        _levelController.PopulateLevel(level);
    }

    /// <summary>
    /// Checks if next level is unlocked 
    /// </summary>
    public bool CheckNextLevelUnlockState()
    {
        return _levelController.IsNextLevelUnlocked();
    }

    /// <summary>
    /// Checks if a previous level is available
    /// </summary>
    /// <returns></returns>
    public bool CheckIfThereIsAPreviousLevel()
    {
        return _levelController.IsPreviousLevelAvailable();
    }

    /// <summary>
    /// Instructs level controller to go to next level 
    /// </summary>
    public void MoveToNextLevel()
    {
        _levelController.GoToNextLevel();
    }

    /// <summary>
    /// Instructs level controller to go to previous level 
    /// </summary>
    public void MoveToPreviousLevel()
    {
        _levelController.GoToPreviousLevel();
    }
    
    /// <summary>
    /// Saves current level on player prefs when the game is closed
    /// </summary>
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", _levelController.GetCurrentLevel());
    }

    /// <summary>
    /// Returns current level
    /// </summary>
    public int GetCurrentLevel()
    {
        return _levelController.GetCurrentLevel();
    }
}
