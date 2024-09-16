using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class HUD : NetworkBehaviour
{
    public TextMeshProUGUI textCoinsP1;
    public TextMeshProUGUI textCoinsP2;

    Player player1;
    Player player2;

    [ClientRpc]
    public void UpdatePlayer1Coins(int coins)
    {
        textCoinsP1.text = coins.ToString();
    }

    [ClientRpc]
    public void UpdatePlayer2Coins(int coins)
    {
        textCoinsP2.text = coins.ToString();
    }

    public void AddPlayerListener(Player player)
    {
        if (player1 == null)
        {
            player1 = player;
            player1.OnCoinCollect.AddListener(UpdatePlayer1Coins);
        }
        else
        {
            player2 = player;
            player2.OnCoinCollect.AddListener(UpdatePlayer2Coins);
        }
    }
}
