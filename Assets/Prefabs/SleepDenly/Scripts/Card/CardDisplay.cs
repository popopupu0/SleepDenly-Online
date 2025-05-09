using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SleepDenly;

public class CardDisplay : MonoBehaviour
{
    public CardData cardData;             // Reference to the card data
    public Image artworkImage;
    public TMP_Text descriptionText;
    public Image cardBackground1;        // Background for Sleep cards
    public Image cardBackground2;        // Background for Hour and Skill cards
    public GameObject cardBack;
    public TMP_Text sleepStatsText;

    public bool isCardBackVisible = false;

    [Header("Sleep Card UI")]
    public GameObject sleepCardUI;        // UI specific to Sleep cards
    public TMP_Text nameText;
    public TMP_Text sleepProgressText;    // Displays current/required sleep hours
    public TMP_Text ageText;              // Displays the age for Sleep cards

    [Header("Hour Card UI")]
    public GameObject hourCardUI;         // UI specific to Hour cards
    public TMP_Text hourName;
    public TMP_Text hourEffectText;       // Displays the hour effect (+/- hours)
    public TMP_Text ageRequiredText;      // Displays the age restriction

    [Header("Skill Card UI")]
    public GameObject skillCardUI;        // UI specific to Skill cards
    public TMP_Text skillName;            // Displays the name of the skill

    private CharacterStats characterStats;
    private bool hasShownCardBack = false;  // Add this flag

    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        if (hourCardUI != null && skillCardUI != null)
        {
            SetupCard(cardData);
        }
        
    }

    void Update()
    {
        // Update sleep progress text if this is a sleep card
        if (cardData != null && cardData.cardType == CardType.Sleep && characterStats != null)
        {
            UpdateSleepProgress();
        }

        // Existing card back logic
        if (!hasShownCardBack && cardData != null && cardData.cardType == CardType.Sleep && characterStats != null)
        {
            if (characterStats.requiredSleepHours > 0 && 
                characterStats.currentSleepHours == characterStats.requiredSleepHours)
            {
                ShowCardBack(true);
                hasShownCardBack = true;
                Debug.Log($"Sleep card complete! Current Hours: {characterStats.currentSleepHours}, Required Hours: {characterStats.requiredSleepHours}");
            }
        }
    }

    private void UpdateSleepProgress()
    {
        if (sleepStatsText != null)
        {
            sleepStatsText.text = $"{characterStats.currentSleepHours}/{characterStats.requiredSleepHours}";
        }
    }

    public void SetupCard(CardData cardData)
    {
        // Set basic info
        artworkImage.sprite = cardData.artwork;
        nameText.text = cardData.cardName;

        // Hide all specific UI panels initially
        sleepCardUI.SetActive(false);
        hourCardUI.SetActive(false);
        skillCardUI.SetActive(false);

        // Handle specific card types
        switch (cardData.cardType)
        {
            case CardType.Sleep:
                sleepCardUI.SetActive(true);
                sleepProgressText.text = $"{cardData.requiredSleepHours} hours";
                ageText.text = $"{cardData.ageText}";
                cardBackground1.enabled = true; // Show the background
                cardBackground2.enabled = false;
                cardBackground1.color = Color.white; // Reset to default color
                break;

            case CardType.Hour:
                hourCardUI.SetActive(true);
                hourName.text = cardData.cardName;
                hourEffectText.text = (cardData.hourEffect >= 0 ? "+" : "") + cardData.hourEffect + ((cardData.hourEffect >= -1 && cardData.hourEffect <= 1) ? " hour" : " hours");
                ageRequiredText.text = cardData.ageRequired > 0 ? $"{cardData.ageRequired}+ years old" : "";

                // Remove the background image and change the color
                cardBackground1.enabled = false; // Disable the background image
                cardBackground2.enabled = true;
                cardBackground2.color = cardData.positive ? new Color(0.745f, 0.89f, 0.733f) : new Color(1f, 0.66f, 0.66f); // Set card color
                break;

            case CardType.Skill:
                skillCardUI.SetActive(true);
                skillName.text = cardData.cardName; // Set the skill name

                // Remove the background image and set the card color to grey
                cardBackground1.enabled = false; // Disable the background image
                cardBackground2.enabled = true;
                cardBackground2.color = new Color(0.76f, 0.8f, 0.82f); // Set card color to grey
                break;
        }
    }

    public void ShowCardBack(bool show)
    {
        cardBack.SetActive(show);
        Debug.Log("Card back shown");
    }
}
