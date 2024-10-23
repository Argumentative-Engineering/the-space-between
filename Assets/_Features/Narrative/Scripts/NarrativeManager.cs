using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    [Header("Settings")]
    public DialogueSequence CurrentSequence;
    [SerializeField] float _fadeDuration = 0.2f;

    [Header("References")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] TextMeshProUGUI _dialogueTextUI;
    [SerializeField] TextMeshProUGUI _dialogueSpeakerUI;

    readonly Queue<DialogueLine> _lines = new();
    Coroutine _dialogueCoroutine;
    bool _playNextLine;

    public static NarrativeManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void PlaySequence(DialogueSequence sequence)
    {
        if (_lines.Count > 0 || _dialogueCoroutine != null)
        {
            Debug.LogError("Ongoing dialogue!");
            return;
        }

        CurrentSequence = sequence;
        _dialogueCoroutine = StartCoroutine(Play());
    }

    public void StopSequence()
    {
        if (_dialogueCoroutine == null) return;

        StopCoroutine(_dialogueCoroutine);
        _dialogueCoroutine = null;
        _audioSource.Stop();
        _lines.Clear();

        _dialogueSpeakerUI.DOFade(0, _fadeDuration);
        _dialogueTextUI.DOFade(0, _fadeDuration);
    }

    public void PlayNextLine()
    {
        _playNextLine = true;
    }

    IEnumerator Play()
    {
        foreach (var line in CurrentSequence.Lines)
            _lines.Enqueue(line);

        while (_lines.TryDequeue(out DialogueLine line))
        {
            _dialogueSpeakerUI.text = line.Speaker;
            _dialogueTextUI.text = line.DialogueText;
            _audioSource.clip = line.DialogueVoiceOver;
            _dialogueSpeakerUI.DOFade(1, _fadeDuration);
            _dialogueTextUI.DOFade(1, _fadeDuration);

            _audioSource.Play();
            _playNextLine = false;

            yield return CurrentSequence.IsContinuous
                ? new WaitForSeconds(_audioSource.clip.length)
                : new WaitUntil(() => _playNextLine);
        }

        StopSequence();
    }
}
