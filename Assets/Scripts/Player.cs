using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public Vector2 inputVec;
    public float speed = 3f;
    public float jumpForce = 30f;
    public float maxSpeed = 300f;
    Vector3 mousePos;
    Animator anim;
    bool isJumping = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

        inputVec.y = 0f;
    }
    public void OnFire()
    {
        if (isJumping)
            return;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    private void FixedUpdate()
    {
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // 아래방향으로 초록색 레이 그림

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

            if (rayHit.collider)
            {
                if (rayHit.distance < 0.7f)
                    isJumping = false;
            }
            else
                isJumping = true;
        }
    }
    private void Update()
    {
        GetMousePos();

        rigid.velocity = inputVec * speed + new Vector2(0f,Mathf.Clamp(rigid.velocity.y, -maxSpeed, maxSpeed));
        transform.localScale = new Vector3((rigid.position.x - mousePos.x > 0 ? 1f : -1f), 1f, 1f);
    }
    private void LateUpdate()
    {
        Animate();
    }
    void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Animate()
    {
        anim.SetBool("IsMoving", (inputVec.x != 0));
    }
}
