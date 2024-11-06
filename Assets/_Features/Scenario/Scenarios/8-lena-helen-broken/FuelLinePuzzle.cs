using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelLinePuzzle : GameInteractable
{
    [SerializeField] GameObject _fixedPipe;
    [Header("Dialogue")]
    [SerializeField] DialogueData _noFuelPipe;
    [SerializeField] DialogueData _fixedFuelPipe;

    [SerializeField] FixScoriaScenario _scena;

    bool _hasChecked = false;
    void Start()
    {
        _fixedPipe.SetActive(false);
        PlayerSettings.Instance.GetComponent<PlayerLocalInput>().OnJump.AddListener(PutFuelLine);
    }

    public override bool TryInteract()
    {
        if (!_hasChecked)
        {
            _hasChecked = true;
            NarrativeManager.Instance.PlayDialogue(_noFuelPipe);
        }
        PlayerSettings.Instance.IsAnchored = false;
        return base.TryInteract();
    }

    private void PutFuelLine()
    {
        if (_scena.AreFuelLinesFixed || !PlayerInteraction.Instance.IsInteracting) return;
        if (PlayerInteraction.Instance.CurrentInteractable != this) return;
        if (!_scena.HasFuelPipe) return;

        _fixedPipe.SetActive(true);
        _scena.AreFuelLinesFixed = true;
        StartCoroutine(FixedDatShit());
    }

    IEnumerator FixedDatShit()
    {
        NarrativeManager.Instance.PlayDialogue(_fixedFuelPipe);
        yield return new WaitForSeconds(4);

        PlayerInteraction.Instance.StopInteracting();
    }
}
