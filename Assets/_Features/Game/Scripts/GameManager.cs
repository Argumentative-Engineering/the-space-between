using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Paused,
    Game,
}

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public GameState GameState { get; private set; }

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        SetGameState(GameState.Game);
    }

    public void SetGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }

        GameState = newState;
    }
    public void MovePlayer(Transform targetTransform, bool rotate = true, bool move = true)
    {
        var currPos = Player.GetComponent<Rigidbody>().position;
        var targPos = targetTransform.position;
        var pos = Vector3.MoveTowards(currPos, targPos, (currPos - targPos).magnitude);
        if (move)
            Player.GetComponent<Rigidbody>().position = pos;
        if (rotate)
            Player.GetComponent<PlayerLocalInput>().SnapToRotation(targetTransform.rotation);
    }

    public void LoadLevel(string sceneName)
    {
        var scn = ScenarioManager.Instance;
        CutsceneManager.Instance.Fade(1, () =>
        {
            scn.UnloadScenarios();
            NarrativeManager.Instance.UnloadAllAudio();
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            var loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loading.completed += (op) =>
            {
                if (op.isDone)
                {
                    scn.LoadScenarios();
                    scn.RunNextScenario();
                    CutsceneManager.Instance.Fade(0, null);
                }
            };
        });
    }
}

public static class SceneDefinitions
{
    public static string HelenExterior = "SCN_lena-outside-helen";
    public static string SwingFlashback = "SCN_lena-fb-swing";
    public static string MarineFlashback = "SCN_lena-fb-swim";
    public static string HelenBroken = "SCN_helen-broken";
}