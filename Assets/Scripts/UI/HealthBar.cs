using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    public GameObject bar;

    private Slider slider;

    private void Start() {
        slider = bar.GetComponent<Slider>();
    }

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