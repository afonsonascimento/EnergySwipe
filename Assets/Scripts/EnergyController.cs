using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool _hasEnergy;

    [SerializeField] private Image _glow;
    
    private LineTest _lineTest;

    private void Start()
    {
        _lineTest = FindObjectOfType<LineTest>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_hasEnergy){
            _lineTest.EnergyObjectClicked(transform.position);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entrou");
        if (_hasEnergy){
            _lineTest.EnergyObjectClicked(transform.position);
            return;
        }

        _lineTest.SetEnergyDisabledObject(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_hasEnergy){
            _lineTest.RemoveEnergyDisabledObject();
        }
    }

    public void EnableEnergy()
    {
        _hasEnergy = true;
        _glow.enabled = true;
    }
}