using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // 要跟隨的角色
    public float smoothSpeed = 0.125f;  // 攝影機平滑度
    public Vector3 offset;          // 攝影機與角色的偏移量

    //邊界設置
    public BoxCollider2D bounds;    // 攝影機邊界
    private float halfHeight;
    private float halfWidth;


    void Start()
    {
        Camera cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null || bounds == null) return;

        // 目標位置
        Vector3 desiredPosition = target.position + offset;

        // 只沿 X 軸移動（平行卷軸）
        desiredPosition.y = transform.position.y;
        desiredPosition.z = transform.position.z;

        // 平滑移動
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 取得邊界最小/最大值
        Bounds b = bounds.bounds;
        float clampedX = Mathf.Clamp(smoothedPosition.x, b.min.x + halfWidth, b.max.x - halfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, b.min.y + halfHeight, b.max.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}
