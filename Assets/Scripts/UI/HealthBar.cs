using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{    
    public TextMeshProUGUI text;
    public Slider slider;

    public void SetMaxHealth(int health) {
        text.text = health.ToString();

        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) {
        text.text = health.ToString();
        
        slider.value = health;
    }
}