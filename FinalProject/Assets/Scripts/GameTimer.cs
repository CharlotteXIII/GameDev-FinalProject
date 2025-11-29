using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // เผื่อรีเซ็ตเกม

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // 60 วินาที
    public bool timerIsRunning = false;
    
    [SerializeField] private TMP_Text timerText; // ลาก Text UI มาใส่เพื่อโชว์เวลา
    [SerializeField] private GameObject winPanel; // หน้าต่างจบเกม
    [SerializeField] private TMP_Text winText;    // ข้อความบอกใครชนะ

    void Start()
    {
        timerIsRunning = true;
        winPanel.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                CalculateWinner(); // หมดเวลา! คำนวณผล
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CalculateWinner()
    {
        // หาพื้นที่ทั้งหมดในฉาก (ที่มี Tag enemy)
        GameObject[] allTerritories = GameObject.FindGameObjectsWithTag("enemy");
        
        int p1Score = 0;
        int p2Score = 0;

        foreach(GameObject t in allTerritories)
        {
            EnemyManager em = t.GetComponent<EnemyManager>();
            if(em.currentOwner == "Player1") p1Score++;
            else if(em.currentOwner == "Player2") p2Score++;
        }

        // แสดงผล
        winPanel.SetActive(true);
        if(p1Score > p2Score) winText.text = "Player 1 Wins!";
        else if(p2Score > p1Score) winText.text = "Player 2 Wins!";
        else winText.text = "Draw!"; // เสมอ

        // หยุดเกม (หยุดการเกิดทหาร ฯลฯ)
        Time.timeScale = 0; 
    }
}