using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float[] spawnTime; // ���� �ֱ�
    int currentWave = 0; // ���� ���̵�
    int stack = 0; // ���̵� ����(���� 5 ������ 1�ܰ� �ø���?)
    public Transform[] spawnPos; // ���� ���
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
