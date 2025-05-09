using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SleepDenly;

public class CardStats : MonoBehaviour
{
    public CardData cardData;
    
    [TextArea] public string cardName;
    [TextArea] public string description;
    public int hourEffect;
    public int ageRequired;
    
    private bool statsSet = false;

    void Update()
    {
        if (!statsSet && cardData != null)
        {
            SetCardStats();
        }
    }

    private void SetCardStats()
    {
        cardName = cardData.cardName;
        description = cardData.description;
        hourEffect = cardData.hourEffect;
        ageRequired = cardData.ageRequired;
        statsSet = true;
    }
}
