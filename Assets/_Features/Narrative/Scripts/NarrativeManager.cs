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
public class TimelineInfo
{
    public StringWrapper LastMarker = new StringWrapper();
}
public class NarrativeManager : MonoBehaviour
{
    [Header("Settings")]
    public EventReference DialogueEvent;
    [SerializeField] float _fadeDuration = 0.2f;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _dialogueTextUI;
    [SerializeField] TextMeshProUGUI _dialogueSpeakerUI;

    Coroutine _dialogueCoroutine;
    EventInstance _dialogueInstance;

    TimelineInfo _timelineInfo;
    GCHandle _timelineHandle;
    EVENT_CALLBACK _markerCallback;

    public static NarrativeManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

        _timelineInfo = new TimelineInfo();
        _markerCallback = new EVENT_CALLBACK(MarkerEventCallback);
        _timelineHandle = GCHandle.Alloc(_timelineInfo);

    }

    public void PlaySequence(EventReference dialogue)
    {
        DialogueEvent = dialogue;
        _dialogueCoroutine = StartCoroutine(Play());
    }

    public void StopSequence()
    {
        if (_dialogueCoroutine == null) return;

        StopCoroutine(_dialogueCoroutine);
        _dialogueCoroutine = null;

        _dialogueSpeakerUI.DOFade(0, _fadeDuration);
        _dialogueTextUI.DOFade(0, _fadeDuration);
    }

    IEnumerator Play()
    {
        if (_dialogueInstance.isValid()) _dialogueInstance.release();

        _dialogueInstance = RuntimeManager.CreateInstance(DialogueEvent);
        _dialogueInstance.setUserData(GCHandle.ToIntPtr(_timelineHandle));
        _dialogueInstance.setCallback(_markerCallback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        _dialogueInstance.start();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    private RESULT MarkerEventCallback(EVENT_CALLBACK_TYPE type, IntPtr _event, IntPtr parameters)
    {
    }

    void OnDestroy()
    {
        _dialogueInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _dialogueInstance.release();
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
