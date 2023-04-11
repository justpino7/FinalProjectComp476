using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthManager playerHealthManager;
    public Image healthBarImage;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)playerHealthManager.health / 100;
        healthBarImage.transform.localScale = new Vector3(healthPercentage, 1, 1);
    }
}