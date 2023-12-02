using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Vector2 inputVec;
    public float speed = 3f;
    public float jumpForce = 30f;
    public float maxSpeed = 300f;
    public int maxHp = 3;
    public int currentHp;
    public GameObject attackRange;

    public bool isGuarding = false; // 방어상태
    public bool isAttacking = false; // 공격상태
    public bool isKnockDown = false; // 경직상태

    Vector3 mousePos;
    public Animator anim;
    public bool isJumping = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        currentHp = maxHp;
    }

    public void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        inputVec.y = 0f;
    }
    public void OnFire()
    {
        if (isJumping || isKnockDown)
            return;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }
    public void OnAttack()
    {
        if (isGuarding || isKnockDown)
            return;
        anim.SetTrigger("Attack");
        StartCoroutine("Attack");
    }
    private void FixedUpdate()
    {
        if (GetComponent<PlayerInput>().actions["Guard"].IsPressed() && !isGuarding)
            StartCoroutine("Guard"); // 가드 홀딩 여부 확인(0.5초 딜레이)
        else if (!GetComponent<PlayerInput>().actions["Guard"].IsPressed()) // 가드 안하고 있으면
            isGuarding = false;

        if (!isKnockDown)
        {
            if (isGuarding)
                rigid.velocity = new Vector2(0f, Mathf.Clamp(rigid.velocity.y, -maxSpeed, 100f));
            else
                rigid.velocity = inputVec * speed + new Vector2(0f, Mathf.Clamp(rigid.velocity.y, -maxSpeed, 100f));
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1.5f, 0)); // 아래로 초록색 레이 그리기

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("Ground"));

            if (rayHit.collider)
            {
                if (rayHit.distance < 1.5f)
                {
                    isJumping = false;
                }
            }
            else
                isJumping = true; 
        }
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 계산

    }
    private void LateUpdate()
    {
        Animate();
        transform.localScale = new Vector3((rigid.position.x - mousePos.x > 0 ? 1f : -1f), 1f, 1f);
    }

    void Animate()
    {
        anim.SetBool("IsMoving", (inputVec.x != 0 && !isGuarding));
        anim.SetBool("IsGuarding", isGuarding);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }
    
    IEnumerator Guard()
    {
        rigid.velocity = Vector2.zero + new Vector2(0f, rigid.velocity.y);
        yield return new WaitForSeconds(0.2f);
        isGuarding = true;
    }

    IEnumerator KnockBack()
    {
        isKnockDown = true;
        yield return new WaitForSeconds(0.4f);
        isKnockDown = false;
    }

    IEnumerator Dead()
    {
        anim.SetTrigger("IsDead");
        isKnockDown = true;
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");
    }
}
