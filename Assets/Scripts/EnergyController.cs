using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool _hasEnergy;

    [SerializeField] private bool _isConnected;

    [SerializeField] private Image _glow;
    
    private LineManager _lineManager;

    private void Start()
    {
        _lineManager = FindObjectOfType<LineManager>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_hasEnergy){
            _lineManager.EnergyObjectClicked(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hasEnergy && _isConnected){
            _lineManager.EnergyObjectClicked(this);
            return;
        }

        _lineManager.SetEnergyDisabledObject(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_hasEnergy){
            _lineManager.RemoveEnergyDisabledObject();
        }
    }

    public void EnableEnergy()
    {
        _hasEnergy = true;
        _glow.enabled = true;
        _isConnected = true;
    }

    public bool GetConnectedStatus()
    {
        return _isConnected;
    }
}