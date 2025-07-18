using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isAlive;
    public float gameTime;
    public float maxGameTime = 2 * 10f; 

    [Header("# Player Info")]
    public int playerId; 
    public float health; 
    public float maxHealth = 100; 
    public int level; 
    public int kill; 
    public int exp; 
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; 

    [Header("# Game Object")]
    public PoolManager pool; 
    public Player player; 
    public LevelUp uiLevelUp; 
    public Result uiResult;
    public Transform uiJoy;
    public GameObject enemyCleaner; 

    private void Awake()
    {
        instance = this;
    }


    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume(); 

        AudioManager.instance.PlayBgm(true); 
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select); 
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        isAlive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose(); 
        Stop(); 

        AudioManager.instance.PlayBgm(false); 
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose); 
    }

  
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    private IEnumerator GameVictoryRoutine()
    {
        isAlive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false); 
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

 
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

  
    private void Update()
    {
        if (!isAlive)
            return;

       
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0); 

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory(); 
        }
    }


    public void GetExp()
    {
        if (!isAlive)
            return;

        exp++; 

    
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++; 
            exp = 0; 
            uiLevelUp.Show(); 
        }
    }


    public void Stop()
    {
        isAlive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

  
    public void Resume()
    {
        isAlive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }
}
