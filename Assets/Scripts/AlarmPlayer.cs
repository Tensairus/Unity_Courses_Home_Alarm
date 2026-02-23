using UnityEngine;
using System.Collections;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private float _alarmSoundIncreaseSpeed;
    [SerializeField] private float _alarmSoundDecreaseSpeed;

    private int _currentTargetVolumeLevel;
    private float _alarmSoundChangeSpeed;

    private int _minSoundLevel = 0;
    private int _maxSoundLevel = 1;

    private Coroutine _playAlarmSound;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _alarmAudioSource.Stop();
    }

    public void InitiateAlarmActivation()
    {
        if (_alarmAudioSource.isPlaying == false)
        {
            _alarmAudioSource.Play();
        }

        if (_playAlarmSound != null)
        {
            StopCoroutine(_playAlarmSound);
        }

        SetUpAlarmSoundPlayer(_alarmSoundIncreaseSpeed, _maxSoundLevel);
        _alarmAudioSource.volume = _minSoundLevel;
        _playAlarmSound = StartCoroutine(PlayAlarmSound());
    }

    public void InitiateAlarmDeactivation()
    {
        SetUpAlarmSoundPlayer(_alarmSoundDecreaseSpeed, _minSoundLevel);
    }

    private void SetUpAlarmSoundPlayer(float alarmSoundChangeSpeed, int currentTargetVolumeLevel)
    {
        _alarmSoundChangeSpeed = alarmSoundChangeSpeed;
        _currentTargetVolumeLevel = currentTargetVolumeLevel;
    }

    private IEnumerator PlayAlarmSound()
    {
        bool isAlarmActive = true;

        while (isAlarmActive)
        {
            _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, _currentTargetVolumeLevel, _alarmSoundChangeSpeed * Time.deltaTime);

            if (_alarmAudioSource.volume == _minSoundLevel)
            {
                _alarmAudioSource.Stop();
                isAlarmActive = false;
            }

            yield return null;
        }
    }
}
