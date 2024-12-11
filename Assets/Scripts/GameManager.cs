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
        gui.SetActive(true);
        FindAnyObjectByType<GoldManager>().SetInitialGold();
        FindAnyObjectByType<BarInteractable>().SetStartingStock();
        FindAnyObjectByType<AchievementKeeper>().AchievementReset();
        FindAnyObjectByType<LightsManager>().SetDayLights();
        FindAnyObjectByType<PlayerMovement>().ResetPlayerPosition();
        FindAnyObjectByType<PopularityManager>().Start();
        FindAnyObjectByType<ChairManager>().ResetChairs();
        FindAnyObjectByType<TutorialManager>().StartTutorial();
        FindAnyObjectByType<HandController>().ReleaseMug();
        ResetTables();
        FindAnyObjectByType<EmployeeManager>().RemoveAllEmployees();
        DestroyAllCustomers();
    }

    public void EndGame()
    {
        FindObjectOfType<LightsManager>().SetDayLights();
        FindAnyObjectByType<MenuCamera>().CameraState = CameraState.Menu;
        FindAnyObjectByType<MenuBoxController>().boxState = BoxState.Close;
        FindAnyObjectByType<DayCycleManager>().ForceClose();
        FindAnyObjectByType<AchievementPopUp>().ResetPosition();
        gui.SetActive(false);
    }

    private void DestroyAllCustomers()
    {
        foreach (Customer customer in FindObjectsOfType<Customer>())
        {
            Destroy(customer.gameObject);
        }
    }

    private void ResetTables()
    {
        foreach (TableInteraction table in FindObjectsOfType<TableInteraction>())
        {
            table.SetFurniture(table.startsActive);
        }
    }
}
