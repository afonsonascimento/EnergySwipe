using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private int _currentLevel = 1;

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 1);
        GenerateLevel(_currentLevel);
    }

    /// <summary>
    /// Generates level based on saved data
    /// </summary>
    private void GenerateLevel(int level)
    {
        _levelController.GenerateLevel(level);
    }
    
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", _levelController.GetLevelData().GetLevels().Count);
    }
}
