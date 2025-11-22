using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float shakeSpeed = 20f;
    private float totalDuration = 0f;

    [Header("2D Shake Settings")]
    public float verticalMultiplier = 0.4f;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            float progress = shakeDuration / totalDuration;
            float fade = progress;

            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude * fade;
            float y = Mathf.Cos(Time.time * shakeSpeed) * shakeMagnitude * fade * verticalMultiplier;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0f)
                transform.localPosition = originalPosition;
        }
    }

    public void Shake(float duration, float magnitude, float speed = 20f)
    {
        shakeDuration = duration;
        totalDuration = duration;
        shakeMagnitude = magnitude;
        shakeSpeed = speed;
    }
}
