using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private GameObject playerCamera;

    public CameraState _cameraState = CameraState.Game;
    public CameraState cameraState
    {
        get { return _cameraState; }
        set
        {
            StartCoroutine(AnimateTransition(value));
        }
    }

    private bool animating = false;

    public Vector3 menuCameraPosition;
    public Vector3 menuCameraRotation;

    public Vector3 pauseCameraPosition;
    public Vector3 pauseCameraRotation;

    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraState == CameraState.Game && !animating)
        {
            transform.position = playerCamera.transform.position;
            transform.rotation = playerCamera.transform.rotation;
        }

        var isInTutorial = FindObjectOfType<TutorialManager>().IsInTutorialMode;
        if (Input.GetKeyDown(KeyCode.Escape) && !animating && !isInTutorial)
        {
            Debug.Log("Toggling camera state");
            if (cameraState == CameraState.Game)
            {
                cameraState = CameraState.Pause;
            }
            else if (cameraState == CameraState.Pause)
            {
                cameraState = CameraState.Game;
            }
        }
    }

    private IEnumerator AnimateTransition(CameraState newState)
    {
        Debug.Log("Animating transition to " + newState);
        if (animating)
        {
            Debug.Log("Already animating");
            yield break;
        }

        animating = true;
        float elapsedTime = 0f;
        var initialPosition = transform.position;
        var initialRotation = transform.rotation.eulerAngles;
        const float animationDuration = 2f;

        Vector3 targetPosition = Vector3.zero;
        Vector3 targetRotation = Vector3.zero;

        switch (newState)
        {
            case CameraState.Menu:
                targetPosition = menuCameraPosition;
                targetRotation = menuCameraRotation;
                break;
            case CameraState.Pause:
                targetPosition = pauseCameraPosition;
                targetRotation = pauseCameraRotation;
                break;
            case CameraState.Game:
                targetPosition = playerCamera.transform.position;
                targetRotation = playerCamera.transform.rotation.eulerAngles;
                break;
        }

        Debug.Log("Initial position: " + initialPosition);
        Debug.Log("Target position: " + targetPosition);

        while (elapsedTime < animationDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / animationDuration);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRotation, targetRotation, elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _cameraState = newState;
        animating = false;
    }
}

public enum CameraState { Menu, Pause, Game }