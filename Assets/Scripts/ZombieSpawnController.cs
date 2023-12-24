using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiePerWave = 5;
    public int currentZombiePerWave;
    public float spawnDelay = 0.5f; // Thời gian delay giữa mỗi lần sinh zombie

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Thời gian delay giữa mỗi wave

    public bool inCooldown;
    public float cooldownCounter = 0; // Đếm thời gian cooldown

    public List<Zombie> currentZombiesAlive;

    public GameObject zombiePrefab;

    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
        {
            // Sinh ngẫu nhiên zombie từ 4 hướng
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // get zombie script
            Zombie zombieScript = zombie.GetComponent<Zombie>();

            //track this zombie
            currentZombiesAlive.Add(zombieScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        List<Zombie> zombiesToRemove = new List<Zombie>();
        foreach (Zombie zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // Xóa zombie đã chết khỏi list
        foreach (Zombie zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        if (currentZombiesAlive.Count == 0 && inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }

        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }

    }
    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(waveCooldown);
        inCooldown = false;
        currentZombiePerWave += 3;
        StartNextWave();
    }
}

