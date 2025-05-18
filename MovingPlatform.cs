using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 3f;   // Насколько далеко вверх-вниз
    public float speed = 2f;          // Скорость движения
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.PingPong(Time.time * speed, moveDistance);
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}
