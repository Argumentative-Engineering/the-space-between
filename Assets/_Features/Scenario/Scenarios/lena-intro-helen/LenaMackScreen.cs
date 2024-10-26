
using UnityEngine;

public class LenaMackScreen : MonoBehaviour
{
    EventManager _evt;

    private void Start()
    {
        _evt = EventManager.Instance;
        _evt.RegisterListener("start-beeping", StartBeeping);
    }

    private void OnDisable()
    {
        _evt.UnregisterListener("start-beeping", StartBeeping);
    }

    private void StartBeeping(object[] obj)
    {
        bool beeping = (bool)obj[0];

        print(beeping);
    }

    [SerializeField] Light _light;
}
