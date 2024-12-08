using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class AttackButton : MonoBehaviour
{
    public GameObject playManager;


    void Start()
    {
        playManager = GameObject.Find("PlayManager");

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerUp);
    }

    void OnPointerDown(PointerEventData data)
    {
        if ( PlayManager.isOver )
            return;

        GameObject spaceship = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = spaceship.GetComponent<PlayerController>();
        playerController.ButtonAttackDown();
    }

    void OnPointerUp(PointerEventData data)
    {
        if ( PlayManager.isOver )
            return;        

        GameObject spaceship = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = spaceship.GetComponent<PlayerController>();
        playerController.ButtonAttackUp();
    }
}