using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LenaSpinningScenario : Scenario
{
    float _spinningPercent = 100;

    bool _startSpinning = false;

    GameObject _player;
    private void Start()
    {
        _player = GameManager.Instance.Player;
        EventManager.Instance.RegisterListener("player-thrust", OnPlayerThrust);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("player-thrust", OnPlayerThrust);
    }

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(12);
        _startSpinning = true;
        _player.GetComponent<PlayerThruster>().SetThrusterVisiblity(true);

        yield return base.RunScenario();
    }


    private void OnPlayerThrust(object[] obj)
    {
        _spinningPercent -= Time.deltaTime * 5;
    }

    private void Update()
    {
        if (!_startSpinning) return;

        if (_spinningPercent <= 10)
        {
            _startSpinning = false;
            _player.transform.DORotate(Vector3.up * 180, 3);
            Camera.main.transform.DOLocalRotate(Vector3.zero, 3).OnComplete(() =>
            {
                _player.GetComponent<PlayerLocalInput>().SnapToRotation(Camera.main.transform.localRotation);
                _player.GetComponent<PlayerSettings>().UseLocalRot = false;
            }
            );
            ScenarioManager.Instance.RunNextScenario();
            return;
        }
        _player.transform.Rotate(Vector3.one, _spinningPercent * 1 * Time.deltaTime);
    }
}
