using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { Arrow, Magic}
    public BulletType bulletType;
    public float bulletSpeed;

    public bool hitBox = false;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        hitBox = false;
    }

    public void Shoot(Vector2 destination)
    {
        rigid.velocity = Vector2.zero;
        switch (bulletType)
        {
            case BulletType.Arrow:
                StartCoroutine(ShootingArrow(destination));
                break;
            case BulletType.Magic:
                StartCoroutine(ShootingMagic(destination));
                break;
        }
    }
    
    IEnumerator ShootingArrow(Vector2 destination)
    {
        hitBox = true;
        Vector2 targetDir = (destination - rigid.position).normalized;
        rigid.velocity = targetDir * bulletSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90);
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);
    }
    IEnumerator ShootingMagic(Vector2 destination)
    {
        this.rigid.position = new Vector2(Random.Range(destination.x - 5f, destination.x + 5f), Random.Range(destination.y - 5f, destination.y + 5f)); // �÷��̾� �ݰ� 5 �̳� ������ ������ ������ ����
        yield return new WaitForSeconds(1.5f);
        hitBox = true;
        rigid.velocity = (GameManager.instance.player.rigid.position - rigid.position).normalized * bulletSpeed; // 1.5�� �� ���� �ӵ��� �÷��̾�� ���ư�.
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    private void Hit()
    {
        GameManager.instance.player.currentHp--;
        if(GameManager.instance.player.currentHp <= 0)
        {
            GameManager.instance.player.StartCoroutine("Dead");
        }
        Debug.Log("�ƾ�");
        GameManager.instance.hud.CutLife();
        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
            Blocked();
    }
    void Blocked()
    {
        GameManager.instance.player.isGuarding = false;
        GameManager.instance.player.StartCoroutine("KnockBack");
        this.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && hitBox)
        {
            Hit();
            hitBox = false;
        }
    }
}
