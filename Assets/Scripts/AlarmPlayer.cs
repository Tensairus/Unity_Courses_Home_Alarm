using UnityEngine;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private float _alarmSoundIncreaseSpeed;
    [SerializeField] private float _alarmSoundDecreaseSpeed;

    private float _alarmSoundChangeSpeed;
    private bool isIncreasingVolume;

    private void Awake()
    {
        _alarmAudioSource.Stop();
    }

    public void AlarmPlayerUpdate()
    {
        if (_alarmAudioSource.isPlaying)
        {
            if (isIncreasingVolume)
            {
                _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, 1, _alarmSoundChangeSpeed * Time.deltaTime);
            }
            else
            {
                _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, 0, _alarmSoundChangeSpeed * Time.deltaTime);
            }
        }

        if (_alarmAudioSource.volume == 0)
        {
            _alarmAudioSource.Stop();
        }
    }

    public void OnHomeInvaded()
    {
        if (!_alarmAudioSource.isPlaying)
        {
            _alarmAudioSource.volume = 0;
            _alarmAudioSource.Play();
        }

        _alarmSoundChangeSpeed = _alarmSoundIncreaseSpeed;
        isIncreasingVolume = true;        
    }

    public void OnHomeSafe()
    {
        _alarmSoundChangeSpeed = _alarmSoundDecreaseSpeed;
        isIncreasingVolume = false;
    }
}
