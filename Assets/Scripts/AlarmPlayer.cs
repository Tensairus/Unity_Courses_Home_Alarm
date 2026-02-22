using UnityEngine;
using System.Collections;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private float _alarmSoundIncreaseSpeed;
    [SerializeField] private float _alarmSoundDecreaseSpeed;

    private float _alarmSoundChangeSpeed;
    private bool _isIncreasingVolume;

    private Coroutine _enableAlarm;
    private Coroutine _disableAlarm;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _alarmAudioSource.Stop();
        _enableAlarm = StartCoroutine(EnableAlarm());
        StopCoroutine(_enableAlarm);
        _disableAlarm = StartCoroutine(DisableAlarm());
        StopCoroutine(_disableAlarm);
    }

    public void OnHomeInvaded()
    {
        if (!_alarmAudioSource.isPlaying)
        {
            _alarmAudioSource.Stop();
        }

        _alarmAudioSource.volume = 0;
        SetUpAlarmSoundPlayer(_alarmSoundIncreaseSpeed, true);

        _alarmAudioSource.Play();
        
        StopCoroutine(_disableAlarm);
        _enableAlarm = StartCoroutine(EnableAlarm());
    }

    public void OnHomeSafe()
    {
        SetUpAlarmSoundPlayer(_alarmSoundDecreaseSpeed, false);

        StopCoroutine(_enableAlarm);
        _disableAlarm = StartCoroutine(DisableAlarm());
    }

    private void SetUpAlarmSoundPlayer(float alarmSoundChangeSpeed, bool isIncreasingVolume)
    {
        _alarmSoundChangeSpeed = alarmSoundChangeSpeed;
        _isIncreasingVolume = isIncreasingVolume;
    }

    private IEnumerator EnableAlarm()
    {
        while (true)
        {
            if (_alarmAudioSource.isPlaying)
            {
                if (_isIncreasingVolume)
                {
                    _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, 1, _alarmSoundChangeSpeed * Time.deltaTime);
                }
            }

            yield return null;
        }
    }

    private IEnumerator DisableAlarm()
    {
        while (true)
        {
            if (_alarmAudioSource.isPlaying)
            {
                if (!_isIncreasingVolume)
                {
                    _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, 0, _alarmSoundChangeSpeed * Time.deltaTime);
                }
            }

            if (_alarmAudioSource.volume == 0)
            {
                _alarmAudioSource.Stop();
            }

            yield return null;
        }
    }
}
