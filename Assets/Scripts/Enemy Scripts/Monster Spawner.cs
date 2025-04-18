using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsterReference;

    GameObject spawnedMonster;

    [SerializeField]
    Transform leftPos,
        rightPos;

    int randomIndex;
    int randomSide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    // Update is called once per frame
    // void Update() { }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            if (randomSide == 0) // Left
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<Monsters>().speed = Random.Range(4, 10);
            }
            else // Right
            {
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<Monsters>().speed = -Random.Range(4, 10);
                spawnedMonster.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
        }
    }
} //Class
