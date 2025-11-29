using UnityEngine;
using TMPro; // อย่าลืมบรรทัดนี้ ไม่งั้น error

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // ตั้งเวลาตรงนี้ (60 วินาที)
    public bool timerIsRunning = false;
    
    [SerializeField] private TMP_Text timerText; // ช่องสำหรับลาก Text มาใส่

    void Start()
    {
        // เริ่มเกมปุ๊บ สั่งให้เวลาเดินทันที
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // ลดเวลาลงตามเวลาจริง
                timeRemaining -= Time.deltaTime;
                
                // สั่งอัปเดตตัวเลขบนหน้าจอ
                DisplayTime(timeRemaining);
            }
            else
            {
                // เวลาหมด!
                timeRemaining = 0;
                timerIsRunning = false;
                
                Debug.Log("หมดเวลาแล้ว! (Time's up!)"); 
                // เดี๋ยวค่อยมาใส่โค้ดตัดสินผู้ชนะตรงนี้ทีหลัง
            }
        }
    }

    // ฟังก์ชันแปลงตัวเลขวินาที ให้เป็นรูปแบบ 00:00 (นาที:วินาที)
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // บวก 1 เพื่อให้เลขดูเนียนขึ้น (ไม่ค้างที่ 0 นานเกิน)
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // แสดงผลออกมาเป็นข้อความ
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}