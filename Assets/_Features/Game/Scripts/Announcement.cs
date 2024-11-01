using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Announcement : MonoBehaviour
{
    public static Announcement Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _announcementText, _warningText;

    public void ShowAnnouncment(string text)
    {
        _announcementText.text = text;
        _image.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
        _warningText.DOFade(0.05f, 1).SetLoops(-1, LoopType.Yoyo);
        _announcementText.DOFade(0.05f, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnnouncement()
    {
        _image.DOKill(true);
        _announcementText.DOKill(true);
        _warningText.DOKill(true);

        _image.DOFade(0, 1f);
        _announcementText.DOFade(0, 1f);
        _warningText.DOFade(0, 1f);
    }
}