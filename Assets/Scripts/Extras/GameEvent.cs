using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action<EventType> OnEventFired;
    
    public enum EventType
    {
        BossFight
    }

    [SerializeField] private EventType eventType;
    [SerializeField] private LayerMask eventLayer;

    private bool eventFired;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary.CheckLayer(other.gameObject.layer, eventLayer))
        {
            if (!eventFired)
            {
                OnEventFired?.Invoke(eventType);
                eventFired = true;
            }
        }
    }
}
