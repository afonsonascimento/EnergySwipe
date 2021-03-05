using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField, Tooltip("Line manager reference")]
    private LineManager _lineManager;
    
    [SerializeField] private Levels _levelData;

    [SerializeField] private List<Transform> _objectPossiblePositions;

    [SerializeField] private GameObject _energyPrefab;

    [SerializeField] private GameObject _lampPrefab;

    [SerializeField] private List<EnergyController> _levelEnergyControllers;

    [SerializeField] private int _percentageOfEnergies = 5;
    [SerializeField] private int _percentageOfLamps = 20;

    private int _currentLevel = 0;

    public Levels GetLevelData()
    {
        return _levelData;
    }
    
    /// <summary>
    /// Populates a level based on position and objects, from level data
    /// </summary>
    public void PopulateLevel(int _levelToGenerate)
    {
        _currentLevel = _levelToGenerate;
        
        Level level = _levelData.GetLevels()[_levelToGenerate];
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

    /// <summary>
    /// 
    /// </summary>
    public void CheckLevelCompletion()
    {
        if (CheckLevelCompleted()){
            GoToNextLevel();
        }
    }


    private void GoToNextLevel()
    {
        CleanLevel();
        _currentLevel += 1;
        if (_currentLevel < _levelData.GetLevels().Count){
            PopulateLevel(_currentLevel);
        } else{
            GenerateNewLevel();
            PopulateLevel(_currentLevel);
        }
        
    }

    /// <summary>
    /// Destroys previous level objects
    /// </summary>
    private void CleanLevel()
    {
        foreach (var energyController in _levelEnergyControllers){
            Destroy(energyController.gameObject);
        }
        
        _levelEnergyControllers.Clear();
        
        _lineManager.ClearLines();
    }

    /// <summary>
    /// Generates a new level based on some percentage rules described below, each level is saved in a scriptable object
    /// </summary>
    private void GenerateNewLevel()
    {
        List<bool> _levelEnergybools = new List<bool>();
        List<int> _levelPositionInts = new List<int>();

        //Creates mandatory energy component
        int _mandatoryEnergyInt = Random.Range(0, _objectPossiblePositions.Count);
        _levelPositionInts.Add(_mandatoryEnergyInt);
        _levelEnergybools.Add(true);

        //Creates mandatory lamp component
        int _mandatoryLampInt = Random.Range(0, _objectPossiblePositions.Count);
        while (_mandatoryLampInt == _mandatoryEnergyInt){
            _mandatoryLampInt = Random.Range(0, _objectPossiblePositions.Count);
        }
        _levelPositionInts.Add(_mandatoryLampInt);
        _levelEnergybools.Add(false);
        
        //Creates additional lamps and energies to fill the level
        foreach (var position in _objectPossiblePositions){

            if (position != _objectPossiblePositions[_mandatoryEnergyInt] && position != _objectPossiblePositions[_mandatoryLampInt]){
                int randomNumber = Random.Range(1, 100);

                if (randomNumber <= _percentageOfEnergies){
                    _levelPositionInts.Add(_objectPossiblePositions.IndexOf(position));
                    _levelEnergybools.Add(true);
                }else if (randomNumber <= _percentageOfLamps){
                    _levelPositionInts.Add(_objectPossiblePositions.IndexOf(position));
                    _levelEnergybools.Add(false);
                }
            }
        }

        //Save data into a scriptable objet
        Level newLevel = ScriptableObject.CreateInstance<Level>();
        newLevel.SetLevelEnergyBoolList(_levelEnergybools);
        newLevel.SetLevelParcelNumbersList(_levelPositionInts);
        int levelNumber = _levelData.GetLevels().Count;
        
        string path = "Assets/Scripts/Levels/";
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path  + typeof(Level) + " " + levelNumber + ".asset");
        AssetDatabase.CreateAsset (newLevel, assetPathAndName);
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = newLevel;
        
        
        _levelData.AdLevel(newLevel);
    }

    private bool CheckLevelCompleted()
    {
        for (var i = 0; i < _levelEnergyControllers.Count; i++){
            if (!_levelEnergyControllers[i].GetConnectedStatus()){
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns current level
    /// </summary>
    public int GetCurrentLevel()
    {
        return _currentLevel;
    }
}
