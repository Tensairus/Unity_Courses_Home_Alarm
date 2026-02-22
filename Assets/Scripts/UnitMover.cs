using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeedInside;
    [SerializeField] private float _moveSpeedOutside;
    [SerializeField] private Transform _waypointsParentObject;

    private Vector3[] _waypointPositions;
    private Vector3 _currentTargetWaypointPosition;

    private float _currentMoveSpeed;
    private int _currentWaypointArrayIndex;
    private int _waypointsArrayMinIndex;
    private int _waypointsArrayMaxIndex;

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
        if(collider.TryGetComponent<AlarmDetector>(out AlarmDetector _))
        {
            _currentMoveSpeed = _moveSpeedInside;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<AlarmDetector>(out AlarmDetector _))
        {
            _currentMoveSpeed = _moveSpeedOutside;
        }
    }

    private void Initialize()
    {
        _currentMoveSpeed = _moveSpeedOutside;

        _waypointsArrayMinIndex = 0;
        _waypointsArrayMaxIndex = _waypointsParentObject.childCount;
        _waypointPositions = new Vector3[_waypointsArrayMaxIndex];

        for (int childIndex = _waypointsArrayMinIndex; childIndex < _waypointsArrayMaxIndex; childIndex++)
        {
            _waypointPositions[childIndex] = _waypointsParentObject.GetChild(childIndex).GetComponent<Transform>().position;
        }

        _currentWaypointArrayIndex = _waypointsArrayMinIndex;
        _currentTargetWaypointPosition = _waypointPositions[_currentWaypointArrayIndex];
    }

    private void SetNextWaypointPosition()
    {
        _currentWaypointArrayIndex++;

        if (_currentWaypointArrayIndex == _waypointsArrayMaxIndex)
        {
            _currentWaypointArrayIndex = _waypointsArrayMinIndex;
        }

        _currentTargetWaypointPosition = _waypointPositions[_currentWaypointArrayIndex];
    }
}