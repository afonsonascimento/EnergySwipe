using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private int _currentLevel = 0;

    private void Start()
    {
        //PlayerPrefs.SetInt("Level",0);
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
    
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", _levelController.GetCurrentLevel());
    }
}
