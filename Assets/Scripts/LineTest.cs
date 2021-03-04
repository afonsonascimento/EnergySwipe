using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private LineController _line;

    private LineRenderer line;
    private Vector3 mousePos;
    public Material material;
    private int currLines = 0;
    private bool _energyObjectClicked;
    private Vector3 _objectClickedPosition;
    private EnergyController _energyDisabledObject;


    private void Start()
    {
        //_line.SetUpLine(_points);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            if (!_energyObjectClicked){
                return;
            }
            if (line == null){
                CreateLine();
            }

            _energyObjectClicked = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0, _objectClickedPosition);
            line.SetPosition(1, mousePos);
        } else if (Input.GetMouseButtonUp(0) && line){
            if (_energyDisabledObject){
                _energyDisabledObject.EnableEnergy();
                line.SetPosition(0, _objectClickedPosition);
                line.SetPosition(1, _energyDisabledObject.transform.position);
                line = null;
                currLines++;
                RemoveEnergyDisabledObject();
            } else{
                Destroy(line.gameObject);
            }
            
        } else if (Input.GetMouseButton(0) && line){
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0, _objectClickedPosition);
            line.SetPosition(1, mousePos);
        }
    }

    private void CreateLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
        line.sortingOrder = 1;
    }

    public void EnergyObjectClicked(Vector3 _objectPosition)
    {
        _energyObjectClicked = true;
        _objectClickedPosition = _objectPosition;
    }

    public void SetEnergyDisabledObject(EnergyController _energyObject)
    {
        _energyDisabledObject = _energyObject;
    }

    public void RemoveEnergyDisabledObject()
    {
        _energyDisabledObject = null;
    }
}