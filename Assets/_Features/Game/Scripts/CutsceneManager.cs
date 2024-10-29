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

    public void Fade(float fadeAmount, Action onFadeComplete, float duration = 2)
    {
        var player = GameManager.Instance.Player;
        player.GetComponent<PlayerSettings>().IsFrozen = true;

        print("fade to " + fadeAmount);
        _fadeImage.DOFade(fadeAmount, duration).OnComplete(() =>
        {
            player.GetComponent<PlayerSettings>().IsFrozen = false;
            onFadeComplete?.Invoke();
        });
    }

    public void RunCutscene(PlayableDirector director, Action OnComplete = null)
    {
        _hud.SetActive(false);
        GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = true;
        director.Play();

        director.stopped += (_) =>
        {
            OnComplete?.Invoke();
            GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = false;
            _hud.SetActive(true);
        };
    }
}