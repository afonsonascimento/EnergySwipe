using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Levels _levelData;

    [SerializeField] private List<Transform> _objectPossiblePositions;

    [SerializeField] private GameObject _energyPrefab;

    [SerializeField] private GameObject _lampPrefab;

    [SerializeField] private List<EnergyController> _levelEnergyControllers;

    public Levels GetLevelData()
    {
        return _levelData;
    }
    
    public void GenerateLevel(int _levelToGenerate)
    {
        Level level = _levelData.GetLevels()[_levelToGenerate - 1];
        List<int> parcelNumbers = level.GetLevelParcels();
        List<bool> hasEnergyObjects = level.GetEnergyBools();

        for (int i = 0; i < level.GetLevelParcels().Count; i++){
            if (hasEnergyObjects[i]){
                GameObject energy = Instantiate(_energyPrefab, _objectPossiblePositions[parcelNumbers[i]]);
                _levelEnergyControllers.Add(energy.GetComponent<EnergyController>());
            } else{
                GameObject energy = Instantiate(_lampPrefab, _objectPossiblePositions[parcelNumbers[i]]);
                _levelEnergyControllers.Add(energy.GetComponent<EnergyController>());
            }
            
        }
    }

    public void GenerateNewLevel()
    {
        
    }

    public bool CheckLevelCompleted()
    {
        for (var i = 0; i < _levelEnergyControllers.Count; i++){
            if (!_levelEnergyControllers[i].GetConnectedStatus()){
                return false;
            }
        }
        return true;
    }
}
