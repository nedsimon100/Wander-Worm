using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public PlayerController player;
    public int MaxDist = 0;
    public TextMeshProUGUI[] ScoreText;
    public GameObject BestScoreLine;
    public string BestDistKey = "BestDist";
    public GameObject killScreen;
    public GameObject NormUI;
    void Start()
    {
        Time.timeScale = 1f;
        player = FindFirstObjectByType<PlayerController>();
        PlayerPrefs.GetInt(BestDistKey, 0);
        PlayerPrefs.Save();
        player.BestDistKey = BestDistKey;
        if (PlayerPrefs.GetInt(BestDistKey, 0) > 0)
        {
            Instantiate(BestScoreLine, new Vector3(0, PlayerPrefs.GetInt(BestDistKey, 0), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxDist < Mathf.FloorToInt(player.transform.position.y))
        {
            MaxDist = Mathf.FloorToInt(player.transform.position.y);
            ScoreText[0].text = MaxDist.ToString() + "m";
        }
        if (player.gameOver)
        {
            killScreen.SetActive(true);
            NormUI.SetActive(false);
            Time.timeScale = 0;
            ScoreText[1].text = MaxDist.ToString() + "m";
            ScoreText[2].text = PlayerPrefs.GetInt(BestDistKey, 0).ToString() + "m";

        }

    }

    
}
