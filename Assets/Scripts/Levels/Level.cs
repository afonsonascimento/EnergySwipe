using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField] private bool _isUnlocked;
    
    [SerializeField] private List<int> _parcelNumbers;
    
    [SerializeField] private List<bool> _checkParcelEnergy;

    public List<int> GetLevelParcels()
    {
        return _parcelNumbers;
    }

    public List<bool> GetEnergyBooleans()
    {
        return _checkParcelEnergy;
    }

    /// <summary>
    /// On level creation, these numbers represent location positions
    /// </summary>
    public void SetLevelParcelNumbersList(List<int> _numbers)
    {
        _parcelNumbers = _numbers;
    }

    /// <summary>
    /// On level creation, these booleans represent objects that have energy 
    /// </summary>
    public void SetLevelEnergyBoolList(List<bool> _bools)
    {
        _checkParcelEnergy = _bools;
    }

    /// <summary>
    /// Returns unlocked status
    /// </summary>
    public bool GetUnlockedStatus()
    {
        return _isUnlocked;
    }

    /// <summary>
    /// Unlocks this level
    /// </summary>
    public void UnlockLevel()
    {
        _isUnlocked = true;
    }
}
