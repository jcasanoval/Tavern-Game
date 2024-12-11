using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchievementPopUp : MonoBehaviour
{

    private static AchievementPopUp _instance;
    public static AchievementPopUp Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AchievementPopUp>();
            }
            return _instance;
        }
    }
    public bool toggled = false;
    private bool ultraLocker = false;

    public TextMeshProUGUI Title;
    public Image Icon;

    private List<Achievement> achievements = new List<Achievement>();

    private void Awake()
    {
        _instance = this;
    }

    public void RewardAchievement(Achievement achievement){
        achievements.Add(achievement);
        if(!ultraLocker){
            StartCoroutine(ProcessAchievement());
        }
    }

    public IEnumerator ProcessAchievement(){
        while(achievements.Count > 0){
            ultraLocker = true;
            yield return StartCoroutine(ShowAchievement(achievements[0]));
            achievements.RemoveAt(0);
        }
        ultraLocker = false;
    }



    public IEnumerator ShowAchievement(Achievement achievement){
        Debug.Log("Showing Achievement");
        toggled = false;
        SetText(achievement.Title);
        SetIcon(achievement.Icon);
        float timer = 0;
        while(timer < 1f){
            transform.localPosition = new Vector3(0 - Mathf.Min(timer,1f)*350, 50 , 0);
            timer += Time.unscaledDeltaTime; 
            yield return null;
        }
        timer = 0;
        while(!toggled && timer < 5f){
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        timer = 0;
        while(timer < 1f){
            transform.localPosition = new Vector3(0 - (1-Mathf.Min(timer,1f))*350, 50 , 0);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

    }
    

    public void toggledOn(){
        toggled = true;
    }

    public void SetText(string text){
        Title.text = text;
    }

    public void SetIcon(Sprite icon){
        Icon.sprite = icon;
    }

    public void ResetPosition(){
        transform.localPosition = new Vector3(0, 50, 0);

        achievements.Clear();
        ultraLocker = false;
    }
}
