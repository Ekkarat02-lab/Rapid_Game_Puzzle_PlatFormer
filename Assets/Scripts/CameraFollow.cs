using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string player1Tag = "Player1";
    public string player2Tag = "Player2";
    public float minZoom = 3f;
    public float maxZoom = 10f;
    public float zoomLimiter = 10f;
    public float smoothTime = 0.5f;

    private Transform player1;
    private Transform player2;
    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();


        player1 = GameObject.FindGameObjectWithTag(player1Tag).transform;
        player2 = GameObject.FindGameObjectWithTag(player2Tag).transform;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {

            player1 = GameObject.FindGameObjectWithTag(player1Tag)?.transform;
            player2 = GameObject.FindGameObjectWithTag(player2Tag)?.transform;
            return;
        }

        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint;

        newPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void ZoomCamera()
    {
        float distance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(minZoom, maxZoom, distance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2;
    }

    float GetGreatestDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}