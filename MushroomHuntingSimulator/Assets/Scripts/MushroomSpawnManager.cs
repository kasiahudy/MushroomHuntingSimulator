using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Mushroom[] _mushroomPrefabs;
    [SerializeField]
    private Transform _rootPrefab;
    [SerializeField]
    private int numberOfMushrooms;

    private List<Mushroom> mushrooms = new List<Mushroom>();
    private float circleRadius = 250f;

    public List<Mushroom> GetMushrooms()
    {
        return mushrooms;
    }

    public void SpawnMushrooms()
    {
        mushrooms.Clear();

        for (int i = 0; i < numberOfMushrooms / _mushroomPrefabs.Length; i++)
        {
            foreach (Mushroom mushroomPrefab in _mushroomPrefabs)
            {
                if (Random.value < mushroomPrefab.GetSpawnProbability())
                {
                    Vector3 rndPos = Random.insideUnitSphere * circleRadius + _rootPrefab.position;
                    rndPos.y = 0.0f;

                    Mushroom mushroom = Instantiate<Mushroom>(mushroomPrefab, rndPos, Quaternion.identity, _rootPrefab);
                    mushrooms.Add(mushroom);
                }
            }
        }
    }
}
