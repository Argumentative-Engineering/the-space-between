using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image _fadeImage;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        SetGameState(GameState.Game);
        _fadeImage.DOFade(0, 0.01f);
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

    public void MovePlayer(Vector3 position)
    {
        Player.GetComponent<Rigidbody>().position = position;
    }
    public void MovePlayer(Transform targetTransform)
    {
        Player.GetComponent<Rigidbody>().position = targetTransform.position;
        Player.GetComponent<PlayerLocalInput>().SnapToRotation(targetTransform.localRotation);
    }

    public void Fade(float fadeAmount, Action onFadeComplete)
    {
        Player.GetComponent<PlayerSettings>().IsFrozen = true;
        _fadeImage.DOFade(fadeAmount, 2).OnComplete(() =>
        {
            Player.GetComponent<PlayerSettings>().IsFrozen = false;
            onFadeComplete?.Invoke();
        });
    }
}