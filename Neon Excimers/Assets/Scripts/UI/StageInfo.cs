using UnityEngine;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;
    public TMP_Text WaveText;
    public TMP_Text LevelText; 

    string level = "1";
    string wave = "1";

    // Update is called once per frame
    void Update()
    {   

        WaveText.text = "Wave:" + wave;
        LevelText.text = "Level: " + level;
    }
}
