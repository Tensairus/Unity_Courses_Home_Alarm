using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessageChanger : MonoBehaviour
{
    [SerializeField] private AlarmDetector _alarmDetector;
    [SerializeField] private TextMeshProUGUI _messageUI;
    [SerializeField] private TextMeshProUGUI _guardianName;
    [SerializeField] private Image _guardianImage;
    [SerializeField] private GameObject _guardianImageBackground;
    [SerializeField] private string _warningMessage;
    [SerializeField] private string _runYouFoolsMessage;
    [SerializeField] private float _alphaChangingSpeed;

    private bool isInDanger;

    private void Awake()
    {
        _alarmDetector.intruderEntered += OnHomeInvaded;
        _alarmDetector.intruderLeft += OnHomeSafe;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (isInDanger)
        {
            float newAlpha = Mathf.MoveTowards(_guardianImage.color.a, 1, _alphaChangingSpeed * Time.deltaTime);
            SetImageColorAlpha(_guardianImage, newAlpha);
        }
        else if (_guardianImage.color.a != 0)
        {
            float newAlpha = Mathf.MoveTowards(_guardianImage.color.a, 0, _alphaChangingSpeed * Time.deltaTime);
            SetImageColorAlpha(_guardianImage, newAlpha);
        }

        if (_guardianImage.color.a != 0)
        {
            if (!_guardianImageBackground.activeInHierarchy)
            {
                _guardianImageBackground.SetActive(true);
                _guardianName.gameObject.SetActive(true);
            }
        }
        else
        {
            _guardianImageBackground.SetActive(false);
            _guardianName.gameObject.SetActive(false);
        }
    }

    private void Initialize()
    {
        isInDanger = false;

        _messageUI.text = _warningMessage;

        _guardianImage.gameObject.SetActive(true);
        _guardianImageBackground.SetActive(false);
        _guardianName.gameObject.SetActive(false);


        SetImageColorAlpha(_guardianImage, 0);
    }

    private void OnHomeInvaded()
    {
        _messageUI.color = Color.red;
        _messageUI.text = _runYouFoolsMessage;
        isInDanger = true;
    }

    private void OnHomeSafe()
    {
        _messageUI.color = Color.white;
        _messageUI.text = _warningMessage;
        isInDanger = false;
    }

    private void SetImageColorAlpha(Image image, float newAlpha)
    {
        Color tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }
}
