using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAnimation : MonoBehaviour
{
    private Vector3 initialPosition;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float animationDuration = 1f;

    [SerializeField]
    [Range(0.1f, 100f)]
    private float animationDistance = 50f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float bouncebackDistance = 1f;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float bouncebackDuration = 0.1f;

    private AudioSource createTableAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        createTableAudioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    public void Animate()
    {
        createTableAudioSource.Play();
        StartCoroutine(AnimateTable());
    }

    private IEnumerator AnimateTable()
    {
        float elapsedTime = 0f;
        transform.position = initialPosition + Vector3.up * animationDistance;

        while (elapsedTime < animationDuration)
        {
            transform.position = Vector3.Lerp(initialPosition + Vector3.up * animationDistance, initialPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < bouncebackDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * bouncebackDistance, elapsedTime / bouncebackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < bouncebackDuration)
        {
            transform.position = Vector3.Lerp(initialPosition + Vector3.up * bouncebackDistance, initialPosition, elapsedTime / bouncebackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
