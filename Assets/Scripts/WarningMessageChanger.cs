using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningMessageChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageUI;
    [SerializeField] private TextMeshProUGUI _guardianName;
    [SerializeField] private Image _guardianImage;
    [SerializeField] private GameObject _guardianImageBackground;
    [SerializeField] private string _warningMessage;
    [SerializeField] private string _runYouFoolsMessage;
    [SerializeField] private float _alphaChangingSpeed;

    private Coroutine _enableAlarm;
    private Coroutine _disableAlarm;

    private bool _isInDanger;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _isInDanger = false;

        _messageUI.text = _warningMessage;

        _guardianImage.gameObject.SetActive(true);
        SetUpBossWindowStatus(false);
        SetImageColorAlpha(_guardianImage, 0);

        _enableAlarm = StartCoroutine(EnableAlarm());
        StopCoroutine(_enableAlarm);
        _disableAlarm = StartCoroutine(DisableAlarm());
        StopCoroutine(_disableAlarm);
    }

    public void OnHomeInvaded()
    {
        SetImageColorAlpha(_guardianImage, 0);
        SetUpBossWindowStatus(true);
        SetUpWarningMessage(true, Color.red, _runYouFoolsMessage);

        StopCoroutine(_disableAlarm);
        _enableAlarm = StartCoroutine(EnableAlarm());
    }

    public void OnHomeSafe()
    {
        SetUpWarningMessage(false, Color.white, _warningMessage);

        StopCoroutine(_enableAlarm);
        _disableAlarm = StartCoroutine(DisableAlarm());
    }

    private void SetUpBossWindowStatus(bool newStatus)
    {
        _guardianImageBackground.SetActive(newStatus);
        _guardianName.gameObject.SetActive(newStatus);
    }

    private void SetUpWarningMessage(bool isInDangerStatus, Color messageColor, string warningMessageText)
    {
        _isInDanger = isInDangerStatus;
        _messageUI.color = messageColor;
        _messageUI.text = warningMessageText;
    }

    private IEnumerator EnableAlarm()
    {
        while (true)
        {
            float newAlpha = Mathf.MoveTowards(_guardianImage.color.a, 1, _alphaChangingSpeed * Time.deltaTime);
            SetImageColorAlpha(_guardianImage, newAlpha);

            yield return null;
        }
    }

    private IEnumerator DisableAlarm()
    {
        while (true)
        {
            float newAlpha = Mathf.MoveTowards(_guardianImage.color.a, 0, _alphaChangingSpeed * Time.deltaTime);
            SetImageColorAlpha(_guardianImage, newAlpha);

            if (_guardianImage.color.a == 0)
            {
                SetUpBossWindowStatus(false);
            }

            yield return null;
        }
    }

    private void SetImageColorAlpha(Image image, float newAlpha)
    {
        Color tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }
}
