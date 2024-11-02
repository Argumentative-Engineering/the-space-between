using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public enum NodeColor { Blue, Green, Yellow }
    private bool isPowered = false;
    [SerializeField] Image nodeImage;
    private NodeManager nodeManager;
    private NodeColor colorType;
    private LineRenderer lineRenderer;

    public void Init(NodeManager manager, NodeColor color)
    {
        nodeManager = manager;
        colorType = color;
        SetNodeColor(color);
        SetPowered(false);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public bool IsPowered => isPowered;
    public NodeColor ColorType => colorType;

    public void SetPowered(bool powered)
    {
        isPowered = powered;
    }

    private void SetNodeColor(NodeColor color)
    {
        switch (color)
        {
            case NodeColor.Blue:
                nodeImage.color = Color.blue;
                break;
            case NodeColor.Green:
                nodeImage.color = Color.green;
                break;
            case NodeColor.Yellow:
                nodeImage.color = Color.yellow;
                break;
        }
    }

    public void ConnectTo(Node targetNode)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetNode.transform.position);
    }

    public void Disconnect()
    {
        lineRenderer.positionCount = 0;
    }
}
