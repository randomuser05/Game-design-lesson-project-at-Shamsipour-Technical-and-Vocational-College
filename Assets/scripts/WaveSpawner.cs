using UnityEngine;
using TMPro; // برای استفاده از TextMeshPro
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // آرایه 4 پریفب دشمن
    [SerializeField] private Transform[] spawnerStatues; // آرایه 4 مجسمه اسپاونر دشمن
    [SerializeField] private Transform healStatue; // مجسمه Heal
    [SerializeField] private GameObject healItemPrefab; // پریفب آیتم Heal
    [SerializeField] private float waveDelay = 10f; // تاخیر 10 ثانیه بین ویوها
    [SerializeField] private TextMeshProUGUI waveText; // کامپوننت TextMeshPro برای نمایش شماره ویو

    private int currentWave = 0; // شماره ویو فعلی
    private List<GameObject> activeEnemies = new List<GameObject>(); // لیست دشمنان فعال

    void Start()
    {
        UpdateWaveText();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            currentWave++;
            UpdateWaveText(); // به‌روزرسانی متن ویو
            int enemiesToSpawn = currentWave; // تعداد دشمنان برابر با شماره ویو

            // اسپاون دشمنان
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                // انتخاب تصادفی یک پریفب دشمن
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                // انتخاب تصادفی یک مجسمه اسپاونر
                Transform spawnPoint = spawnerStatues[Random.Range(0, spawnerStatues.Length)];

                // اسپاون دشمن
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                activeEnemies.Add(enemy);

                // صبر کوتاه بین اسپاون هر دشمن
                yield return new WaitForSeconds(0.5f);
            }

            // منتظر مرگ تمام دشمنان
            while (activeEnemies.Count > 0)
            {
                activeEnemies.RemoveAll(enemy => enemy == null); // حذف دشمنان مرده
                yield return null; // صبر تا فریم بعدی
            }

            // دراپ آیتم از مجسمه Heal
           // Instantiate(healItemPrefab, healStatue.position, healStatue.rotation);

            // صبر 10 ثانیه قبل از ویو بعدی
            yield return new WaitForSeconds(waveDelay);
        }
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
    }
}