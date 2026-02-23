using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningMessageChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageUI;
    [SerializeField] private Image _guardianImage;
    [SerializeField] private GameObject _guardianImageBackground;
    [SerializeField] private string _warningMessage;
    [SerializeField] private string _runYouFoolsMessage;
    [SerializeField] private float _alphaChangingSpeed;

    private int _currentTargetBossAvatarAlpha;
    private int _minBossAvatarAlphaLevel = 0;
    private int _maxBossAvatarAlphaLevel = 1;

    private Coroutine _changeBossAvatarVisibility;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _messageUI.text = _warningMessage;
        _guardianImage.gameObject.SetActive(true);
        SetUpBossWindowStatus(false);
    }

    public void InitiateAlarmActivation()
    {
        SetImageColorAlpha(_guardianImage, _minBossAvatarAlphaLevel);
        SetUpBossWindowStatus(true);
        SetUpWarningMessage(true, Color.red, _runYouFoolsMessage);
        StartChangingBossAvatarAlpha(_maxBossAvatarAlphaLevel);
    }

    public void InitiateAlarmDeactivation()
    {
        SetUpWarningMessage(false, Color.white, _warningMessage);
        StartChangingBossAvatarAlpha(_minBossAvatarAlphaLevel);
    }

    private void SetUpBossWindowStatus(bool newStatus)
    {
        _guardianImageBackground.SetActive(newStatus);
    }

    private void SetUpWarningMessage(bool isInDangerStatus, Color messageColor, string warningMessageText)
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

            if (newAlpha == _minBossAvatarAlphaLevel || newAlpha == _maxBossAvatarAlphaLevel)
            {
                isChangingVisibility = false;

                if(newAlpha == _minBossAvatarAlphaLevel)
                {
                    SetUpBossWindowStatus(false);
                }
            }

            yield return null;
        }
    }
}