using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // อ้างอิงไปที่ตำแหน่งของผู้เล่น
    public Vector3 offset; // ค่าชดเชยระหว่างกล้องกับผู้เล่น
    public float smoothSpeed = 0.125f; // ความเร็วในการติดตามกล้อง
    public float defaultSize = 5f; // ขนาดปกติของการซูมกล้อง
    public float zoomOutSize = 10f; // ขนาดซูมออกเมื่อต้องการเห็นผู้เล่นหลายคน
    public string playerTag1 = "Player"; // แท็กของผู้เล่น 1
    public string playerTag2 = "Player"; // แท็กของผู้เล่น 2

    void Start()
    {
        FindPlayers();
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    void FindPlayers()
    {
        // ค้นหาผู้เล่นด้วยแท็ก Player1 และ Player2
        GameObject player1Obj = GameObject.FindGameObjectWithTag(playerTag1);
        GameObject player2Obj = GameObject.FindGameObjectWithTag(playerTag2);

        // หากพบผู้เล่น 2 คน ให้ซูมออก
        if (player1Obj != null && player2Obj != null)
        {
            player = null; // ไม่จำเป็นต้องติดตามผู้เล่นคนเดียว
            Camera.main.orthographicSize = zoomOutSize;
        }
        // หากพบผู้เล่นเพียงคนเดียว ให้ติดตามผู้เล่นคนนั้นและซูมปกติ
        else if (player1Obj != null)
        {
            player = player1Obj.transform;
            Camera.main.orthographicSize = defaultSize;
        }
        else if (player2Obj != null)
        {
            player = player2Obj.transform;
            Camera.main.orthographicSize = defaultSize;
        }
    }

    void UpdateCamera()
    {
        // ติดตามผู้เล่นหากมีเพียงผู้เล่นเดียว
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}