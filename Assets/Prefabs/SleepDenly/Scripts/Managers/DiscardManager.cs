using System.Collections.Generic;
using SleepDenly;
using TMPro;
using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    [SerializeField] public List<CardData> discardCards = new List<CardData>();
    public TextMeshProUGUI discardCount;
    public int discardCardsCount;

    void Awake()
    {
        UpdateDiscardCount();
    }

    private void UpdateDiscardCount()
    {
        discardCount.text = discardCards.Count.ToString();
        discardCardsCount = discardCards.Count;
    }

    public void AddToDiscard(CardData card)
    {
        if (card != null)
        {
            discardCards.Add(card);
            UpdateDiscardCount();
        }
    }

    public CardData PullFromDiscard()
    {
        if (discardCards.Count > 0)
        {
            CardData cardToReturn = discardCards[discardCards.Count - 1];
            discardCards.RemoveAt(discardCards.Count - 1);
            UpdateDiscardCount();
            return cardToReturn;
        }
        else
        {
            return null;
        }
    }

    public bool PullSelectCardFromDiscard(CardData card)
    {
        if (discardCards.Count > 0 && discardCards.Contains(card))
        {
            discardCards.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<CardData> PullAllFromDiscard()
    {
        if (discardCards.Count > 0)
        {
            List<CardData> cardsToReturn = new List<CardData>(discardCards);
            discardCards.Clear();
            UpdateDiscardCount();
            return cardsToReturn;
        }
        else
        {
            return new List<CardData>();
        }
    }
}
