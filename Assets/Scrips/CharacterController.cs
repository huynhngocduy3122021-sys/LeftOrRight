using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;

    [Header("Boundary Limit")]
    public float minX = -2.5f;
    public float maxX = 2.5f;

    [Header("Mobile Settings")]
    public float accelerometerSensitivity = 1.5f;
    public float deadZone = 0.05f;

    private float moveX;
    public Vector3 resetPosition;

    void Start()
    {
        resetPosition = transform.position;
    }

    void Update()
    {

        if (GameCOntroller.Instance != null && GameCOntroller.Instance.isProcessing)
        {
            return; // Thoát ngay khỏi Update, bỏ qua toàn bộ phần nhận Input bên dưới
        }
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        // Input cũ trên PC & Web
        moveX = Input.GetAxis("Horizontal");
#else
        // Mobile dùng gia tốc
        moveX = Input.acceleration.x * accelerometerSensitivity;
#endif

        // Dead zone chống trôi
        if (Mathf.Abs(moveX) < deadZone)
            moveX = 0f;

        MoveCharacter();
    }

    void MoveCharacter()
    {
        Vector3 pos = transform.position;
        pos.x += moveX * moveSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }

    public void ResetPosition()
    {
        transform.position = resetPosition;
    }
}