using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public Health_Module healthModule;
    public GameObject healthBarFill;

    public TMP_Text healthText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = Mathf.Clamp01((float)healthModule.Current_Health / healthModule.Max_Health);
        healthBarFill.transform.localScale = new Vector3(healthPercent, 1f, 1f);

        healthText.text = $"{healthModule.Current_Health} / {healthModule.Max_Health}";
    }
}
