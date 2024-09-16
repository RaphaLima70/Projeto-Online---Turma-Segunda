using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;

[Serializable]
public class IntEvent : UnityEvent<int> { }

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 10;
    [SyncVar]
    public int coins;
    [SyncVar]
    public Color playerColor;
    public IntEvent OnCoinCollect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = playerColor;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddPlayerListener(this);
    }

    [ClientRpc]
    void TalkToAll()
    {
        Debug.Log("E aí galerinha!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pedindo uma mensagem para o Server!");
            TalkToAll();
        }


        if (isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            if (inputX != 0 && inputY != 0)
            {
                speed = 5;
            }
            else
            {
                speed = 10;
            }

            rb.velocity = new Vector2(inputX, inputY) * speed;
        }
    }

    [Command]
    public void AddCoins()
    {
        coins += 1;
        OnCoinCollect.Invoke(coins);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AddCoins();
            MyNetworkManager.spawnedCoins--;
            Destroy(other.gameObject);
        }
    }
}