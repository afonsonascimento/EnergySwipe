using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField, Tooltip("Current unlocked levels")]
    private int _currentUnlockedLevels;

    [SerializeField, Tooltip("Level list")]
    private List<Level> _levels;


    public void AdLevel(Level level)
    {
        _levels.Add(level);
    }

    public List<Level> GetLevels()
    {
        return _levels;
    }
    
}
