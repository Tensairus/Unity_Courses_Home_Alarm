using UnityEngine;
using System.Collections;

public class SmartHome : MonoBehaviour
{
    [SerializeField] private AlarmDetector _alarmDetector;
    [SerializeField] private WarningMessageChanger _warningMessageChanger;
    [SerializeField] private AlarmPlayer _alarmPlayer;

    private void Start()
    {
        _alarmDetector.intruderEntered += OnHomeInvaded;
        _alarmDetector.intruderLeft += OnHomeSafe;
    }

    private void OnDestroy()
    {
        _alarmDetector.intruderEntered -= OnHomeInvaded;
        _alarmDetector.intruderLeft -= OnHomeSafe;
    }

    private void OnHomeInvaded()
    {
        _warningMessageChanger.OnHomeInvaded();
        _alarmPlayer.OnHomeInvaded();
    }

    private void OnHomeSafe()
    {
        _warningMessageChanger.OnHomeSafe();
        _alarmPlayer.OnHomeSafe();
    }
}
