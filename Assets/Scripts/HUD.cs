using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject[] lifes;
    public Text score;
    private void Awake()
    {

        foreach(GameObject life in lifes)
        {
            life.SetActive(true);
        }
    }

    public void CutLife()
    {
        for(int i = lifes.Length - 1; i >= 0; i--)
        {
            if (lifes[i].activeSelf)
            {
                lifes[i].SetActive(false);
                break;
            }
        }
    }

    private void LateUpdate()
    {
        score.text = GameManager.instance.score.ToString();
    }
}
