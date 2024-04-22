using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Camera cam;
    private Vector2 mouseWorldPosition;

    [SerializeField] private float aimSpeed;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        // Para obtener la referencia
        cam = Camera.main;
    }

    void Update()
    {
        // El transform del objeto con el script,
        // es decir el Aim se igual al del player pasado.
        transform.position = playerTransform.position;

        // Transformamos las coordenadas del screenSpace a worldSpace
        Vector3 mouseScreenSpace = Input.mousePosition;
        mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenSpace);

        // Calculo del vector para el apuntado
        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;

        // Rotar el componente hacia la posicion seleccionada
        transform.right = Vector2.MoveTowards(
            transform.right, // Desde donde
            direction, // hacia donde
            aimSpeed * Time.deltaTime // Velocidad
        );

    }
}
