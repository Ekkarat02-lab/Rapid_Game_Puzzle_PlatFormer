using UnityEngine;
using TMPro; // ใช้สำหรับ TextMeshPro

public class RackingScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // ข้อความที่จะใช้แสดงผลคะแนน
    public int playerDeaths;           // จำนวนครั้งที่ผู้เล่นตาย
    public float gameTime;             // เวลาในการเล่นเกม (ในหน่วยวินาที)

    void Start()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        string deathRank = GetDeathRank(playerDeaths);
        string timeRank = GetTimeRank(gameTime);
        
        // แสดงข้อความที่รวมการจัดอันดับทั้งสองอย่าง
        scoreText.text = "Death Rank: " + deathRank + "\nTime Rank: " + timeRank;
    }

    string GetDeathRank(int deaths)
    {
        if (deaths <= 2)
        {
            return "S";
        }
        else if (deaths <= 4)
        {
            return "A";
        }
        else if (deaths <= 7)
        {
            return "B";
        }
        else
        {
            return "C"; // หากมีการตายมากกว่า 7 ครั้ง
        }
    }

    string GetTimeRank(float timeInSeconds)
    {
        float timeInMinutes = timeInSeconds / 60f; // แปลงวินาทีเป็นนาที

        if (timeInMinutes <= 5f)
        {
            return "S";
        }
        else if (timeInMinutes <= 7f)
        {
            return "A";
        }
        else
        {
            return "B"; // หากเวลามากกว่า 7 นาที
        }
    }
}