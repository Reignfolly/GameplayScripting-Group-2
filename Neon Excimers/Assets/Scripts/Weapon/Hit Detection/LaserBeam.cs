using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    float lifetime;
    float startWidth;

    public void Initialize(Vector3 start, Vector3 end, float width, float duration)
    {
        lifetime = duration;
        startWidth = width;

        Vector3 direction = end - start;
        float distance = direction.magnitude;

        // Position in middle
        transform.position = start + direction / 2f;

        // Rotate toward target
        transform.rotation = Quaternion.LookRotation(direction);

        // Scale (IMPORTANT: assumes cylinder forward = Z)
        transform.localScale = new Vector3(width, width, distance);

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float timer = 0f;

        Vector3 originalScale = transform.localScale;

        while (timer < lifetime)
        {
            timer += Time.deltaTime;

            float t = timer / lifetime;

            // Slight shrink effect
            float scaleFactor = Mathf.Lerp(1f, 0.85f, t);

            transform.localScale = new Vector3(
                originalScale.x * scaleFactor,
                originalScale.y * scaleFactor,
                originalScale.z
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}