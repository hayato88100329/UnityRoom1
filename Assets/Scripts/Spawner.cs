using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab = null;
    [SerializeField]
    int spawnCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < spawnCount; i++)
        {
            Instantiate(prefab);
        }

    }

}
