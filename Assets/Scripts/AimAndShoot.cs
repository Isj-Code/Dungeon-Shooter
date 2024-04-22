using Unity.VisualScripting;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Camera cam;
    private Vector2 mouseWorldPosition, direction;

    [SerializeField] private float aimSpeed;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform playerTransform, shootPosition;

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

        if (GameManager.Instance.isPlayerDead == false)
        {
            // Transformamos las coordenadas del screenSpace a worldSpace
            Aim();

            // Instancia de flecha
            Shoot();
        }


    }
    private void Aim()
    {
        Vector3 mouseScreenSpace = Input.mousePosition;
        mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenSpace);

        // Calculo del vector para el apuntado
        direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

        // Rotar el componente hacia la posicion seleccionada
        transform.right = Vector2.MoveTowards(
            transform.right, // Desde donde
            direction, // hacia donde
            aimSpeed * Time.deltaTime // Velocidad
        );
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject arrow = Instantiate(arrowPrefab, shootPosition.position, transform.rotation);
            arrow.GetComponent<Arrow>().Launch(direction);
        }
    }

}
