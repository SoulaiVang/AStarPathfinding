using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovePoint : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 translatedMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetButtonDown("Fire1"))
        {
        mousePos = Input.mousePosition;
        Debug.Log(mousePos.x);
        Debug.Log(mousePos.y);
        }

        transform.position = mousePos;
    }
}
