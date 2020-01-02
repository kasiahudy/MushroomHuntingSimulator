using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollectionKeyboardAndMouseController : MonoBehaviour
{
    [SerializeField]
    private string collectableMushroomTag = "Mushroom";
    [SerializeField]
    private float collectionDistance = 1.0f;
    [SerializeField]
    private KeyCode collectKey = KeyCode.Space;
    [SerializeField]
    private Camera playerCamera;

    private Transform current;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, collectionDistance))
        { 
            if (hit.transform.CompareTag(collectableMushroomTag))
            {
                if (current == null)
                {
                    current = hit.transform;
                    AddHighlightToCurrent();
                }

                if (current != hit.transform)
                {
                    RemoveHighlightFromCurrent();
                    current = hit.transform;
                    AddHighlightToCurrent();
                }

                if (Input.GetKeyUp(collectKey))
                {
                    hit.transform.gameObject.GetComponent<Mushroom>().Collect();
                }
            }
        }
        else
        {
            RemoveHighlightFromCurrent();
            current = null;
        }
            
    }

    private void AddHighlightToCurrent()
    {
        current.gameObject.GetComponent<Mushroom>().AddHighlightForCollection();
    }

    private void RemoveHighlightFromCurrent()
    {
        current.gameObject.GetComponent<Mushroom>().RemoveHighlightForCollection();
    }
}
