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

    public void MovePlayer(Transform targetTransform, Vector3 offset = default, bool rotate = true, bool move = true, bool snap = true)
    {
        var targPos = targetTransform.position + offset;
        var playerRb = Player.GetComponent<Rigidbody>();

        if (move)
        {
            if (snap) playerRb.position = targPos;
            else
            {
                playerRb.GetComponent<Collider>().enabled = false;
                playerRb.DOMove(targPos, 0.2f).OnComplete(() =>
                {
                    playerRb.GetComponent<Collider>().enabled = true;
                });
            }
        }

        if (rotate)
            Player.GetComponent<PlayerLocalInput>().SnapToRotation(targetTransform.rotation);

        playerRb.velocity = Vector3.zero;
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