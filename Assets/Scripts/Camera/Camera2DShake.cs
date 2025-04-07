using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Camera2DShake : MonoBehaviour
{
    [SerializeField] private float shakeVibrato = 10f;
    [SerializeField] private float shakeRandomness = 0.1f;
    [SerializeField] private float shakeTime= 0.01f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shake();
        }
    }

    public void Shake()
    {
        StartCoroutine(IEShake());
    }

    private IEnumerator IEShake()
    {
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < shakeVibrato; i++)
        {
            Vector3 shakePosition = currentPosition + Random.onUnitSphere * shakeRandomness;
            yield return new WaitForSeconds(shakeTime);
            transform.position = shakePosition;
        }
    }

    private void OnShooting()
    {
       Shake();
    }
    
    private void OnEnable()
    {
        CharacterWeapon.OnStartShooting += OnShooting;
    }
    
    private void OnDisable()
    {
        CharacterWeapon.OnStartShooting -= OnShooting;
    }
}
