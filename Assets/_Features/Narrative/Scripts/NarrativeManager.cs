using System;
using System.Collections;
using System.Collections.Generic;
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

    readonly Queue<DialogueData> _dialogueQueue = new();
    EventInstance _dialogueInstance;
    DialogueData _currentDialogue;

    Coroutine _dialogueSeq;

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
        _dialogueQueue.Enqueue(data);
        print($"Queued dialogue: {data.name}. Current queue size: {_dialogueQueue.Count}");
        _dialogueSeq ??= StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        while (_dialogueQueue.TryDequeue(out var data))
        {
            _currentDialogue = data;
            _dialogueInstance = RuntimeManager.CreateInstance(data.DialogueEvent);
            _dialogueInstance.start();
            _audioSource.AssignEvents(_dialogueInstance);
            IsRunning = true;

            yield return new WaitUntil(() => !IsRunning);
        }

        _dialogueSeq = null;
    }

    void HideText()
    {
        _dialogueSpeakerUI.DOFade(0, _fadeDuration);
        _dialogueTextUI.DOFade(0, _fadeDuration);
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
        UnloadAllAudio();

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

        if (marker == "hide")
        {
            HideText();
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

    public void UnloadAllAudio()
    {
        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _dialogueInstance.clearHandle();

        foreach (var emitter in FindObjectsOfType<StudioEventEmitter>())
        {
            emitter.AllowFadeout = true;
            emitter.Stop();
        }
    }
}
