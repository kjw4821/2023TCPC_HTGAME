using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public HUD hud;
    public static GameManager instance;

    public int score = 0;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
}
