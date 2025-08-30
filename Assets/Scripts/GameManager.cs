using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Player playerPrefab;
    
    private Player player;
    
    public static GameManager Singleton;

    public Action OnGameEnd;
    public Action<Player> OnPlayerSpawn;
    public RectTransform playerMinBounds;
    public RectTransform playerMaxBounds;

    private void Awake()
    {
        if (Singleton != null)
        {
            Debug.LogError("There's another Game Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        playerUI.SetActive(true);

        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        OnPlayerSpawn?.Invoke(player);
        player.health.OnHealthZero += EndGame;

        
        enemyManager.StartSpawnEnemiesCoroutine();
    }

    void EndGame()
    {
        OnGameEnd?.Invoke();    // useless for now

        ScoreManager.Instance.RegisterHighestScore();

        enemyManager.StopSpawnEnemiesCoroutine();

        playerUI.SetActive(false);
        endMenu.SetActive(true);
    }

    public void StartMenu()
    {
        endMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public Player GetPlayerReference()
    {
        return player;
    }
}
