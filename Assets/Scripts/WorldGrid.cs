using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour {

    public bool drawGridGizmo;
    public Vector2 gridWoldSize;
    public float nodeRadius;
    public GameObject nodePrefab, gridPannel;

    private Node[,] grid;
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWoldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWoldSize.y / nodeDiameter);
        CreateGrid();
    }

    //Creates the grid of Nodes
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - (Vector3.right * gridWoldSize.x / 2) - (Vector3.up * gridWoldSize.y / 2);

        //Places every Node in the Grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //Takes the center of the new Node in the Grid
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                //Place the new node to the choosen coordonate
                //grid[x, y] = new Node(worldPoint, x, y, this);
                grid[x, y] = Instantiate(nodePrefab, transform).GetComponent<Node>().Init(worldPoint, x, y, gridPannel);
            }
        }
    }

    //Returns the Node of the position in parameter
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWoldSize.x / 2) / gridWoldSize.x;
        float percentY = (worldPosition.z + gridWoldSize.y / 2) / gridWoldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    //Returns a list of all neighbours of the Node in parameter
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    //Draws the Grid and the Nodes
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWoldSize.x, gridWoldSize.y, 1));

        if (grid != null && drawGridGizmo)
        {
            foreach (Node n in grid)
            {
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
}
