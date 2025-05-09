using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    
    public enum TurnState
    {
        PlayerTurn,
        EnemyTurn,
        GameOver
    }

    public TurnState currentTurn = TurnState.PlayerTurn;
    public int turnCount = 1;
    public bool isTurnInProgress = false;

    private const float TURN_TIME_LIMIT = 90f;
    private float currentTurnTime;
    public float RemainingTime { get { return TURN_TIME_LIMIT - currentTurnTime; } }
    public bool isTimerRunning = false;

    private HandManager handManager;
    private DeckManager deckManager;
    private const int MIN_CARDS_IN_HAND = 5;
    private bool hasDrawnInitialRegularCards = false;

    public delegate void TurnChanged(TurnState newTurn);
    public event TurnChanged OnTurnChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentTurn = TurnState.PlayerTurn;
            handManager = FindObjectOfType<HandManager>();
            deckManager = FindObjectOfType<DeckManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartPlayerTurn(); // Add this line to ensure the game starts with player turn
    }

    void Update()
    {
        if (isTimerRunning)  // Remove the player turn check so timer runs for both turns
        {
            currentTurnTime += Time.deltaTime;
            
            if (currentTurnTime >= TURN_TIME_LIMIT)
            {
                Debug.Log("Time's up!");
                if (currentTurn == TurnState.PlayerTurn)
                {
                    EndPlayerTurn();
                }
                else if (currentTurn == TurnState.EnemyTurn)
                {
                    EndEnemyTurn();
                }
            }
        }
    }

    public void StartPlayerTurn()
    {
        if (isTurnInProgress) return;
        
        isTurnInProgress = true;
        currentTurn = TurnState.PlayerTurn;
        currentTurnTime = 0f; // Reset timer
        isTimerRunning = true; // Start timer
        OnTurnChanged?.Invoke(currentTurn);
        Debug.Log($"Player Turn {turnCount} Started - {TURN_TIME_LIMIT} seconds remaining");
        
        // Enable player actions here (like drawing cards)
    }

    public void EndPlayerTurn()
    {
        if (currentTurn != TurnState.PlayerTurn) return;
        
        isTurnInProgress = false;
        isTimerRunning = false;

        // Draw regular cards on first turn end
        if (!hasDrawnInitialRegularCards)
        {
            deckManager.DrawRegularCards();
            hasDrawnInitialRegularCards = true;
        }
        else
        {
            CheckAndDrawCards();
        }

        OnTurnChanged?.Invoke(TurnState.EnemyTurn);
        StartCoroutine(StartEnemyTurn());
    }

    private IEnumerator StartEnemyTurn()
    {
        currentTurn = TurnState.EnemyTurn;
        currentTurnTime = 0f;
        isTimerRunning = true;  // Start timer for enemy turn
        Debug.Log($"Enemy Turn {turnCount} Started");

        // Remove the automatic turn end
        yield return null;
    }

    public void EndEnemyTurn()
    {
        isTimerRunning = false;
        CheckAndDrawCards();  // Add this line
        turnCount++;
        StartPlayerTurn();
    }

    private void CheckAndDrawCards()
    {
        if (handManager != null)
        {
            int currentCards = handManager.cardsInHand.Count;
            int cardsToDraw = MIN_CARDS_IN_HAND - currentCards;

            if (cardsToDraw > 0)
            {
                Debug.Log($"Drawing {cardsToDraw} cards to reach minimum of {MIN_CARDS_IN_HAND}");
                for (int i = 0; i < cardsToDraw; i++)
                {
                    handManager.DrawCard();
                }
            }
        }
    }

    public bool IsPlayerTurn()
    {
        return currentTurn == TurnState.PlayerTurn;
    }

    public void EndGame()
    {
        currentTurn = TurnState.GameOver;
        Debug.Log("Game Over");
    }
}
