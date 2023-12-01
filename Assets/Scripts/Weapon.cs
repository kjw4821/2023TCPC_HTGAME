using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Shield;

    Vector2 lookPos;
    Vector2 mousePos;

    void Update()
    {
        transform.localScale = new Vector3(-1f, (lookPos.x > 0 ? 1f : -1f), 1f);

        transform.position = GameManager.instance.player.transform.position ;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookPos = mousePos - GameManager.instance.player.rigid.position;

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg);
    }

    private void LateUpdate()
    {
        Shield.SetActive(GameManager.instance.player.isGuarding);
    }
}
