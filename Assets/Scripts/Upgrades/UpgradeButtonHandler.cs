using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonHandler : MonoBehaviour
{
    public List<Image> _upgradeImages = new List<Image>();
    public List<Sprite> _upgradeSprites = new List<Sprite>();
    public List<Sprite> _oldSprites = new List<Sprite>();

    public void Start(){
        for(int i = 0; i < _upgradeImages.Count; i++){
            _oldSprites.Add(_upgradeImages[i].sprite);
        }
    }

    public int costToUpgrade = 5;

    public UpgradeType upgradeType;

    private bool _isUpgraded = false;



    public void TryToUpgrade(){
        if(_isUpgraded){
            return;
        }
        GoldManager goldManager = FindObjectOfType<GoldManager>();
        if(goldManager.CanSpendGold(costToUpgrade)){
            goldManager.SpendGold(costToUpgrade);
            UpgradeHandler upgradeHandler = UpgradeHandler.Instance;
            upgradeHandler.UpgradeStat(upgradeType, this);
            UpdateUpgradeImages();
            _isUpgraded = true;
        }
    }

    public void UpdateUpgradeImages(){
        for(int i = 0; i < _upgradeImages.Count; i++){
            _upgradeImages[i].sprite = _upgradeSprites[i];
        }
    }

    public void ResetUpgrades(){
        _isUpgraded = false;
        for(int i = 0; i < _upgradeImages.Count; i++){
            _upgradeImages[i].sprite = _oldSprites[i];
        }
    }


}
