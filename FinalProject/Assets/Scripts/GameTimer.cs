using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // จำเป็นสำหรับการโหลดฉากใหม่

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public bool timerIsRunning = false;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;    // ตัวเลขจับเวลา
    [SerializeField] private GameObject winPanel;   // หน้าต่างจบเกม (Panel)
    [SerializeField] private TMP_Text winText;      // ข้อความบอกใครชนะ

    void Start()
    {
        timerIsRunning = true;
        
        // ซ่อนหน้าต่างจบเกมตอนเริ่ม
        if(winPanel != null) winPanel.SetActive(false);
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
                
                // หมดเวลา! คำนวณหาผู้ชนะทันที
                CalculateWinner();
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

    // --- ฟังก์ชันตัดสินผู้ชนะ ---
    void CalculateWinner()
    {
        // 1. หาพื้นที่ทั้งหมดในฉาก (ต้องติด Tag "enemy" ไว้ที่พื้นที่นะ!)
        GameObject[] allTerritories = GameObject.FindGameObjectsWithTag("enemy");
        
        int p1Score = 0;
        int p2Score = 0;

        // 2. วนลูปเช็กทีละอันว่าเป็นของใคร
        foreach(GameObject t in allTerritories)
        {
            // ดึงสคริปต์ EnemyManager ออกมาดูชื่อเจ้าของ
            EnemyManager em = t.GetComponent<EnemyManager>();
            
            if(em != null)
            {
                if(em.currentOwner == "Player1") p1Score++;
                else if(em.currentOwner == "Player2") p2Score++;
            }
        }

        // 3. แสดงหน้าต่างจบเกม
        if(winPanel != null) 
        {
            winPanel.SetActive(true);
            
            // 4. ตัดสินผลและขึ้นข้อความ
            if(p1Score > p2Score) 
            {
                winText.text = "Player 1 Wins!";
                winText.color = Color.green; // เปลี่ยนสีตัวหนังสือตามคนชนะก็ได้
            }
            else if(p2Score > p1Score) 
            {
                winText.text = "Player 2 Wins!";
                winText.color = Color.red;
            }
            else 
            {
                winText.text = "Draw!";
                winText.color = Color.white;
            }
        }

        // 5. หยุดเวลาในเกม (ทหารจะหยุดเดิน)
        Time.timeScale = 0; 
    }

    // --- ฟังก์ชันเริ่มเกมใหม่ (ผูกกับปุ่ม) ---
    public void RestartGame()
    {
        Time.timeScale = 1; // อย่าลืมคืนค่าเวลา! ไม่งั้นเริ่มเกมใหม่แล้วทุกอย่างจะนิ่ง
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดฉากปัจจุบันซ้ำ
    }
}