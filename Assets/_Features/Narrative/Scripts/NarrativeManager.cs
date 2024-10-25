using System.Linq;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSystem))]
public class NarrativeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _fadeDuration = 0.2f;

    [Header("References")]
    [SerializeField] AudioSystem _audioSource;
    [SerializeField] TextMeshProUGUI _dialogueTextUI;
    [SerializeField] TextMeshProUGUI _dialogueSpeakerUI;

    EventInstance _dialogueInstance;
    DialogueData _currentDialogue;

    public bool IsRunning { get; set; }

    public static NarrativeManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _audioSource.OnMarker += OnMarkerEvent;
    }
    private void OnDisable()
    {

        _audioSource.OnMarker -= OnMarkerEvent;
    }

    public void PlayDialogue(DialogueData data)
    {
        _currentDialogue = data;
        PlaySequence(data.DialogueEvent);
    }

    public void PlaySequence(EventReference dialogue)
    {
        _dialogueInstance = RuntimeManager.CreateInstance(dialogue);
        _dialogueInstance.start();
        _audioSource.AssignEvents(_dialogueInstance);
        IsRunning = true;
    }

    void OnDestroy()
    {
        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _dialogueInstance.release();
    }

    private void OnMarkerEvent(string marker)
    {
        var line = _currentDialogue.Subtitles.Where(d => d.Key == marker).FirstOrDefault() ?? new Subtitle()
        {
            Speaker = "Test speaker",
            Text = "I'm missing dialogue please contact seifer or jimae or aaron:(",
        };

        _dialogueSpeakerUI.text = line.Speaker.Trim();
        _dialogueTextUI.text = line.Text.Trim();

        _dialogueSpeakerUI.DOKill();
        _dialogueTextUI.DOKill();

        _dialogueSpeakerUI.DOFade(1, _fadeDuration);
        _dialogueTextUI.DOFade(1, _fadeDuration);

        _dialogueSpeakerUI.DOFade(0, _fadeDuration).SetDelay(10);
        _dialogueTextUI.DOFade(0, _fadeDuration).SetDelay(10).OnComplete(() =>
        {
            IsRunning = false;
        });
    }
}
