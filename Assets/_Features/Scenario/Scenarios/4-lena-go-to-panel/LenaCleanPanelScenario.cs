using System;
using System.Collections;
using UnityEngine;

public class LenaCleanPanelScenario : Scenario
{
    [SerializeField] DialogueData _finishedCleaningDialogue;

    int _cleanedPanels;

    private void Start()
    {
        EventManager.Instance.RegisterListener(EventDefinitions.PlayerThrust, OnPlayerThrust);
    }

    public override IEnumerator RunScenario()
    {
        CutsceneManager.Instance.Fade(0, null, startBlack: true);
        return base.RunScenario();
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.PlayerThrust, OnPlayerThrust);
    }

    private void OnPlayerThrust(object[] obj)
    {
        var thruster = (PlayerThruster)obj[0];
        var mask = (LayerMask)obj[1];

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, thruster.ThrusterRange, mask))
        {
            if (hit.collider.CompareTag("Clean"))
            {
                var clean = hit.collider.GetComponent<Cleanable>();
                clean.CleanPercent -= Time.deltaTime;
            }
        }
    }

    public void CleanedPanel()
    {
        _cleanedPanels++;

        print("Cleaned " + _cleanedPanels);
        if (_cleanedPanels == 3)
        {
            NarrativeManager.Instance.PlayDialogue(_finishedCleaningDialogue);
            ScenarioManager.Instance.RunNextScenario();
        }
    }
}