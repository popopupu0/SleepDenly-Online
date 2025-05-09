using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the "using Mirror" assembly reference is required on any script that involves networking
using Mirror;

//the PlayerManager is the main controller script that can act as Server, Client, and Host (Server/Client). Like all network scripts, it must derive from NetworkBehaviour (instead of the standard MonoBehaviour)
public class PlayerManager : NetworkBehaviour
{
    //Card1 and Card2 are located in the inspector, whereas PlayerArea, EnemyArea, and DropZone are located at runtime within OnStartClient()
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    public GameObject Card4;
    public GameObject Card5;
    public GameObject Card6;
    public GameObject Card7;
    public GameObject Card8;
    public GameObject Card9;
    public GameObject Card10;
    public GameObject Card11;
    public GameObject Card12;
    public GameObject Card13;
    public GameObject Card14;
    public GameObject Card15;
    public GameObject Card16;
    public GameObject Card17;
    public GameObject Card18;
    public GameObject Card19;
    public GameObject Card20;
    public GameObject Card21;
    public GameObject Card22;
    public GameObject Card23;
    public GameObject Card24;
    public GameObject Card25;
    public GameObject Card26;
    public GameObject Card27;
    public GameObject Card28;
    public GameObject Card29;
    public GameObject Card30;
    public GameObject Card31;
    public GameObject Card32;
    public GameObject Card33;
    public GameObject Card34;
    public GameObject Card35;
    public GameObject Card36;
    public GameObject Card37;
    public GameObject Card38;
    public GameObject Card39;
    public GameObject Card40;
    public GameObject Card41;
    public GameObject Card42;
    public GameObject Card43;
    public GameObject Card44;
    public GameObject Card45;
    public GameObject Card46;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    //public GameObject DropZone;
    public GameObject DropZone11;
    public GameObject DropZone12;
    public GameObject DropZone13;
    public GameObject DropZone21;
    public GameObject DropZone22;
    public GameObject DropZone23;

