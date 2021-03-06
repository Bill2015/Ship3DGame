using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public static int enemyLifeNumber = 0;
    public static int enemyPlayerDestoryNumer = 0;
    public static int turretLifeNumber = 0;

    public static int destoryFlyBombNumber = 0;
    public static int destoryWarBalloonNumber = 0;
    public static int destoryTurretNumber = 0;

    public static int enemyPlayerGetScore = 0;

    public static bool canUpdate = true;

    [Header("Enemy Monitor (Don't set)")]
    [SerializeField]  private int enemyLife;
    [SerializeField]  private int turretLife;
    [SerializeField]  private int playerDestory;
    [SerializeField]  private int warBalloonDestory;
    [SerializeField]  private int flyBombDestory;
    [SerializeField]  private int turretDestory;
    [SerializeField]  private int score;

    [Header("Score Parameter")]
    [SerializeField]  private int warBalloonScore = 400;
    [SerializeField]  private int flyBombScore = 50;
    [SerializeField]  private int turretScore = 100;

    // Start is called before the first frame update
    void Start()
    {
        canUpdate = true;
        enemyPlayerGetScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(canUpdate)
        {
            enemyPlayerGetScore = destoryWarBalloonNumber * warBalloonScore +
                                destoryTurretNumber * turretScore + 
                                destoryFlyBombNumber * flyBombScore;

            enemyLife = enemyLifeNumber;
            turretLife = turretLifeNumber;
            playerDestory = enemyPlayerDestoryNumer;

            warBalloonDestory = destoryWarBalloonNumber;
            flyBombDestory = destoryFlyBombNumber;
            turretDestory = destoryTurretNumber;
            score = enemyPlayerGetScore;
        }
    }
}
