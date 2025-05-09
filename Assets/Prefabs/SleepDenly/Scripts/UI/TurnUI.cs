using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI endTurnButtonText; // Add this line
    [SerializeField] private Image endTurnButtonImage;  // Add this
    [SerializeField] private Sprite playerTurnSprite;   // Add this
    [SerializeField] private Sprite opponentTurnSprite; // Add this
    [SerializeField] private Button endGameButton;
    [SerializeField] private TextMeshProUGUI VictoryText; // Fix type name
    [SerializeField] private Image VictoryTextBackground; // Add this
    private TurnManager turnManager;
    private PlayerScore playerScore;

    void Start()
    {
        // change later
        playerScore = FindObjectOfType<PlayerScore>();
        
        turnManager = TurnManager.Instance;
        endTurnButton.onClick.AddListener(OnEndTurnClicked);
        turnManager.OnTurnChanged += OnTurnChanged;
        
        // Get button text component if not assigned
        if (endTurnButtonText == null)
        {
            endTurnButtonText = endTurnButton.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        if (endTurnButtonImage == null)
        {
            endTurnButtonImage = endTurnButton.GetComponent<Image>();
        }
        
        UpdateTurnText();
        UpdateEndTurnButtonText();
        UpdateButtonAppearance(TurnManager.TurnState.PlayerTurn);
        endTurnButton.interactable = true; // Make button always interactable

        if (endGameButton != null)
        {
            endGameButton.onClick.AddListener(OnEndGameButtonClicked);
        }
        
        if (VictoryText != null)
        {
            VictoryText.gameObject.SetActive(false);
        }

        if (VictoryTextBackground != null)
        {
            VictoryTextBackground.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (turnManager != null)
        {
            turnManager.OnTurnChanged -= OnTurnChanged;
        }
    }

    void Update()
    {
        if (turnManager != null)  // Always update timer if TurnManager exists
        {
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(turnManager.RemainingTime);
        timerText.text = $"Time: {seconds}s";
    }

    void UpdateTurnText()
    {
        if (turnManager != null)
        {
            turnText.text = turnManager.currentTurn == TurnManager.TurnState.PlayerTurn 
                ? "Your Turn" 
                : "Opponent's Turn";
            Debug.Log($"Turn text updated to: {turnText.text}");
        }
    }

    void UpdateEndTurnButtonText()
    {
        if (endTurnButtonText != null && turnManager != null)
        {
            endTurnButtonText.text = turnManager.currentTurn == TurnManager.TurnState.PlayerTurn 
                ? "End Your Turn" 
                : "End Opponent Turn";
        }
    }

    void UpdateButtonAppearance(TurnManager.TurnState turnState)
    {
        if (endTurnButtonImage != null)
        {
            if (turnState == TurnManager.TurnState.PlayerTurn)
            {
                endTurnButtonImage.sprite = playerTurnSprite;
                endTurnButtonImage.color = Color.white; // Full opacity
            }
            else
            {
                endTurnButtonImage.sprite = opponentTurnSprite;
                Color fadeColor = Color.white;
                fadeColor.a = 0.6f; // 20% opacity
                endTurnButtonImage.color = fadeColor;
            }
        }
    }

    void OnTurnChanged(TurnManager.TurnState newTurn)
    {
        UpdateTurnText();
        UpdateTimerText();
        UpdateEndTurnButtonText();
        UpdateButtonAppearance(newTurn);
        // Remove the button interactable setting here since we want it always enabled
    }

    void OnEndTurnClicked()
    {
        if (turnManager.currentTurn == TurnManager.TurnState.PlayerTurn)
        {
            turnManager.EndPlayerTurn();
        }
        else if (turnManager.currentTurn == TurnManager.TurnState.EnemyTurn)
        {
            turnManager.EndEnemyTurn();
        }
        UpdateTurnText();
    }

    private void OnEndGameButtonClicked()
    {
        if (VictoryText != null)
        {
            VictoryText.gameObject.SetActive(true);
            VictoryText.text = "Victory!";

            // Fix the assignment operator
            playerScore.score = playerScore.maxScore;
        }

        if (VictoryTextBackground != null)
        {
            VictoryTextBackground.gameObject.SetActive(true);
        }
    }
}
