using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField, Tooltip("Level Controller reference")]
    private LevelController _levelController;

    [SerializeField, Tooltip("Audio manager reference")]
    private AudioManager _audioManager;
    
    [SerializeField, Tooltip("Line parent reference")]
    private Transform _lineParentTransform;
    
    private LineRenderer line;
    private Vector3 mousePos;
    public Material _lineMaterial;
    private int currLines = 0;
    
    private EnergyController _energyObjectClicked;
    private EnergyController _energyDisabledObject;
    
    private List<LineRenderer> _spawnedLines = new List<LineRenderer>();
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){ 

            if (!_energyObjectClicked){
                return;
            }
            if (line == null){
                CreateLine();
            }
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0, _energyObjectClicked.transform.position);
            line.SetPosition(1, mousePos);
            _audioManager.EnergyClicked();
        } else if (Input.GetMouseButtonUp(0) && line){
            if (_energyDisabledObject){
                _energyDisabledObject.EnableEnergy();
                _energyObjectClicked.EnableEnergy();
                line.SetPosition(0, _energyObjectClicked.transform.position);
                line.SetPosition(1, _energyDisabledObject.transform.position);
                _spawnedLines.Add(line);
                line = null;
                currLines++;
                RemoveEnergyDisabledObject();
                _levelController.CheckLevelCompletion();
                _audioManager.LineConnected();

            } else{
                Destroy(line.gameObject);
            }
            RemoveEnergyClickedObject();
            
        } else if (Input.GetMouseButton(0) && line){
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0,  _energyObjectClicked.transform.position);
            line.SetPosition(1, mousePos);
            MMVibrationManager.Haptic(HapticTypes.Selection,true,true,this);
        }
    }

    /// <summary>
    /// Creates a line renderer 
    /// </summary>
    private void CreateLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.gameObject.transform.SetParent(_lineParentTransform);
        line.material = _lineMaterial;
        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
        line.sortingOrder = 1;
    }

    /// <summary>
    /// Sets energy object clicked for line positioning
    /// </summary>
    public void EnergyObjectClicked(EnergyController _objectClicked)
    {
        _energyObjectClicked = _objectClicked;
    }

    /// <summary>
    /// Sets energy disabled object for line positioning
    /// </summary>
    public void SetEnergyDisabledObject(EnergyController _energyObject)
    {
        if (!_energyObjectClicked){
            return;
        }
        if (_energyObject != _energyObjectClicked){
            _energyDisabledObject = _energyObject;
        }
    }

    /// <summary>
    /// Removes energy disabled object
    /// </summary>
    public void RemoveEnergyDisabledObject()
    {
        _energyDisabledObject = null;
    }

    /// <summary>
    /// Removes energy clicked object
    /// </summary>
    private void RemoveEnergyClickedObject()
    {
        _energyObjectClicked = null;
    }

    /// <summary>
    /// Destroys lines for a new level
    /// </summary>
    public void ClearLines()
    {
        foreach (var line in _spawnedLines){
            Destroy(line.gameObject);
        }
        _spawnedLines.Clear();
    }
}