using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Header("Wave Info")]
    public int waveNum = 0;
    public int waveDelay = 20;
    public float timer = 0;

    public List<Enemy> enemies = new List<Enemy>();

    [Header("Enemy Types")]
    public GameObject[] enemyObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0) {
            timer += Time.deltaTime;
            if (timer >= waveDelay) {
                waveNum++;
                for (int i = 0; i < waveNum * 2; i++) {
                    enemies.Add(Instantiate(enemyObjects[Random.Range(0, 2)], new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), new Quaternion()).GetComponent<Enemy>());
                }
                timer = 0;
            }
        }
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null) {
                enemies.Remove(enemies[i]);
            }
        }
    }
}
