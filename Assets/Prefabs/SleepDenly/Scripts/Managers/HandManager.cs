using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SleepDenly;
using System;
using System.Xml.Serialization;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //Root of the hand position
    public float fanSpread = -7.5f;

    public float cardSpacing = 1;
    public float verticalSpacing = 1;
    public int maxHandSize = 12;
    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of the card objects in our hand

    private DrawPileManager drawPileManager;

    void Awake()
    {
        drawPileManager = FindObjectOfType<DrawPileManager>();
    }

    void Start()
    {

    }

    public void AddCardToHand(CardData cardData)
    {

        //Instantiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        //Set the CardData of the instantiated card
        newCard.GetComponent<CardDisplay>().cardData = cardData;
        newCard.GetComponent<CardDisplay>().SetupCard(cardData);

        UpdateHandVisuals();
    }

    public void DrawCard()
    {
        if (drawPileManager != null)
        {
            CardData cardToDraw = drawPileManager.DrawCard(this);  // Pass 'this' as parameter
            if (cardToDraw != null)
            {
                AddCardToHand(cardToDraw);
            }
        }
    }

    void Update()
    {
        //UpdateHandVisuals();
    }
    
    public void BattleSetup(int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); //Normalize card position between -1, 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

            //Set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
