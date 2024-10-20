using UnityEngine;

public enum GameState
{
    Menu,
    Paused,
    Game,
}

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public GameState GameState { get; set; }

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
}