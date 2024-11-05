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

    FixScoriaScenario _scena;
    void Start()
    {
        _fixedPipe.SetActive(false);
        PlayerSettings.Instance.GetComponent<PlayerLocalInput>().OnJump.AddListener(PutFuelLine);
    }

    public override bool TryInteract()
    {
        PlayerSettings.Instance.IsAnchored = false;
        return base.TryInteract();
    }

    private void PutFuelLine()
    {
        _scena = ScenarioManager.Instance.GetScenario<FixScoriaScenario>();
        if (_scena.AreFuelLinesFixed || !PlayerInteraction.Instance.IsInteracting) return;

        if (!_scena.HasFuelPipe)
        {
            NarrativeManager.Instance.PlayDialogue(_noFuelPipe);
            return;
        }


        _fixedPipe.SetActive(true);
        StartCoroutine(FixedDatShit());
    }

    IEnumerator FixedDatShit()
    {
        NarrativeManager.Instance.PlayDialogue(_fixedFuelPipe);
        yield return new WaitForSeconds(3);

        PlayerInteraction.Instance.StopInteracting();
    }
}
