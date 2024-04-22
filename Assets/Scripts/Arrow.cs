using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speedArrow;

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speedArrow;
    }
}
