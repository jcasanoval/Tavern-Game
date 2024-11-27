using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private GameObject playerCamera;

    public CameraState _cameraState = CameraState.Menu;
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
        transform.position = menuCameraPosition;
        transform.rotation = Quaternion.Euler(menuCameraRotation);
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

        if (Input.GetKeyDown(KeyCode.P) && !animating)
        {
            FindAnyObjectByType<MenuBoxController>().boxState = BoxState.Open;
            cameraState = CameraState.Game;
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

        if (newState != CameraState.Game)
        {
            Time.timeScale = 0;
        }

        animating = true;
        var initialTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;
        var initialPosition = transform.position;
        var initialRotation = transform.rotation.eulerAngles;
        float animationDuration = newState == CameraState.Menu || _cameraState == CameraState.Menu ? 2 : 1;

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
                targetRotation = new Vector3(playerCamera.transform.rotation.eulerAngles.x, 0, 0);
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

        _cameraState = newState;
        if (_cameraState == CameraState.Game)
        {
            Time.timeScale = 1;
        }
        animating = false;
    }
}

public enum CameraState { Menu, Pause, Game }