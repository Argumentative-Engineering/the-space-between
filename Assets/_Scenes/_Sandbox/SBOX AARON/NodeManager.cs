using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject powerNodeUI;
    public Text powerStatusTxt;
    public int gridSize = 5;
    public float spacing = 10f;
    public int targetPower = 100;

    private int remainingPower = 0;
    private List<Node> nodes = new List<Node>();

    void Start()
    {
        powerNodeUI.SetActive(false);
        GenerateGrid();
        UpdatePowerStatus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !powerNodeUI.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "TABLET")
            {
                ActivatePuzzle();
            }
        }
    }

    void GenerateGrid()
    {
        float startX = -(gridSize - 1) * (nodePrefab.GetComponent<RectTransform>().sizeDelta.x + spacing) / 2f;
        float startY = -(gridSize - 1) * (nodePrefab.GetComponent<RectTransform>().sizeDelta.y + spacing) / 2f;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject nodeObject = Instantiate(nodePrefab, powerNodeUI.transform);
                nodeObject.name = "Node_" + x + "_" + y;

                RectTransform rt = nodeObject.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(startX + x * (rt.sizeDelta.x + spacing), startY + y * (rt.sizeDelta.y + spacing));

                Node node = nodeObject.GetComponent<Node>();
                node.Init(this);
                nodes.Add(node);

                Button btn = nodeObject.GetComponent<Button>();
                btn.onClick.AddListener(() => ToggleNodePower(node));
            }
        }

        Debug.Log("Grid generated with " + nodes.Count + " nodes.");
    }

    void ActivatePuzzle()
    {
        powerNodeUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToggleNodePower(Node node)
    {
        if (node.IsPowered)
        {
            node.SetPowered(false);
            remainingPower -= 10;
        }
        else
        {
            node.SetPowered(true);
            remainingPower += 10;
        }

        UpdatePowerStatus();

        if (remainingPower >= targetPower)
        {
            CompletePuzzle();
        }
    }

    void UpdatePowerStatus()
    {
        powerStatusTxt.text = "Power Level: " + remainingPower + "%";
    }

    void CompletePuzzle()
    {
        powerNodeUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Puzzle completed!");
    }
}
