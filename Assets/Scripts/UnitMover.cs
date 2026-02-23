using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeedInside;
    [SerializeField] private float _moveSpeedOutside;
    [SerializeField] private Transform _waypointsParentObject;
    [SerializeField] private Vector3[] _waypointPositions;
    
    private Vector3 _currentTargetWaypointPosition;

    private float _currentMoveSpeed;
    private int _currentWaypointArrayIndex;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (transform.position == _currentTargetWaypointPosition)
        {
            SetNextWaypointPosition();
        }

        transform.forward = _currentTargetWaypointPosition - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _currentTargetWaypointPosition, _currentMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<AlarmDetector>(out _))
        {
            _currentMoveSpeed = _moveSpeedInside;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<AlarmDetector>(out _))
        {
            _currentMoveSpeed = _moveSpeedOutside;
        }
    }

    private void Initialize()
    {
        _currentMoveSpeed = _moveSpeedOutside;
        _currentWaypointArrayIndex = 0;
        _currentTargetWaypointPosition = _waypointPositions[_currentWaypointArrayIndex];
    }

    private void SetNextWaypointPosition()
    {
        _currentWaypointArrayIndex = ++_currentWaypointArrayIndex % _waypointPositions.Length;
        _currentTargetWaypointPosition = _waypointPositions[_currentWaypointArrayIndex];
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _waypointsParentObject.childCount;
        _waypointPositions = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
            _waypointPositions[i] = _waypointsParentObject.GetChild(i).GetComponent<Transform>().position;
    }
#endif
}