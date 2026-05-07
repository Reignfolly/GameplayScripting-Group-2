using UnityEngine;
using TMPro;

public class XPDisplay : MonoBehaviour
{
    public UIActivationController uiController;
    public GameObject XPBarFill;

    public TMP_Text XPText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XPPercent = Mathf.Clamp01((float)uiController.KillCount / uiController.upgradeThreshold);
        XPBarFill.transform.localScale = new Vector3(XPPercent, 1f, 1f);

        XPText.text = $"{uiController.KillCount} / {uiController.upgradeThreshold}";
    }
}
