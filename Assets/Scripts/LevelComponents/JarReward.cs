using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarReward : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float xRandomPosition = 2f;
    [SerializeField] private float yRandomPosition = 2f;
    [SerializeField] [Range(0,100)] private float chanceToDrop = 50f;

    [Header("Rewards")]
    [SerializeField] private GameObject[] rewards;

    private Vector3 rewardRandomPosition;
    
    public void GiveReward()
    {
        rewardRandomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
        rewardRandomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);

        float probability = Random.Range(0, 100);
        if (probability > chanceToDrop)
        {
            Instantiate(SelectReward(), transform.position + rewardRandomPosition, Quaternion.identity);
        }
    }

    private GameObject SelectReward()
    {
        int randomRewardIndex = Random.Range(0, rewards.Length);
        for (int i = 0; i < rewards.Length; i++)
        {
            return rewards[randomRewardIndex];
        }

        return null;
    }
}
