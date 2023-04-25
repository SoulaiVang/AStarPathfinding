using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateGrid : MonoBehaviour
{
    public Grid gridBase;
    public Tilemap floor;                         // walkable tilemap
    public List<Tilemap> obstacleLayers;   // all layers that contain objects to navigate around
    public GameObject gridNode;            // where the generated tiles will be stored
    public GameObject nodePrefab;          // world tile prefab

    //these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
    public int scanStartX = -300, scanStartY = -300, scanFinishX = 300, scanFinishY = 300;
    private int gridSizeX, gridSizeY;

    private List<GameObject> unsortedNodes = new List<GameObject>();   // all the nodes in the world
    public GameObject[,] nodes;           // sorted 2d array of nodes, may contain null entries if the map is of an odd shape e.g. gaps
    private int gridBoundX = 0, gridBoundY = 0;

    void Start()
    {
        gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
        gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);
        createGrid();
    }

    public List<WorldTile> getNeighbours(int x, int y, int width, int height)
    {
    List<WorldTile> myNeighbours = new List<WorldTile>();

    if (x > 0 && x < width - 1)
    {
        if (y > 0 && y < height - 1)
        {
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }

            if (nodes[x, y + 1] != null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }

            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }
        }
        else if (y == 0)
        {
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }

            if (nodes[x, y + 1] == null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }
        }
        else if (y == height - 1)
        {
            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }
        }
    }
    else if (x == 0)
    {
        if (y > 0 && y < height - 1)
        {
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }

            if (nodes[x, y + 1] != null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }
        }
        else if (y == 0)
        {
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x, y + 1] != null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }
        }
        else if (y == height - 1)
        {
            if (nodes[x + 1, y] != null)
            {
                WorldTile wt1 = nodes[x + 1, y].GetComponent<WorldTile>();
                if (wt1 != null) myNeighbours.Add(wt1);
            }

            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }
        }
    }
    else if (x == width - 1)
    {
        if (y > 0 && y < height - 1)
        {
            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }

            if (nodes[x, y + 1] != null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }

            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }
        }
        else if (y == 0)
        {
            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }
            if (nodes[x, y + 1] != null)
            {
                WorldTile wt3 = nodes[x, y + 1].GetComponent<WorldTile>();
                if (wt3 != null) myNeighbours.Add(wt3);
            }
        }
        else if (y == height - 1)
        {
            if (nodes[x - 1, y] != null)
            {
                WorldTile wt2 = nodes[x - 1, y].GetComponent<WorldTile>();
                if (wt2 != null) myNeighbours.Add(wt2);
            }

            if (nodes[x, y - 1] != null)
            {
                WorldTile wt4 = nodes[x, y - 1].GetComponent<WorldTile>();
                if (wt4 != null) myNeighbours.Add(wt4);
            }
        }
    }

    return myNeighbours;
    }

    void createGrid()
    {
        int gridX = 0, gridY = 0;
        bool foundTileOnLastPass = false;
        for (int x = scanStartX; x < scanFinishX; x++)
        {
            for (int y = scanStartY; y < scanFinishY; y++)
            {
                TileBase tb = floor.GetTile(new Vector3Int(x, y, 0));
                if (tb != null)
                {
                    bool foundObstacle = false;
                    foreach (Tilemap t in obstacleLayers)
                    {
                        TileBase tb2 = t.GetTile(new Vector3Int(x, y, 0));
                        if (tb2 != null) foundObstacle = true;
                    }

                    Vector3 worldPosition = new Vector3(x + 0.5f + gridBase.transform.position.x, y + 0.5f + gridBase.transform.position.y, 0);
                    GameObject node = (GameObject)Instantiate(nodePrefab, worldPosition, Quaternion.Euler(0, 0, 0));
                    Vector3Int cellPosition = floor.WorldToCell(worldPosition);
                    WorldTile wt = node.GetComponent<WorldTile>();
                    print(node);
                    print(wt);
                    wt.gridX = gridX;
                    wt.gridY = gridY;
                    wt.cellX = cellPosition.x;
                    wt.cellY = cellPosition.y;
                    node.transform.parent = gridNode.transform;

                    if (!foundObstacle)
                    {
                        foundTileOnLastPass = true;
                        node.name = "Walkable_" + gridX.ToString() + "_" + gridY.ToString();
                    }
                    else
                    {
                        foundTileOnLastPass = true;
                        node.name = "Unwalkable_" + gridX.ToString() + "_" + gridY.ToString();
                        wt.walkable = false;
                        node.GetComponent<SpriteRenderer>().color = Color.red;
                    }

                    unsortedNodes.Add(node);

                    gridY++;
                    if (gridX > gridBoundX)
                        gridBoundX = gridX;

                    if (gridY > gridBoundY)
                        gridBoundY = gridY;
                }
            }

            if (foundTileOnLastPass)
            {
                gridX++;
                gridY = 0;
                foundTileOnLastPass = false;
            }
        }

        nodes = new GameObject[gridBoundX + 1, gridBoundY + 1];

        foreach (GameObject g in unsortedNodes)
        {
            WorldTile wt = g.GetComponent<WorldTile>();
            nodes[wt.gridX, wt.gridY] = g;
        }

        for (int x = 0; x < gridBoundX; x++)
        {
            for (int y = 0; y < gridBoundY; y++)
            {
                if (nodes[x, y] != null)
                {
                    WorldTile wt = nodes[x, y].GetComponent<WorldTile>();
                    wt.myNeighbors = getNeighbours(x, y, gridBoundX, gridBoundY);
                }
            }
        }
    }
}