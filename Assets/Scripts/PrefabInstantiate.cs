using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _roadlinePrefab;
    [SerializeField] private GameObject _electricPolePrefab;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private GameObject _empty;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject[] _roadObstacles = new GameObject[14];
    [SerializeField] private GameObject[] _trees = new GameObject[5];
    [SerializeField] private GameObject[] _mountains = new GameObject[2];

    [SerializeField] private bool _obstacleSpawning;
    [SerializeField] private bool _obstacleSideSpawning;
    [SerializeField] private bool _bossSpawning;

    [SerializeField] private float _worldSpeedCorrection;
    private float _roadSideLeft = -3.8f;
    private float _roadSideRight = 3.8f;

    [SerializeField] private float _boxMinSpawnRate;
    [SerializeField] private float _boxMaxSpawnRate;

    [SerializeField] private float _randomSpawnTime;

    private GlobalObjectMoving _globalObjectMoving;
    private GameObject _handler;

    public void SetBoxSpawnRate(float MinRate, float MaxRate)
    {
        _boxMinSpawnRate = MinRate;
        _boxMaxSpawnRate = MaxRate;
    }

    private void InstantiateRoadline()
    {
        GameObject.Instantiate(_roadlinePrefab, _roadlinePrefab.transform);
    }

    private void InstantiateElectricPole()
    {
        GameObject.Instantiate(_electricPolePrefab, _electricPolePrefab.transform);
        GameObject.Instantiate(_electricPolePrefab, new Vector3(-_electricPolePrefab.transform.position.x, _electricPolePrefab.transform.position.y, _electricPolePrefab.transform.position.z), _electricPolePrefab.transform.rotation);
    }

    private void InstantiateTree()
    {
        _randomSpawnTime = Random.Range(0.08f, 0.15f);
        int randomPrefab = Random.Range(0, _trees.Length);
        float randomSpawnX = Random.Range(-25f, -10.6f);
        if (Random.value < 0.5f)
            randomSpawnX = Random.Range(10.6f, 25.0f);

        GameObject.Instantiate(_trees[randomPrefab], new Vector3(randomSpawnX, 0f, 220f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)), null);
    }

    private void InstantiateMountain()
    {
        int randomPrefab = Random.Range(0, _mountains.Length);

        GameObject.Instantiate(_mountains[randomPrefab], new Vector3(-45f, 0f, 220f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
        GameObject.Instantiate(_mountains[randomPrefab], new Vector3(45f, 0f, 220f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
    }

    private void InstantiateRoad()
    {
        
    }

    private void InstantiateObstacle()
    {
        if (_obstacleSpawning)
        {
            _randomSpawnTime = Random.Range(_boxMinSpawnRate, _boxMaxSpawnRate);
            int randomPrefab = Random.Range(0, _roadObstacles.Length);
            float roadSpawnPosition = Random.Range(_roadSideLeft, _roadSideRight);

            GameObject.Instantiate(_roadObstacles[randomPrefab], new Vector3(roadSpawnPosition, 3f, 100f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 359f), 0)), null);
        }
    }

    private void InstantiateObstacleOnSides()
    {
        int randomPrefab = Random.Range(0, _roadObstacles.Length);
        GameObject.Instantiate(_roadObstacles[randomPrefab], new Vector3(_roadSideRight, 3f, 100f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 359f), 0)), null);
        GameObject.Instantiate(_roadObstacles[randomPrefab], new Vector3(_roadSideLeft, 3f, 100f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 359f), 0)), null);
    }

    private void SpawnBoss()
    {
        float roadSpawnPosition = Random.Range(_roadSideLeft, _roadSideRight);
        float randomValue = Random.value;

        if(randomValue > 0.1f)
        {
            GameObject.Instantiate(_bossPrefab, new Vector3(roadSpawnPosition, 3f, 200f), Quaternion.Euler(new Vector3(0, 180, 0)), null);

            if (randomValue > 0.5f)
            {
                GameObject.Instantiate(_bossPrefab, new Vector3(roadSpawnPosition, 3f, 200f), Quaternion.Euler(new Vector3(0, 180, 0)), null);

                if (randomValue > 0.8f)
                    GameObject.Instantiate(_bossPrefab, new Vector3(roadSpawnPosition, 3f, 200f), Quaternion.Euler(new Vector3(0, 180, 0)), null);
            }
        }
        else
        {
            GameObject.Instantiate(_bossPrefab, new Vector3(-3.4f, 3f, 200f), Quaternion.Euler(new Vector3(0, 180, 0)), null);
            GameObject.Instantiate(_bossPrefab, new Vector3(3.4f, 3f, 200f), Quaternion.Euler(new Vector3(0, 180, 0)), null);
        }
    }

    private void SpawnBossObstacles()
    {
        for (int i = 0; i < 18; i++)
        {
            int randomPrefab = Random.Range(0, _roadObstacles.Length);
            float roadSpawnPosition = Random.Range(_roadSideLeft, _roadSideRight);

            GameObject.Instantiate(_roadObstacles[randomPrefab], new Vector3(roadSpawnPosition, 3f, 200f), Quaternion.Euler(new Vector3(0, Random.Range(0f, 359f), 0)), null);
        }
    }
    
    private IEnumerator SpawnTree()
    {
        while(true)
        {
            InstantiateTree();
            yield return new WaitForSeconds(_randomSpawnTime);
        }
    }

    private IEnumerator SpawnObstacle()
    {
        while (_obstacleSpawning)
        {
            InstantiateObstacle();
            if (Random.value < 0.25)
                InstantiateObstacle();

            yield return new WaitForSeconds(_randomSpawnTime);
        }
    }

    void Start()
    {
        _handler = GameObject.Find("Handler");
        _globalObjectMoving = _handler.GetComponent<GlobalObjectMoving>();
        _worldSpeedCorrection = 30f / _globalObjectMoving.GetWorldSpeed();

        SpawnFirstTrees();
        InvokeRepeating("InstantiateRoadline", 0.5f * _worldSpeedCorrection, 0.5f * _worldSpeedCorrection);
        InvokeRepeating("InstantiateElectricPole", 1.3f * _worldSpeedCorrection, 1.3f * _worldSpeedCorrection);
        InvokeRepeating("InstantiateMountain", 0f * _worldSpeedCorrection, 1f * _worldSpeedCorrection); 

        if(_obstacleSideSpawning)
            InvokeRepeating("InstantiateObstacleOnSides", 0f, 1f);

        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnTree());

        if (_bossSpawning)
        {
            InvokeRepeating("SpawnBoss", 0f, 3f);
            InvokeRepeating("SpawnBossObstacles", 1.5f, 3f);
        }
    }

    void SpawnFirstTrees()
    {
        for (int i = -10; i < 220; i++)
        {
            int randomPrefab = Random.Range(0, _trees.Length);
            float randomSpawnX = Random.Range(-25f, -10.6f);
            if (Random.value < 0.5f)
                randomSpawnX = Random.Range(10.6f, 25.0f);
            GameObject.Instantiate(_trees[randomPrefab], new Vector3(randomSpawnX, 0f, i), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)), null);
            i += Random.Range(4, 10);
        }
    }
}