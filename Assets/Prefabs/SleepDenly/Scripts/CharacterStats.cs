using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SleepDenly;

public class CharacterStats : MonoBehaviour
{
    public CardData characterStartData;
    private PlayerScore playerScore;
    private Opponent1Score opponent1Score;
    private Opponent2Score opponent2Score;

    [TextArea] public string cardName;           // Name of the card
    [TextArea] public string description;
    public int age;
    public string ageText;
    public int requiredSleepHours;    // Total hours needed for a Sleep card to complete
    public int currentSleepHours;

    private bool statsSet = false;
    private bool scoreIncremented = false;

    void Start()
    {
        playerScore = FindObjectOfType<PlayerScore>();
        opponent1Score = FindObjectOfType<Opponent1Score>();
        opponent2Score = FindObjectOfType<Opponent2Score>();
    }

    void Update()
    {
        if (!statsSet && characterStartData != null)
        {
            SetStartStats();
        }

        checkSleep();
    }

    private void SetStartStats()
    {
        cardName = characterStartData.cardName;
        description = characterStartData.description;
        age = characterStartData.age;
        ageText = characterStartData.ageText;
        requiredSleepHours = characterStartData.requiredSleepHours;
        currentSleepHours = characterStartData.currentSleepHours;
        statsSet = true;
    }

    private void checkSleep()
    {
        if (currentSleepHours == requiredSleepHours && !scoreIncremented)
        {
            // Check character names for Opponent2Score
            if (cardName == "Martin" || cardName == "Gigi" || cardName == "William")
            {
                if (opponent2Score != null)
                {
                    opponent2Score.score += 1;
                    scoreIncremented = true;
                }
            }
            // Check character names for Opponent1Score
            else if (cardName == "Richie" || cardName == "Rosie" || cardName == "Chris")
            {
                if (opponent1Score != null)
                {
                    opponent1Score.score += 1;
                    scoreIncremented = true;
                }
            }
        }
        else if (currentSleepHours != requiredSleepHours)
        {
            scoreIncremented = false; // Reset flag when sleep hours change
        }
    }
}
