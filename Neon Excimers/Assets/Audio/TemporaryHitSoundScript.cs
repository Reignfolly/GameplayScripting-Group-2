using UnityEngine;

public class TemporaryHitSoundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float SecondsToDelete = 2f;
    void Start()
    {

    }

    void Awake()
    {
        var PitchMax = 1.18f;
        var PitchMin = 0.92f;
        var AudioSource = this.gameObject.GetComponentInChildren<AudioSource>();
        var NewPitch = UnityEngine.Random.Range(PitchMin, PitchMax);
        AudioSource.Play();
        AudioSource.pitch = NewPitch;
    }

    // Update is called once per frame
    void Update()
    {
        SecondsToDelete -= Time.deltaTime;
        if (SecondsToDelete <= 0)
        {
            Destroy(gameObject);
        }
    }
}
