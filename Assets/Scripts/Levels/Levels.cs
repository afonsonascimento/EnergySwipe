using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField, Tooltip("Level list")]
    private List<Level> _levels;


    /// <summary>
    /// Ads a level to the current level list
    /// </summary>
    public void AdLevel(Level level)
    {
        _levels.Add(level);
    }

    /// <summary>
    /// Returns level list
    /// </summary>
    public List<Level> GetLevels()
    {
        return _levels;
    }
    
}
