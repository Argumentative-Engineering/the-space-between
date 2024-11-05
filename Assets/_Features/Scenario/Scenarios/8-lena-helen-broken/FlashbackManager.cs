using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashbackManager : MonoBehaviour
{
    [SerializeField] GameObject[] _toDisable;

    string _currentFlashback;

    public static FlashbackManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void RunFlashback(string flashbackSceneName)
    {

        CheckpointManager.Instance.Save();
        CutsceneManager.Instance.Fade(1, () =>
        {
            HideStuff(true);
            _currentFlashback = flashbackSceneName;
            var loading = SceneManager.LoadSceneAsync(flashbackSceneName, LoadSceneMode.Additive);
            loading.completed += (op) =>
            {
                if (op.isDone)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(flashbackSceneName));
                    ScenarioManager.Instance.LoadScenarios();
                    ScenarioManager.Instance.RunNextScenario(movePlayer: true);
                }
            };
        });
    }

    public void ExitFlashback()
    {
        CutsceneManager.Instance.Fade(1, () =>
        {
            var unload = SceneManager.UnloadSceneAsync(_currentFlashback);
            unload.completed += (op) =>
            {
                if (op.isDone)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneDefinitions.HelenBroken));
                    CheckpointManager.Instance.Load();
                    HideStuff(false);
                    CutsceneManager.Instance.Fade(0, null, startBlack: true);
                }
            };
        });
    }

    void HideStuff(bool visible)
    {
        foreach (var disable in _toDisable)
        {
            disable.SetActive(!visible);
        }
    }
}
