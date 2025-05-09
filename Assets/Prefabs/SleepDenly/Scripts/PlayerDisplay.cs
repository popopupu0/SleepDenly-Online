using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this line

public class PlayerDisplay : MonoBehaviour
{
    public PlayerData playerData; // Reference to PlayerData
    public TMP_Text playerNameText; // UI Text to display player's names
    public Image playerAvatarImage; // UI Image to display player's avatar

    // Start is called before the first frame update
    void Start()
    {
        if (playerData != null)
        {
            playerNameText.text = playerData.playerName;
            playerAvatarImage.sprite = playerData.playerAvatar;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
