using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public Health_Module healthModule;
    public Image healthBarFill;

    public TMP_Text healthText;

    private float originalWidth;

    void Start()
    {
            if (healthBarFill != null)
        {
            originalWidth = healthBarFill.rectTransform.sizeDelta.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = Mathf.Clamp01((float)healthModule.Current_Health / healthModule.Max_Health);

        if (healthBarFill != null)
        {
            var rt = healthBarFill.rectTransform;
            rt.sizeDelta = new Vector2(healthPercent * originalWidth, rt.sizeDelta.y);
        }

        healthText.text = $"{healthModule.Current_Health} / {healthModule.Max_Health}";
    }
}
