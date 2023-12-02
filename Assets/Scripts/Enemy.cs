using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    public EnemyData data;

    EnemyData.Enemies enemy;
    float maxHp;
    [SerializeField]
    float hp;
    float cooltime;
    bool cooldown = false;
    int weaponId;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        enemy = data.enemy;
        maxHp = data.hp;
        cooltime = data.cooltime;
        weaponId = data.weaponId;

        hp = maxHp;
        cooldown = false;
    }

    private void Update()
    {
        if(!cooldown)
            StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        switch(enemy)
        {
            case EnemyData.Enemies.Archer:
            case EnemyData.Enemies.Magician:
                GameObject obj = PoolManager.instance.GetObj(weaponId);
                obj.GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position;
                obj.GetComponent<Bullet>().Shoot(GameManager.instance.player.transform.position);
                break;
            case EnemyData.Enemies.Warrior:
                break;
        }

        yield return new WaitForSeconds(cooltime);
        cooldown = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("AttackRange") && GameManager.instance.player.isAttacking)
        {
            GameManager.instance.player.isAttacking = false;
            hp--;
            if (hp <= 0)
            {
                StartCoroutine("Dead");
            }
        }

    }

    IEnumerator Dead ()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        GameManager.instance.score++;
        this.gameObject.SetActive(false);
    }
}
