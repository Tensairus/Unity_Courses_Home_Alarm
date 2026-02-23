using UnityEngine;

public class SmartHome : MonoBehaviour
{
    [SerializeField] private AlarmDetector _alarmDetector;
    [SerializeField] private WarningMessageChanger _warningMessageChanger;
    [SerializeField] private AlarmPlayer _alarmPlayer;

    private void Start()
    {
        _alarmDetector.IntruderEntered += OnIntruderEntered;
        _alarmDetector.IntruderLeft += OnIntruderLeft;
    }

    private void OnDestroy()
    {
        _alarmDetector.IntruderEntered -= OnIntruderEntered;
        _alarmDetector.IntruderLeft -= OnIntruderLeft;
    }

    private void OnIntruderEntered()
    {
        _warningMessageChanger.InitiateAlarmActivation();
        _alarmPlayer.InitiateAlarmActivation();
    }

    private void OnIntruderLeft()
    {
        _warningMessageChanger.InitiateAlarmDeactivation();
        _alarmPlayer.InitiateAlarmDeactivation();
    }
}
