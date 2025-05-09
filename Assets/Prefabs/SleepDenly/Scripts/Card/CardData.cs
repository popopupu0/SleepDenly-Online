using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SleepDenly
{
    public enum CardType
    {
        Sleep,   // Represents sleep cards
        Hour,    // Represents hour cards (+/- hours)
        Skill    // Represents special action cards
    }

    [CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card Data")]
    public class CardData : ScriptableObject
    {
        [Header("Basic Info")]
        public int cardID;                  // Unique ID for the card
        [TextArea]public string cardName;           // Name of the card
        [TextArea] public string description; // Description of the card
        public GameObject prefab;
        public Sprite artwork;            // Artwork for the card

        [Header("Card Type")]
        public CardType cardType;         // Type of the card (Sleep, Hour, Skill)

        [Header("Sleep Card Specific")]
        public int age;
        public string ageText;
        public int requiredSleepHours;    // Total hours needed for a Sleep card to complete
        public int currentSleepHours;     // Tracks how many hours have been added (progress)

        [Header("Hour Card Specific")]
        public int hourEffect;            // Hours this card adds (+) or removes (-)
        public bool positive;
        public int ageRequired;

        [Header("Skill Card Specific")]
        public SkillEffect skillEffect;   // Enum for different skill card effects
    }

    public enum SkillEffect
    {
        None,            // No skill effect (default)
        StealCard,       // Steal another player's hand
        LuckyCard,       // Complete one Sleep Card
        Protect,         // Protect against an action
        CancelProtect,   // Cancel another player's Protect
        SwapHand         // Swap hands with another player
    }

}
