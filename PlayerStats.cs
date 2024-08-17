using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int RocksRemaining;
    public int startingRocks = 5;

    public static int Lives;
    public int startLives = 100;

    public static int score;
    public int StartScore = 0;


    public static int Rounds;

    void Start ()
    {
        score = StartScore;
        Money = startMoney;
        RocksRemaining = startingRocks;
        Lives = startLives;

        Rounds = 0;
    }
}