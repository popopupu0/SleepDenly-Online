using System.Collections.Generic;
using SleepDenly;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards = new List<CardData>();
    public List<CardData> sleepCards = new List<CardData>();
    public List<CardData> playableCards = new List<CardData>();

    public int startingHandSize = 8;  // Total starting hand size
    public int maxHandSize = 12;
    private const int SLEEP_CARDS_COUNT = 3;
    private const int REGULAR_CARDS_COUNT = 5;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private bool startBattleRun = true;

    void Start()
    {
        //Load all card assets from the Resources folder
        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        //Separate cards by type
        foreach (CardData card in cards)
        {
            if (card.cardType == CardType.Sleep)
            {
                sleepCards.Add(card);
            }
            else if (card.cardType == CardType.Hour || card.cardType == CardType.Skill)
            {
                playableCards.Add(card);
            }
        }
        allCards = playableCards; // Regular deck only contains hour/skill cards
    }

    void Awake()
    {
        if (drawPileManager == null) drawPileManager = FindObjectOfType<DrawPileManager>();
        if (handManager == null) handManager = FindObjectOfType<HandManager>();
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    private void DrawStartingSleepCards()
    {
        // Create a temporary list of available sleep cards
        List<CardData> availableSleepCards = new List<CardData>(sleepCards);

        for (int i = 0; i < SLEEP_CARDS_COUNT && availableSleepCards.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableSleepCards.Count);
            CardData selectedCard = availableSleepCards[randomIndex];
            handManager.AddCardToHand(selectedCard);
            availableSleepCards.RemoveAt(randomIndex); // Remove the card so it can't be drawn again
        }
    }

    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        
        // Only draw sleep cards initially
        DrawStartingSleepCards();
        startBattleRun = false;
    }

    public void DrawRegularCards()
    {
        // Draw 5 regular cards
        drawPileManager.BattleSetup(REGULAR_CARDS_COUNT, maxHandSize);
    }
}
