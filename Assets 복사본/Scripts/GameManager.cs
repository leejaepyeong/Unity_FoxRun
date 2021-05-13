using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    public bool isGameOver = false;
    public bool isEmpty;

    public Text scoreText;
    public Text healthText;
    public GameObject gameoverUI;
    public GameObject WinUI;


    private int score = 0;
    public int health = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        scoreText.text = "Score : " + score;   
    }

    public void Score(int type)
    {
        if (type == 0)
            score += 50;

        else if (type == 1)
            score -= 30;

        if (score < 0)
            score = 0;

    }

    public void Dead()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }

    public void Win()
    {
        isGameOver = true;
        WinUI.SetActive(true);
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        healthText.text = "HP : " + health;

        if(health == 0)
        {
            player.Die();
            Dead();
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
