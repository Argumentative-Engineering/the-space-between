using System;
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

    public void StopSequence()
    {
        _dialogueSpeakerUI.DOFade(0, _fadeDuration).SetDelay(5);
        _dialogueTextUI.DOFade(0, _fadeDuration).SetDelay(5);

        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _dialogueInstance.release();

        IsRunning = false;
    }

    void OnDestroy()
    {
        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _dialogueInstance.release();

        IsRunning = false;
    }

    private void OnMarkerEvent(string marker)
    {
        _dialogueSpeakerUI.DOKill();
        _dialogueTextUI.DOKill();

        if (marker.StartsWith("evt:"))
        {
            EventManager.Instance.BroadcastEvent(marker.Split(":")[1]);
            return;
        }

        if (marker == "end")
        {
            StopSequence();
            return;
        }

        var line = _currentDialogue.Subtitles.Where(d => d.Key == marker).FirstOrDefault();
        if (line == null)
        {
            Debug.LogError($"Missing dialogue for: {marker}; Current dialogue: {_currentDialogue.DialogueEvent.Path}");
            return;
        }

        _dialogueSpeakerUI.text = line.Speaker.Trim();
        _dialogueTextUI.text = line.Text.Trim();


        _dialogueSpeakerUI.DOFade(1, _fadeDuration);
        _dialogueTextUI.DOFade(1, _fadeDuration);

    }
}
