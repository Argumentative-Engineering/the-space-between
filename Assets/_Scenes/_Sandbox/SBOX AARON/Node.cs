using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private bool isPowered = false;
    private Image nodeImage;
    private NodeManager nodeManager;

    void Awake()
    {
        nodeImage = GetComponent<Image>();
    }

    public void Init(NodeManager manager)
    {
        nodeManager = manager;
        SetPowered(false);
    }

    public bool IsPowered => isPowered;

    public void SetPowered(bool powered)
    {
        isPowered = powered;
        nodeImage.color = isPowered ? Color.green : Color.red;
    }
}
