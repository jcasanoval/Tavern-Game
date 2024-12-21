using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    public static UpgradeHandler _instance;

    private GoldManager goldManager;

    private List<UpgradeButtonHandler> _upgradeButtons = new List<UpgradeButtonHandler>();

    private List<ColoringOnComplete> _coloringOnCompletes = new List<ColoringOnComplete>();

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
        }
    }
    int _dexterity;
    public int DexterityLevel
    {
        get { return _dexterity; }
        set
        {
            _dexterity = value;
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
        foreach (var coloringOnComplete in _coloringOnCompletes)
        {
            coloringOnComplete.Restart();
        }
        _upgradeButtons.Clear();

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
        OverSeerObserver.Instance.Notify(OverSeerEvent.UpgradeStat);
        Debug.Log("Upgraded " + upgradeType);
    
    }

    public int GetUpgradeLevel(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Strength:
                return StrengthLevel;
            case UpgradeType.Dexterity:
                return DexterityLevel;
            case UpgradeType.Intelligence:
                return IntelligenceLevel;
            case UpgradeType.Charisma:
                return CharismaLevel;
        }
        return 0;
    }

    public static void ReadyColorForReset(ColoringOnComplete coloringOnComplete)
    {
        Instance._coloringOnCompletes.Add(coloringOnComplete);
    }
    
}

public enum UpgradeType
{
    Strength,
    Dexterity,
    Intelligence,
    Charisma
}
