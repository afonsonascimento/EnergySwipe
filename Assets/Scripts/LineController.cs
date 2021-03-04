using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _line;

    private Transform[] _points;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points)
    {
        //_line.positionCount = points.Length;
        _points = points;
        
        _line.SetPosition(0, _points[0].position);
        _line.SetPosition(1, _points[1].position);
    }

    /*private void Update()
    {
        for (int i = 0; i < _points.Length; i++){
            _line.SetPosition(i, _points[i].position);
        }
        
        _line.SetPosition(0, _points[0].position);
        _line.SetPosition(1, _points[1].position);
    }*/
}
