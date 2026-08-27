using UnityEngine;
using UnityEngine.InputSystem;

public class linterna : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Obtener posición del mouse en pantalla
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convertir la posición del mouse a coordenadas del mundo
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Calcular dirección desde la linterna hacia el mouse
        Vector2 direction = mousePosition - transform.position;

        // Calcular el ángulo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotar la linterna
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
