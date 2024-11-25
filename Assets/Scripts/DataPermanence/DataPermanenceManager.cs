using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPermanenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "";


    public GameData gameData;

    public List<IDataPermanence> dataPermanenceObjects;

    private FileDataHandler fileDataHandler;
    public static DataPermanenceManager Instance
    {
        get;
        private set;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        this.dataPermanenceObjects = FindAllDataPermanenceObjects();
        // LoadGame();
    }

    private List<IDataPermanence> FindAllDataPermanenceObjects()
    {
        IEnumerable<IDataPermanence> dataPermanenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPermanence>();
        return dataPermanenceObjects.ToList();

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        GameData data = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if(this.gameData == null)
        {
            NewGame();
        }

        foreach (IDataPermanence dataPermanence in dataPermanenceObjects)
        {
            dataPermanence.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        foreach (IDataPermanence dataPermanence in dataPermanenceObjects)
        {
            dataPermanence.SaveData(ref gameData);
        }
        fileDataHandler.Save(ref gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
