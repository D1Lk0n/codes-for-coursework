using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.15f, 3.88f, -10f); // Настроено как ты хотел
    public float zoom = 3f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            cam.orthographicSize = zoom;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Жёстко ставим камеру за игроком
        transform.position = target.position + offset;

        // Обновляем приближение
        if (cam != null && cam.orthographic)
        {
            cam.orthographicSize = zoom;
        }
    }
}
