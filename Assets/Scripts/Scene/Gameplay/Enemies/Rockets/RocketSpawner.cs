using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private List<BaseRocket> rocketPrefabs = new List<BaseRocket>();

    [Tooltip("Transform where rockets will spawn from, using boss location")]
    [SerializeField] private Transform spawnLocation;

    [SerializeField] private List<CharacterData> rocketTargets;

    [SerializeField] private CharacterSpawner characterSpawner;

    private bool isSpawningRockets = false;

    private void OnEnable()
    {
        EventManager.StartListening("LockingPlayer", SpawnRocketAtTarget);
    }

    private void OnDisable()
    {
        EventManager.StopListening("LockingPlayer", SpawnRocketAtTarget);
    }

    private void Start()
    {
        rocketTargets = characterSpawner.SpawnedCharacters;
    }

    //private void SpawnRocketAtTarget(object message)
    //{
    //    bool canSpawn = (bool)message;

    //    isSpawningRockets = canSpawn;

    //    if (!isSpawningRockets)
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < spawnQty; i++)
    //    {
    //        ItemRocket rocket = Instantiate(rocketPrefab, spawnLocation.position, rocketPrefab.gameObject.transform.rotation);
    //        if (i < 2)
    //        {
    //            rocket.LaunchRocket(true, rocketTargets[i]);
    //        }
    //        else
    //        {
    //            rocket.ThrowRocket();
    //        }

    //    }
    //}
    private void SpawnRocketAtTarget(object message)
    {
        bool canSpawn = (bool)message;

        isSpawningRockets = canSpawn;

        if (!isSpawningRockets)
        {
            return;
        }

        for (int i = 0; i < rocketPrefabs.Count; i++)
        {
            BaseRocket rocket = Instantiate(rocketPrefabs[i], spawnLocation.position, rocketPrefabs[i].gameObject.transform.rotation);
            if (i < 2)
            {
                rocket.GetComponent<ExplosiveRocket>().LaunchRocket(rocketTargets[i]);
            }
            else
            {
                rocket.GetComponent<PickableRocket>().ThrowRocket();
            }
        }
    }
}
