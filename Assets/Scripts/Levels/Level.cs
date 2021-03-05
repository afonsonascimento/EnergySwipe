using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField]
    private List<int> _parcelNumbers;
    [SerializeField]
    private List<bool> _checkParcelEnergy;

    public List<int> GetLevelParcels()
    {
        return _parcelNumbers;
    }

    public List<bool> GetEnergyBools()
    {
        return _checkParcelEnergy;
    }

    public void SetLevelParcelNumbersList(List<int> _parcelNumbers)
    {
        _parcelNumbers = _parcelNumbers;
    }

    public void SetLevelEnergyBoolList(List<bool> _bools)
    {
        _checkParcelEnergy = _bools;
    }
}