    //the cards List represents our deck of cards
    List<GameObject> cards = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        //DropZone = GameObject.Find("DropZone");
        DropZone11 = GameObject.Find("DropZone1-1");
        DropZone12 = GameObject.Find("DropZone1-2");
        DropZone13 = GameObject.Find("DropZone1-3");
        DropZone21 = GameObject.Find("DropZone2-1");
        DropZone22 = GameObject.Find("DropZone2-2");
        DropZone23 = GameObject.Find("DropZone2-3");
    }

    //when the server starts, store Card1 and Card2 in the cards deck. Note that server-only methods require the [Server] attribute immediately preceding them!
    [Server]
    public override void OnStartServer()
    {
        cards.Add(Card1);
        cards.Add(Card2);
        cards.Add(Card3);
        cards.Add(Card4);
        cards.Add(Card5);
        cards.Add(Card6);
        cards.Add(Card7);
        cards.Add(Card8);
        cards.Add(Card9);
        cards.Add(Card10);
        cards.Add(Card11);
        cards.Add(Card12);
        cards.Add(Card13);
        cards.Add(Card14);
        cards.Add(Card15);
        cards.Add(Card16);
        cards.Add(Card17);
        cards.Add(Card18);
        cards.Add(Card19);
        cards.Add(Card20);
        cards.Add(Card21);
        cards.Add(Card22);
        cards.Add(Card23);
        cards.Add(Card24);
        cards.Add(Card25);
        cards.Add(Card26);
        cards.Add(Card27);
        cards.Add(Card28);
        cards.Add(Card29);
        cards.Add(Card30);
        cards.Add(Card31);
        cards.Add(Card32);
        cards.Add(Card33);
        cards.Add(Card34);
        cards.Add(Card35);
        cards.Add(Card36);
        cards.Add(Card37);
        cards.Add(Card38);
        cards.Add(Card39);
        cards.Add(Card40);
        cards.Add(Card41);
        cards.Add(Card42);
        cards.Add(Card43);
        cards.Add(Card44);
        cards.Add(Card45);
        cards.Add(Card46);
    }
    
    //Commands are methods requested by Clients to run on the Server, and require the [Command] attribute immediately preceding them. CmdDealCards() is called by the DrawCards script attached to the client Button
    [Command]
    public void CmdDealCards()
    {
        // Get only this player's cards count by checking children with hasAuthority
        int currentHandSize = 0;
        foreach (Transform child in PlayerArea.transform)
        {
            if (child.GetComponent<NetworkIdentity>().hasAuthority == connectionToClient.identity.hasAuthority)
            {
                currentHandSize++;
            }
        }
        
        if (currentHandSize < 5)
        {
            // Create a list of available cards (not yet drawn)
            List<GameObject> availableCards = new List<GameObject>(cards);
            int cardsToDraw = 5 - currentHandSize;
            
            for (int i = 0; i < cardsToDraw && availableCards.Count > 0; i++)
            {
                // Pick a random card from available cards
                int randomIndex = Random.Range(0, availableCards.Count);
                GameObject selectedCard = availableCards[randomIndex];
                
                // Remove the card from available pool to prevent duplicates
                availableCards.RemoveAt(randomIndex);
                
                GameObject card = Instantiate(selectedCard, new Vector2(0, 0), Quaternion.identity);
                NetworkServer.Spawn(card, connectionToClient);
                RpcShowCard(card, "Dealt", "");
            }
        }
    }

    //PlayCard() is called by the DragDrop script when a card is placed in the DropZone, and requests CmdPlayCard() from the Server
    public void PlayCard(GameObject card, GameObject targetDropZone)
    {
        CmdPlayCard(card, targetDropZone.name);
    }

    //CmdPlayCard() uses the same logic as CmdDealCards() in rendering cards on all Clients, except that it specifies that the card has been "Played" rather than "Dealt"
    [Command]
    void CmdPlayCard(GameObject card, string dropZoneName)
    {
        RpcShowCard(card, "Played", dropZoneName);
        
        // Get the CharacterStats of the card in the drop zone
        GameObject targetZone = GameObject.Find(dropZoneName);
        if (targetZone != null)
        {
            CharacterStats zoneCardStats = targetZone.GetComponentInChildren<CharacterStats>();
            if (zoneCardStats != null)
            {
                RpcUpdateSleepHours(zoneCardStats.gameObject, card);  // Pass the played card
            }
        }

        if (isServer)
        {
            UpdateTurnsPlayed();
        }
    }

    [ClientRpc]
    void RpcUpdateSleepHours(GameObject targetCard, GameObject playedCard)
    {
        CharacterStats stats = targetCard.GetComponent<CharacterStats>();
        CardStats cardStats = playedCard.GetComponent<CardStats>();
        Debug.Log($"CardStats: {cardStats}, Stats: {stats}");
        
        if (stats != null && cardStats != null)
        {
            stats.currentSleepHours += cardStats.hourEffect;
        }
    }

    //UpdateTurnsPlayed() is run only by the Server, finding the Server-only GameManager game object and incrementing the relevant variable
    [Server]
    void UpdateTurnsPlayed()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.UpdateTurnsPlayed();
        RpcLogToClients("Turns Played: " + gm.TurnsPlayed);

    }

    //RpcLogToClients demonstrates how to request all clients to log a message to their respective consoles
    [ClientRpc]
    void RpcLogToClients(string message)
    {
        Debug.Log(message);
    }

    //ClientRpcs are methods requested by the Server to run on all Clients, and require the [ClientRpc] attribute immediately preceding them
    [ClientRpc]
    void RpcShowCard(GameObject card, string type, string dropZoneName)
    {
        //if the card has been "Dealt," determine whether this Client has authority over it, and send it either to the PlayerArea or EnemyArea, accordingly. For the latter, flip it so the player can't see the front!
        if (type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform);
                card.GetComponent<CardFlipper>().Flip();
            }
        }
        //if the card has been "Played," send it to the DropZone. If this Client doesn't have authority over it, flip it so the player can now see the front!
        else if (type == "Played" && !string.IsNullOrEmpty(dropZoneName))
        {
            GameObject targetZone = GameObject.Find(dropZoneName);
            if (targetZone != null)
            {
                card.transform.SetParent(targetZone.transform, false);
                if (!hasAuthority)
                {
                    card.GetComponent<CardFlipper>().Flip();
                }
            }
        }
    }

    //CmdTargetSelfCard() is called by the TargetClick script if the Client hasAuthority over the gameobject that was clicked
    [Command]
    public void CmdTargetSelfCard()
    {
        TargetSelfCard();
    }

    //CmdTargetOtherCard is called by the TargetClick script if the Client does not hasAuthority (err...haveAuthority?!?) over the gameobject that was clicked
    [Command]
    public void CmdTargetOtherCard(GameObject target)
    {
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        TargetOtherCard(opponentIdentity.connectionToClient);
    }

    //TargetRpcs are methods requested by the Server to run on a target Client. If no NetworkConnection is specified as the first parameter, the Server will assume you're targeting the Client that hasAuthority over the gameobject
    [TargetRpc]
    void TargetSelfCard()
    {
        Debug.Log("Targeted by self!");
    }

    [TargetRpc]
    void TargetOtherCard(NetworkConnection target)
    {
        Debug.Log("Targeted by other!");
    }

    //CmdIncrementClick() is called by the IncrementClick script
    [Command]
    public void CmdIncrementClick(GameObject card)
    {
        RpcIncrementClick(card);
    }

    //RpcIncrementClick() is called on all clients to increment the NumberOfClicks SyncVar within the IncrementClick script and log it to the debugger to demonstrate that it's working
    [ClientRpc]
    void RpcIncrementClick(GameObject card)
    {
        card.GetComponent<IncrementClick>().NumberOfClicks++;
        Debug.Log("This card has been clicked " + card.GetComponent<IncrementClick>().NumberOfClicks + " times!");
    }
}
