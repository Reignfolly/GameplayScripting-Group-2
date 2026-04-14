using UnityEngine;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;
    public TMP_Text WaveText;
    public TMP_Text LevelText; 

    string level;


    // Update is called once per frame
    void Update()
    {   
        //temporary way to find the player level.
        level = (PlayerStats.moveSpeedModifier+PlayerStats.dashCooldownModifier+WeaponStats.damageModifier+WeaponStats.rangeModifier / 15f).ToString("F1");
        WaveText.text = "Wave: 1";
        LevelText.text = "Level: " + level;
    }
}
