using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    public static UpgradeHandler _instance;

    private GoldManager goldManager;

    private List<UpgradeButtonHandler> _upgradeButtons = new List<UpgradeButtonHandler>();

    public static UpgradeHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UpgradeHandler>();
            }

            return _instance;
        }
        private set { }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        goldManager = FindObjectOfType<GoldManager>();
    }

    int _strength;
    public int StrengthLevel
    {
        get { return _strength; }
        set
        {
            _strength = value;
            Debug.Log("Strength level is now " + _strength);
        }
    }
    int _dexterity;
    public int DexterityLevel
    {
        get { return _dexterity; }
        set
        {
            _dexterity = value;
            Debug.Log("Dexterity level is now " + _dexterity);
        }
    }

    int _intelligence;

    public int IntelligenceLevel
    {
        get { return _intelligence; }
        set
        {
            _intelligence = value;
            FindObjectOfType<EmployeeManager>().NotifyEmployeeUpgrade();
        }
    }

    int _charisma;

    public int CharismaLevel
    {
        get { return _charisma; }
        set
        {
            _charisma = value;
            Debug.Log("Charisma level is now " + _charisma);
        }
    }



    public void ResetUpgrades()
    {
        StrengthLevel = 0;
        DexterityLevel = 0;
        IntelligenceLevel = 0;
        CharismaLevel = 0;
        foreach (var upgradeButton in _upgradeButtons)
        {
            upgradeButton.ResetUpgrades();
        }
    }

    public void UpgradeStat(UpgradeType upgradeType, UpgradeButtonHandler upgradeButton)
    {
        _upgradeButtons.Add(upgradeButton);
        switch (upgradeType)
        {
            case UpgradeType.Strength:
                StrengthLevel++;
                break;
            case UpgradeType.Dexterity:
                DexterityLevel++;
                break;
            case UpgradeType.Intelligence:
                IntelligenceLevel++;
                break;
            case UpgradeType.Charisma:
                CharismaLevel++;
                break;
        }
    
    }
    
}

public enum UpgradeType
{
    Strength,
    Dexterity,
    Intelligence,
    Charisma
}
