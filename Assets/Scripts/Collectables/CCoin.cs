using UnityEngine;

public class CCoin : Colletables
{
    [SerializeField] private int coinsToAdd = 20;
    
    protected override void Pick()
    {
           AddCoins();
    }

    private void AddCoins()
    {
        CoinManager.Instance.AddCoins(coinsToAdd);
    }

    protected override void PlayEffects()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.CoinClip, 0.6f);
    }
}