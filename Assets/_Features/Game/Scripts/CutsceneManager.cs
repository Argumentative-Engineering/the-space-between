using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _hud;
    [SerializeField] Image _fadeImage;

    public static CutsceneManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _fadeImage.DOFade(0, 0.01f);
    }

    public void Fade(float fadeAmount, Action onFadeComplete, float duration = 2, bool startBlack = false)
    {
        _fadeImage.DOKill();
        var player = GameManager.Instance.Player;
        PlayerSettings.FreezePlayer(true);

        _fadeImage.DOFade(fadeAmount, duration).From(startBlack ? 1 : 0).OnComplete(() =>
        {
            PlayerSettings.FreezePlayer(false);
            onFadeComplete?.Invoke();
        });
    }

    public void RunCutscene(PlayableDirector director, Action OnComplete = null, bool unfreezePlayerOnCutsceneEnd = true)
    {
        _hud.SetActive(false);
        PlayerSettings.FreezePlayer(true);
        director.Play();

        director.stopped += (_) =>
        {
            OnComplete?.Invoke();
            if (unfreezePlayerOnCutsceneEnd)
                PlayerSettings.FreezePlayer(false);
            _hud.SetActive(true);
        };
    }
}
