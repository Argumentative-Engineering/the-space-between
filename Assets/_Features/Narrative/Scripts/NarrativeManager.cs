using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using FMOD;
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

    public static NarrativeManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _audioSource.OnMarker += OnMarkerEvent;
        _audioSource.OnComplete += StopSequence;
    }
    private void OnDisable()
    {

        _audioSource.OnMarker -= OnMarkerEvent;
        _audioSource.OnComplete -= StopSequence;
    }


    public void PlaySequence(EventReference dialogue)
    {
        _dialogueInstance = RuntimeManager.CreateInstance(dialogue);
        _dialogueInstance.start();
        _audioSource.AssignEvents(_dialogueInstance);
    }

    public void StopSequence()
    {
        _dialogueSpeakerUI.DOFade(0, _fadeDuration);
        _dialogueTextUI.DOFade(0, _fadeDuration);
    }

    void OnDestroy()
    {
        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _dialogueInstance.release();
    }

    private void OnMarkerEvent(string marker)
    {
        var splits = marker.Split('_');
        var speaker = splits[0];
        var text = splits[1];
        _dialogueSpeakerUI.text = speaker;
        _dialogueTextUI.text = text;
        _dialogueSpeakerUI.DOFade(1, _fadeDuration);
        _dialogueTextUI.DOFade(1, _fadeDuration);

    }


    // IEnumerator Play()
    // {
    //     foreach (var line in CurrentSequence.Lines)
    //         _lines.Enqueue(line);

    //     while (_lines.TryDequeue(out DialogueLine line))
    //     {
    //         _dialogueSpeakerUI.text = line.Speaker;
    //         _dialogueTextUI.text = line.DialogueText;
    //         _dialogueSpeakerUI.DOFade(1, _fadeDuration);
    //         _dialogueTextUI.DOFade(1, _fadeDuration);

    //         _playNextLine = false;

    //         yield return CurrentSequence.IsContinuous
    //             ? new WaitForSeconds(_audioSource.clip.length)
    //             : new WaitUntil(() => _playNextLine);
    //     }

    //     StopSequence();
    // }
}
