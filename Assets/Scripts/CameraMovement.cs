using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 20f; // Velocidad de desplazamiento de la cámara
    public float panBorderThickness = 10f; // Grosor del borde de activación del desplazamiento

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // Mover la cámara hacia arriba
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia abajo
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia la derecha
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        // Mover la cámara hacia la izquierda
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }

}