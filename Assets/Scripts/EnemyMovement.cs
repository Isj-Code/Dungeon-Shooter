
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform playerTransform;
    private bool isFacingRight = true;

    [SerializeField] private float enemySpeed;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovment>().transform;
    }

    void Update()
    {
        FlipEnemy();
        FollowPlayer();
    }

    private void FlipEnemy()
    {
        bool isPlayerRigth = playerTransform.position.x > transform.position.x;

        if ((isFacingRight && !isPlayerRigth) || !isFacingRight && isPlayerRigth)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }

    private void FollowPlayer()
    {
        Vector2 vectorPlayer = playerTransform.position;
        Vector2 direction = (vectorPlayer - (Vector2)transform.position).normalized;

        transform.Translate(enemySpeed * Time.deltaTime * direction);
    }

    public void Stop()
    {
        enemySpeed = 0f;
    }


}
