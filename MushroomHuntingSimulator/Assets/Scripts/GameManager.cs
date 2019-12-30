using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Mushroom[] _mushrooms;

    [SerializeField]
    Transform _root;

    float circleRadius = 250f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMushrooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMushrooms()
    {

        for (int i = 0; i < 100; i++)
        {
            foreach (Mushroom mushroom in _mushrooms)
            {
                if (Random.value < mushroom.spawnProbability)
                {
                    Vector3 rndPos = Random.insideUnitSphere * circleRadius + _root.position;
                    rndPos.y = 0.227f;

                    Mushroom prefab = mushroom;
                    Instantiate<Mushroom>(prefab, rndPos, Quaternion.identity, _root);
                }
            }
        }
            


    }
}
