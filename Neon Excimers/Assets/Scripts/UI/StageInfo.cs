using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;
    public TMP_Text WaveText;
    public TMP_Text LevelText;

    UnityEvent StartNewWaveEvent;

    int level = 0;
    int wave = 0;


    void Start()
    {
        if (StartNewWaveEvent == null)
        {
            StartNewWaveEvent = new UnityEvent();
        }
        StartNewWaveEvent.AddListener(IncrementWaveNumber);
    }

    public void IncrementWaveNumber()
    {
        wave += 1;
    }

    public void IncrementLevelNumber()
    {
        level += 1;
    }


    // Update is called once per frame
    void Update()
    {

        WaveText.text = "Wave: " + wave;
        LevelText.text = "Level: " + level;
    }
}
