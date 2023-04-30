using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 lastDirection = Vector3.zero;
    public Vector3 movement = Vector3.zero;
    bool moveDone = false;   
    List<WorldTile> path;
    List<WorldTile> reachedPathTiles = new List<WorldTile>(); 
    public Transform movePoint;
    public GameObject enemy;
    Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        path = enemy.GetComponent<AStar>().path;
        MovementPerformed();
    }

    void MovementPerformed()
    {
        SetMovementVector();
        rigidbody.velocity = movement;
    }

    void SetMovementVector()
    {
        if (path != null)
        {
            if (path.Count > 0)
            {
                if (!moveDone)
                {
                    for (int i = 0; i < path.Count; i++)
                    {
                        if (reachedPathTiles.Contains(path[i])) continue;
                        else reachedPathTiles.Add(path[i]); break;
                    }
                    WorldTile wt = reachedPathTiles[reachedPathTiles.Count - 1];
                    lastDirection = new Vector3(Mathf.Ceil(wt.cellX - transform.position.x), Mathf.Ceil(wt.cellY - transform.position.y), 0);
                    if (lastDirection.Equals(Vector3.up)) movement.y = 1;
                    if (lastDirection.Equals(Vector3.down)) movement.y = -1;
                    if (lastDirection.Equals(Vector3.left)) movement.x = -1;
                    if (lastDirection.Equals(Vector3.right)) movement.x = 1;
                    moveDone = true;
                }
                else
                {
                    movement = Vector2.zero;
                    if (Vector3.Distance(transform.position, movePoint.position) <= .001f)
                        moveDone = false;
                }
            }
        }
    }
 
}
