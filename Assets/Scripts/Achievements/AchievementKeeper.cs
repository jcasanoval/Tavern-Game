using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementKeeper : MonoBehaviour
{
    private void AccountForBeersSold()
    {
        
        OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer, () =>
        {
            // Increment the number of beers sold
            // Check if the player has unlocked any achievements
            // If so, notify the player
        });
    }
}
