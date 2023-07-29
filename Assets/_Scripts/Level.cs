using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    public int prepareTime = 3;
    public int timer = 31;
    public float setAttackCooldown = 0.5f;

    public static bool IsGameOver;
    public static bool IsGameStart;
    public static bool IsActiveCooldown;
    public static float LastCooldownTime;
    public static float AttackCooldown;
    public static int Points;

    private TMP_Text _pointsText;
    private TMP_Text _gameTimerText;
    private TMP_Text _prepareTimerText;
    private TMP_Text _endGameText;

    [Header("Canvas")]
    [SerializeField] private GameObject gameTimer;
    [SerializeField] private GameObject prepareTimer;
    [SerializeField] private GameObject endGame;
    
    void Start()
    {
        AttackCooldown = setAttackCooldown;
        _pointsText = GameObject.Find("PointsText").GetComponent<TMP_Text>();
        _prepareTimerText = GameObject.Find("PrepareTimerText").GetComponent<TMP_Text>();

        InvokeRepeating(nameof(PrepareTimer), 1, 1);
        InvokeRepeating(nameof(GameTimer), 1, 1);
    }

    void Update()
    {
        if (prepareTime == 0)
        {
            prepareTimer.SetActive(false);
            gameTimer.SetActive(true);
            
            _gameTimerText = GameObject.Find("GameTimerText").GetComponent<TMP_Text>();
            
            IsGameStart = true;
        }
        else
        {
            _prepareTimerText.text = "Приготовьтесь \n" + prepareTime;
        }

        if (timer == 0)
        {
            gameTimer.SetActive(false);
            endGame.SetActive(true);
            _endGameText = GameObject.Find("EndGameText").GetComponent<TMP_Text>();
            
            _endGameText.text = "Игра окончена \nВы набрали " + Points + " очков";

            IsGameStart = false;
            IsGameOver = true;
        }
        else if (prepareTime == 0 && timer != 0)
        {
            _gameTimerText.text = "Осталось: " + timer;
        }
        
        _pointsText.text = "Очков: " + Points;
    }

    private void PrepareTimer()
    {
        if (prepareTime > 0)
            prepareTime--;
    }

    private void GameTimer()
    {
        if (timer > 0 && prepareTime == 0)
            timer--;
    }
}
