using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MushroomCollectionVRSetController : MonoBehaviour
{
    [SerializeField]
    private string collectableMushroomTag = "Mushroom";
    [SerializeField]
    private SteamVR_Action_Boolean collectMushroomPress;

    private GameObject currentMushroom;

    private void Update()
    {
        if (currentMushroom != null && CollectButtonPressed())
        {
            CollectCurrentMushroom();
            currentMushroom = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(collectableMushroomTag))
        {
            currentMushroom = other.gameObject;
            AddHighlightForCurrentMushroom();               
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentMushroom != null)
        {
            RemoveHighlightForCurrentMushroom();
            currentMushroom = null;
        }
    }

    private bool CollectButtonPressed()
    {
        return collectMushroomPress.state;
    }

    private void CollectCurrentMushroom()
    {
        currentMushroom.GetComponent<Mushroom>().Collect();
    }


    private void AddHighlightForCurrentMushroom()
    {
        currentMushroom.GetComponent<Mushroom>().AddHighlightForCollection();

    }

    private void RemoveHighlightForCurrentMushroom()
    {
        currentMushroom.GetComponent<Mushroom>().RemoveHighlightForCollection();
    }
}
