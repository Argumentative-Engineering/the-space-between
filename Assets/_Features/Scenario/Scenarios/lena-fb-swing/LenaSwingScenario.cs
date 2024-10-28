using System;
using UnityEngine;

public class LenaSwingScenario : ScenarioController
{
    bool _complete;
    int _pushCount = 0;
    private void Start()
    {
        EventManager.Instance.RegisterListener("push-swing", OnPushSwing);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("push-swing", OnPushSwing);
    }

    private void OnPushSwing(object[] obj)
    {
        _pushCount = (int)obj[0];

        if (_pushCount == 5 && !NarrativeManager.Instance.IsRunning && !_complete)
        {
            _complete = true;
            Invoke(nameof(NextScene), 3);
        }
    }

    public void NextScene()
    {
        GameManager.Instance.Fade(1, () =>
        {
            print("NEXT SCENE");
        });
    }

    public void KidYell()
    {
        print("kid yelling lol");
    }
}
