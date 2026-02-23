using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningMessageChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageUI;
    [SerializeField] private Image _guardianImage;
    [SerializeField] private GameObject _bossAvatarWindow;
    [SerializeField] private string _warningMessage;
    [SerializeField] private string _runYouFoolsMessage;
    [SerializeField] private float _alphaChangingSpeed;

    private int _currentTargetBossAvatarAlpha;
    private int _minBossAvatarAlpha = 0;
    private int _maxBossAvatarAlpha = 1;

    private Coroutine _changeBossAvatarVisibility;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _messageUI.text = _warningMessage;
        SetImageColorAlpha(_guardianImage, _minBossAvatarAlpha);
        SetUpBossAvatarWindowStatus(false);
    }

    public void InitiateAlarmActivation()
    {        
        SetUpBossAvatarWindowStatus(true);
        SetUpWarningMessage(Color.red, _runYouFoolsMessage);
        StartChangingBossAvatarAlpha(_maxBossAvatarAlpha);
    }

    public void InitiateAlarmDeactivation()
    {
        SetUpWarningMessage(Color.white, _warningMessage);
        StartChangingBossAvatarAlpha(_minBossAvatarAlpha);
    }

    private void SetUpBossAvatarWindowStatus(bool newStatus)
    {
        _bossAvatarWindow.SetActive(newStatus);
    }

    private void SetUpWarningMessage(Color messageColor, string warningMessageText)
    {
        _messageUI.color = messageColor;
        _messageUI.text = warningMessageText;
    }

    private void SetImageColorAlpha(Image image, float newAlpha)
    {
        Color tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }

    private void StartChangingBossAvatarAlpha(int newTargetAlpha)
    {
        _currentTargetBossAvatarAlpha = newTargetAlpha;

        if (_changeBossAvatarVisibility != null)
        {
            StopCoroutine(_changeBossAvatarVisibility);
        }

        _changeBossAvatarVisibility = StartCoroutine(ChangeBossAvatarVisibility());
    }

    private IEnumerator ChangeBossAvatarVisibility()
    {
        bool isChangingVisibility = true;

        while (isChangingVisibility)
        {
            float newAlpha = Mathf.MoveTowards(_guardianImage.color.a, _currentTargetBossAvatarAlpha, _alphaChangingSpeed * Time.deltaTime);
            SetImageColorAlpha(_guardianImage, newAlpha);

            if (newAlpha == _minBossAvatarAlpha || newAlpha == _maxBossAvatarAlpha)
            {
                isChangingVisibility = false;

                if(newAlpha == _minBossAvatarAlpha)
                {
                    SetUpBossAvatarWindowStatus(false);
                }
            }

            yield return null;
        }
    }
}