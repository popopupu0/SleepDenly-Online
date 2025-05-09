using System.Collections.Generic;
using SleepDenly;
using TMPro;
using UnityEngine;

public class DrawPileManager : MonoBehaviour
{
    public List<CardData> drawPile = new List<CardData>();

    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    private DiscardManager discardManager;

    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        handManager = FindObjectOfType<HandManager>();
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void MakeDrawPile(List<CardData> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
        Debug.Log($"Drawing {numberOfCardsToDraw} regular cards");
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            CardData card = DrawCard(handManager);  // Add handManager parameter back
            if (card != null)
            {
                handManager.AddCardToHand(card);
            }
        }
        UpdateDrawPileCount();
    }

    // Add this new method for the button to call
    public void DrawCardButton()
    {
        if (handManager != null)
        {
            CardData card = DrawCard(handManager);
            if (card != null)
            {
                handManager.AddCardToHand(card);
            }
        }
    }

    // Keep the existing DrawCard method
    public CardData DrawCard(HandManager handManager)  // Change back to requiring HandManager parameter
    {
        if (drawPile.Count > 0)
        {
            CardData drawnCard = drawPile[0];
            drawPile.RemoveAt(0);
            UpdateDrawPileCount();  // Update counter after drawing
            return drawnCard;
        }
        return null;
    }

    private void RefillDeckFromDiscard()
    {
        if (discardManager == null)
        {
            discardManager = FindObjectOfType<DiscardManager>();
        }

        if (discardManager != null && discardManager.discardCardsCount > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0;
        }
        UpdateDrawPileCount();
    }

    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }
}
