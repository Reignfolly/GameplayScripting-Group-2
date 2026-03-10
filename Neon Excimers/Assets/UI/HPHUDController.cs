using UnityEngine;
using UnityEngine.UIElements;


public class HPHUDController : MonoBehaviour
{
    public int HP = 3;

    public UIDocument uiDocument;

    public string hpIcon1Name = "hpIcon1";
    public string hpIcon2Name = "hpIcon2";
    public string hpIcon3Name = "hpIcon3";

    private VisualElement hpIcon1;
    private VisualElement hpIcon2;
    private VisualElement hpIcon3;

    void Start()
    {
        if (uiDocument == null)
        {
            Debug.LogError("HPHUDController requires a UIDocument reference.");
            return;
        }

        var root = uiDocument.rootVisualElement;
        hpIcon1 = root.Q<VisualElement>(hpIcon1Name);
        hpIcon2 = root.Q<VisualElement>(hpIcon2Name);
        hpIcon3 = root.Q<VisualElement>(hpIcon3Name);

        UpdateHPDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // If HP is changed externally at runtime, keep the HUD in sync
        UpdateHPDisplay();
    }

    private void UpdateHPDisplay()
    {
        HP = Mathf.Clamp(HP, 0, 3);

        if (hpIcon1 != null) hpIcon1.visible = HP >= 1;
        if (hpIcon2 != null) hpIcon2.visible = HP >= 2;
        if (hpIcon3 != null) hpIcon3.visible = HP >= 3;
    }
}
