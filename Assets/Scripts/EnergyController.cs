using System.Collections;
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
    
    /// <summary>
    /// Called then pointer is down on an energy controller
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_hasEnergy){
            _lineManager.EnergyObjectClicked(this);
        }
        
        Debug.Log("ON POINTER DOWN");
    }

    /// <summary>
    /// Called then pointer enters an energy controller
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hasEnergy && _isConnected){
            _lineManager.EnergyObjectClicked(this);
            return;
        }

        _lineManager.SetEnergyDisabledObject(this);
        Debug.Log("ON POINTER ENTER");
    }

    /// <summary>
    /// Called then pointer leaves an energy controller
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {

        StartCoroutine(WaitSomeTime());
        Debug.Log("ON POINTER EXIT");

    }

    /// <summary>
    /// Its needed to wait a frame because of "Input" sequencing
    /// </summary>
    private IEnumerator WaitSomeTime()
    {
        yield return new WaitForEndOfFrame();
        if (!_hasEnergy){
            _lineManager.RemoveEnergyDisabledObject();
        }
    }

    /// <summary>
    /// Enables energy on energy controller
    /// </summary>
    public void EnableEnergy()
    {
        _hasEnergy = true;
        _glow.enabled = true;
        _isConnected = true;
    }

    /// <summary>
    /// Returns the connected status of the energy controller
    /// </summary>
    public bool GetConnectedStatus()
    {
        return _isConnected;
    }
}