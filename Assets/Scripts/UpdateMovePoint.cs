using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovePoint : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetMouseButton(0))
        {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        }

        transform.position = worldPos;
    }
}
