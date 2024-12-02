using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuBoxController : MonoBehaviour
{
    [SerializeField]
    Vector3 openPosition = new(0, 6.5f, 2);
    [SerializeField]
    Vector3 openRotation = new(-46.8f, 0, 0);
    [SerializeField]
    Vector3 closePosition = new(0, 0, -5);
    [SerializeField]
    Vector3 closeRotation = new(-90, 0, 0);

    private BoxState _boxState = BoxState.Close;

    public BoxState boxState
    {
        get { return _boxState; }
        set
        {
            StartCoroutine(AnimateTransition(value));
        }
    }

    private bool animating = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = closePosition;
        transform.localEulerAngles = closeRotation;
    }


    private IEnumerator AnimateTransition(BoxState newState)
    {
        if (animating)
        {
            yield break;
        }

        animating = true;
        var initialTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;
        var initialPosition = transform.position;
        var initialRotation = transform.rotation.eulerAngles;
        const float animationDuration = 2f;

        Vector3 targetPosition = Vector3.zero;
        Vector3 targetRotation = Vector3.zero;

        switch (newState)
        {
            case BoxState.Close:
                targetPosition = closePosition;
                targetRotation = closeRotation;
                break;
            case BoxState.Open:
                targetPosition = openPosition;
                targetRotation = openRotation;
                break;
        }

        Debug.Log("Initial position: " + initialPosition);
        Debug.Log("Target position: " + targetPosition);

        while (elapsedTime < animationDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / animationDuration);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRotation, targetRotation, elapsedTime / animationDuration));
            elapsedTime = Time.realtimeSinceStartup - initialTime;
            yield return null;
        }

        _boxState = newState;
        animating = false;
    }
}

public enum BoxState
{
    Open, Close
}