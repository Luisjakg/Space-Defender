using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private bool spawn = true;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private Asteroid attackerPrefab;
    
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
        
    }

    private void SpawnAttacker()
    {

        //FindObjectOfType<Asteroid>().Randomize();
        Asteroid newAttacker = Instantiate
                (attackerPrefab, transform.position, transform.rotation) 
            as Asteroid;
        newAttacker.transform.parent = transform;
    }


}
