using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

	public static int EnemiesAlive = 0;
	public Wave[] waves;

	public Transform spawnPoint;

	public Text waveCountdownText;

	public GameManager gameManager;

	private int waveIndex = 0;

 void Update ()
    {        
        if (EnemiesAlive > 0)
		{
			return;
		}

        if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}

        if (PlayerStats.RocksRemaining <= 0)
        {
            StartCoroutine(SpawnWave());
			return;
        }
    }

	IEnumerator SpawnWave ()
	{
		Debug.Log("Spawned a Wave of Enemies");

		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1 / wave.rate);
		}
		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}