using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField, Tooltip("Level Controller reference")]
    private LevelController _levelController;

    [SerializeField, Tooltip("Audio manager reference")]
    private AudioManager _audioManager;
    
    private LineRenderer line;
    private Vector3 mousePos;
    public Material material;
    private int currLines = 0;
    private EnergyController _energyObjectClicked;
    private EnergyController _energyDisabledObject;
    private List<LineRenderer> _SpawnedLines = new List<LineRenderer>();

    [SerializeField, Tooltip("Line parent reference")]
    private Transform _lineParentTransform;
    

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
                _SpawnedLines.Add(line);
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

    private void CreateLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.gameObject.transform.SetParent(_lineParentTransform);
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
        line.sortingOrder = 1;
    }

    public void EnergyObjectClicked(EnergyController _objectClicked)
    {
        _energyObjectClicked = _objectClicked;
    }

    public void SetEnergyDisabledObject(EnergyController _energyObject)
    {
        if (!_energyObjectClicked){
            return;
        }
        if (_energyObject != _energyObjectClicked){
            _energyDisabledObject = _energyObject;
        }
    }

    public void RemoveEnergyDisabledObject()
    {
        _energyDisabledObject = null;
    }

    private void RemoveEnergyClickedObject()
    {
        _energyObjectClicked = null;
    }

    /// <summary>
    /// Destroys lines for a new level
    /// </summary>
    public void ClearLines()
    {
        foreach (var line in _SpawnedLines){
            Destroy(line.gameObject);
        }
        _SpawnedLines.Clear();
    }
}