using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;

    public int level;
    public int kill;
    public int exp;
    public int coin;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Gmae Control")]
    public bool isLive;
    public float gameTime;
    public float gameSleepTime = 1 * 60f;
    public float maxGameTime = 1 * 90f; //1 minutes 30 seconds
    public Spawner spawner;

    [Header("# Gmae Object")]
    public PoolManager pool;
    public GameObject core;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    public GameObject merchant;
    public bool isGame;
    int currentRound = 0;
    public int round = 15;

    void Awake()
    {
        isGame = true;
        instance = this;    
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(1);
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        enemyCleaner.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        if(!isLive)
            return;

        gameTime += Time.deltaTime;
        
        if (gameTime > maxGameTime && isGame)
        {
            
            isGame = false;
            StartCoroutine(Sleep());
            currentRound++;
        } else if (currentRound > round)
        {
            GameVictory();
        }
        

    }

    public void GetExp()
    {
        if(!isLive)
            return;

        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    IEnumerator Sleep(){
        enemyCleaner.SetActive(true);
        merchant.SetActive(true);
        yield return new WaitForSeconds(gameSleepTime);
        isGame = true;
        enemyCleaner.SetActive(false);
        merchant.SetActive(false);
        gameTime = 0;
    }

}
