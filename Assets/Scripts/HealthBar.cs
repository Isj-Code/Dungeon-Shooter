using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    public void UpdateHealthbar(float maxHealth, float health)
    {
        // Mas cerca de 1 mas llena la barra.
        healthbar.fillAmount = health/maxHealth;
    }
}
