using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
    private GameObject playerCamera;
    [SerializeField]
    private Image menuHint;
    CameraState _cameraState = CameraState.Menu;
    public CameraState CameraState
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

    public Vector3 endOfDayCameraPosition;
    public Vector3 endOfDayCameraRotation;

    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera");
        transform.position = menuCameraPosition;
        transform.rotation = Quaternion.Euler(menuCameraRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraState == CameraState.Game && !animating)
        {
            transform.position = playerCamera.transform.position;
            transform.rotation = playerCamera.transform.rotation;
        }

        var isInTutorial = FindObjectOfType<TutorialManager>().IsInTutorialMode;
        if (Input.GetKeyDown(KeyCode.Escape) && !animating && !isInTutorial)
        {
            Debug.Log("Toggling camera state");
            if (CameraState == CameraState.Game)
            {
                CameraState = CameraState.Pause;
            }
            else if (CameraState == CameraState.Pause)
            {
                CameraState = CameraState.Game;
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && !animating && CameraState == CameraState.Game)
        {
            CameraState = CameraState.Upgrades;
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

        HideMenuHint();

        if (newState != CameraState.Game)
        {
            Time.timeScale = 0;
        }

        if (newState == CameraState.Upgrades)
        {
            FindObjectOfType<UpgradesMenuAnimation>().IsDisplayed = true;
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
            case CameraState.Pause or CameraState.Upgrades:
                targetPosition = pauseCameraPosition;
                targetRotation = pauseCameraRotation;
                break;
            case CameraState.Game:
                targetPosition = playerCamera.transform.position;
                targetRotation = new Vector3(playerCamera.transform.rotation.eulerAngles.x, 0, 0);
                break;
            case CameraState.EndOfDay:
                targetPosition = endOfDayCameraPosition;
                targetRotation = endOfDayCameraRotation;
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

        if (_cameraState == CameraState.Upgrades)
        {
            FindObjectOfType<UpgradesMenuAnimation>().IsDisplayed = false;
        }

        _cameraState = newState;
        if (_cameraState == CameraState.Game)
        {
            Time.timeScale = 1;
        }
        animating = false;

        if (newState == CameraState.Game)
        {
            ShowMenuHint();
        }
    }

    public void ShowMenuHint()
    {
        if (menuHint != null)
        {
            var tempColor = menuHint.color;
            tempColor.a = 1;
            menuHint.color = tempColor;
        }
    }

    public void HideMenuHint()
    {
        if (menuHint != null)
        {
            var tempColor = menuHint.color;
            tempColor.a = 0;
            menuHint.color = tempColor;
        }
    }
}

public enum CameraState { Menu, Pause, Game, EndOfDay, Upgrades }