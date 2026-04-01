using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnTemplate = null;

    private void OnEnable()
    {
        MineSpawnManager.Instance.RegisterMineSpawnPoint(this);
    }

    private void OnDisable()
    {
        //this could be called when the game shuts down and all objects are destroyed, so check if the SpawnManager still exists to avoid recreating it
        if (MineSpawnManager.Exists)
            MineSpawnManager.Instance.UnRegisterSpawnPoint(this);
    }

    public GameObject Spawn()
    {
        return Instantiate(_spawnTemplate, transform.position, transform.rotation);
    }
}
