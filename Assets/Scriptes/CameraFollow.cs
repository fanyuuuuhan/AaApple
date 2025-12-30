using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // 要跟隨的角色
    public float smoothTime = 0.25f;    // 達到目標的大約時間
    public Vector3 offset;             // 攝影機與角色的偏移量

    // 邊界設置
    public BoxCollider2D bounds;       // 攝影機邊界
    private float halfHeight;
    private float halfWidth;

    // 原始Y位與平滑緩衝
    private float originalY;
    private Vector3 currentVelocity = Vector3.zero;

    // 設定觸發跟隨的高度閾值
    public float followThresholdY = 3f;
    public float followThresholdY_low = -5f;


    void Start()
    {
        Camera cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
        target = GameManager.Instance.Player.transform;
        // 記錄初始高度
        originalY = transform.position.y;
    }

    void LateUpdate()
    {
        // 安全檢查：自動獲取玩家
        if (target == null)
        {
            if (GameManager.Instance != null && GameManager.Instance.Player != null)
                target = GameManager.Instance.Player.transform;
            else
                return;
        }

        if (bounds == null) return;

        // --- 核心邏輯修改處 ---

        // 1. 取得玩家包含偏移後的理想位置
        Vector3 playerPosWithOffset = target.position + offset;

        // 2. 決定目標 Y 軸：
        // 如果玩家的 Y 座標還沒超過閾值 (3)，目標就是 originalY。
        // 如果超過了，目標就是 (originalY + 超過的部分)。
        float targetY = originalY;
        if (target.position.y > followThresholdY)
        {
            // 計算玩家超過閾值多少，並將這個位移加到攝影機的原始位置上
            float difference = target.position.y - followThresholdY;
            targetY = originalY + difference;
        }
        else if (target.position.y < followThresholdY_low)
        {
            float difference = target.position.y - followThresholdY_low;
            targetY = originalY + difference;
        }

        // 3. 組合目標位置
        Vector3 desiredPosition = new Vector3(playerPosWithOffset.x, targetY, transform.position.z);

        // 4. 平滑移動
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothTime
        );

        // 5. 邊界限制 (Clamping)
        Bounds b = bounds.bounds;
        float clampedX = Mathf.Clamp(smoothedPosition.x, b.min.x + halfWidth, b.max.x - halfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, b.min.y + halfHeight, b.max.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }


}
