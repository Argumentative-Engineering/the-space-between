using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private bool isPowered = false;
    [SerializeField] Image nodeImage;
    private NodeManager nodeManager;

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
