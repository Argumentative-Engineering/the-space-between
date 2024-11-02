using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LenaPushPuzzleScenario : Scenario
{
    [SerializeField] Transform _vitalityModule;

    public override IEnumerator RunScenario()
    {
        yield return base.RunScenario();

        yield return new WaitForSeconds(0.5f);
        Announcement.Instance.ShowAnnouncment("Low oxygen");
        GameManager.Instance.Player.GetComponent<Rigidbody>().drag = 0;
        PlayerOxygen.Instance.CurrentOxygen = 8;
        PlayerOxygen.Instance.StartDecreasingOxygen = true;

        PlayerSettings.Instance.IsFrozen = true;
        PlayerSettings.Instance.OverrideCameraRotation = true;
        Camera.main.transform.DOLookAt(_vitalityModule.position, 2f).OnComplete(() =>
        {
            PlayerSettings.Instance.GetComponent<PlayerLocalInput>().IsZooming = true;
        });
        yield return new WaitForSeconds(5);
        PlayerSettings.Instance.OverrideCameraRotation = false;
        PlayerSettings.Instance.IsFrozen = false;
        PlayerSettings.Instance.GetComponent<PlayerLocalInput>().IsZooming = false;
    }

    public void NearVitalityModule()
    {
        ScenarioManager.Instance.RunNextScenario();
    }
}
