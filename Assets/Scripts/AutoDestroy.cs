using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float destroyTime = 1.0f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
