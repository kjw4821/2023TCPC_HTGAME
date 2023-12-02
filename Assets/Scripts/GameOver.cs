using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        text.text = GameManager.instance.score.ToString();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
