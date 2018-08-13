using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;


// Courtesy of https://gist.github.com/ftvs/5822103
// Modified by us
public class CameraShake : SceneSingleton<CameraShake>
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    public float DefaultDuration = 1.5f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.3f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        SetInstance();
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Shake(float? duration = null)
    {
        duration = duration ?? DefaultDuration;
        shakeDuration = duration.Value;
    }
}