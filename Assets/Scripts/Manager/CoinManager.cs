using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int Coins { get; set; }

    private readonly string COINS_KEY = "MyGame_MyCoins_DontCheat";
    
    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        Coins = PlayerPrefs.GetInt(COINS_KEY);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }
}
