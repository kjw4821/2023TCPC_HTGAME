using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float[] spawnTime; // 스폰 주기
    int currentWave = 0; // 현재 난이도
    int stack = 0; // 난이도 스택(대충 5 쌓으면 1단계 올리죠?)
    public Transform[] spawnPos; // 스폰 장소
    float time = 0f;

    
    private void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime[currentWave])
        {
            time = 0f;
            stack++;
            Spawn();
        }
        if(stack >= 5)
        {
            stack = 0;
            currentWave++;
        }
    }

    void Spawn()
    {
        GameObject obj = PoolManager.instance.GetObj(Random.Range(0, 2));
        obj.transform.position = spawnPos[Random.Range(0, spawnPos.Length)].position;
    }
}
