using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;

    public Transform Boss { get; set; }
    public Transform Player { get; set; }
    
    private void Awake()
    {
        Boss = GameObject.Find("Boss").transform;
        Player = playableCharacter.transform;
        Camera2D.Instance.Target = playableCharacter.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveCharacter();
        }
    }

    private void ReviveCharacter()
    {
        if (playableCharacter.GetComponent<Health>().CurrentHealth <= 0)
        {
            playableCharacter.GetComponent<Health>().Revive();
            playableCharacter.transform.position = spawnPosition.position;
        }
    }
}
