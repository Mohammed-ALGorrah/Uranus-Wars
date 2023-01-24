using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnerManager : NetworkBehaviour
{
    public Transform TowerSpawnPoint;
    bool canInstant = true;

    public void SpawnTower(GameObject tower)
    {
        if (canInstant)
        {
            Runner.Spawn(tower, TowerSpawnPoint.position, Quaternion.LookRotation(TowerSpawnPoint.forward, TowerSpawnPoint.up), Object.StateAuthority, (runner, gameO) =>{
                gameO.GetComponent<MoveObjects>().platform = TowerSpawnPoint;
            });

        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.GetComponent<Tower>() != null)
        {
            canInstant = false;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.GetComponent<Tower>() != null)
        {
            canInstant = true;
        }
    }
}
