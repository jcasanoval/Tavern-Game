using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject gui;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        gui = GameObject.Find("PlayerGUI");
        gui.SetActive(false);
    }

    public void StartNewGame()
    {
        FindAnyObjectByType<MenuCamera>().CameraState = CameraState.Game;
        FindAnyObjectByType<MenuBoxController>().boxState = BoxState.Open;
        FindAnyObjectByType<GoldManager>().SetInitialGold();
        gui.SetActive(true);
    }

    public void EndGame()
    {
        FindAnyObjectByType<MenuCamera>().CameraState = CameraState.Menu;
        FindAnyObjectByType<MenuBoxController>().boxState = BoxState.Close;
        gui.SetActive(false);
    }
}
