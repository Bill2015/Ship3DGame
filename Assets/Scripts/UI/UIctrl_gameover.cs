using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIctrl_gameover : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnReturn;
    public TMP_Text score;
    void Start()
    {
        score.text = "Score: " + EnemyInfo.enemyPlayerGetScore.ToString();
        btnReturn.onClick.AddListener(clickReturn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void clickReturn(){
        Debug.Log("[Log] return");
        SceneManager.LoadScene("UI_start");
    }
}
