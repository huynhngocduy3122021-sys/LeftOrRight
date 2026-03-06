using UnityEngine;
using System.Collections;
public class AnimationBall : MonoBehaviour
{
    [Header("Ball Component")]
    [SerializeField] private Transform ballTransform;

    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 originalLocalPosition;
    
    // Biến để theo dõi animation đang chạy, giúp tránh xung đột
    private Coroutine currentAnimation;

    void Start()
    {
        if (ballTransform == null) ballTransform = transform;
        
        // Lưu lại các thông số ban đầu
        originalScale = ballTransform.localScale;
        originalRotation = ballTransform.localRotation;
        originalLocalPosition = ballTransform.localPosition;
    }

    /// <summary>
    /// Gọi hàm này khi người chơi trả lời ĐÚNG
    /// </summary>
    public void PlayCorrectAnimation()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        ResetBallState();
        currentAnimation = StartCoroutine(CorrectRoutine());
    }

    /// <summary>
    /// Gọi hàm này khi người chơi trả lời SAI
    /// </summary>
    public void PlayIncorrectAnimation()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        ResetBallState();
        currentAnimation = StartCoroutine(IncorrectRoutine());
    }

    private void ResetBallState()
    {
        ballTransform.localScale = originalScale;
        ballTransform.localRotation = originalRotation;
        ballTransform.localPosition = originalLocalPosition;
    }

    // --- CÁC HÀM XỬ LÝ HIỆU ỨNG THUẦN UNITY ---

    private IEnumerator CorrectRoutine()
    {
        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 targetScale = originalScale * 1.5f;

        // Bước 1: Phóng to và xoay lộn vòng
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            ballTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            ballTransform.Rotate(0, 0, -360 * Time.deltaTime / duration); // Xoay vòng
            
            yield return null;
        }

        // Bước 2: Nảy thu nhỏ về kích thước ban đầu
        elapsed = 0f;
        duration = 0.2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            ballTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }
        
        ResetBallState();
    }

    private IEnumerator IncorrectRoutine()
    {
        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 targetScale = originalScale * 0.8f; // Hơi xẹp xuống

        // Bước 1: Lắc qua lại (dùng sóng Sin) và xẹp xuống
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Tính toán độ lắc theo trục X
            float shakeX = Mathf.Sin(t * Mathf.PI * 6) * 0.2f; 
            ballTransform.localPosition = originalLocalPosition + new Vector3(shakeX, 0, 0);

            // Bóng xẹp xuống
            ballTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            yield return null;
        }

        // Bước 2: Phục hồi vị trí và kích thước
        elapsed = 0f;
        duration = 0.15f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            ballTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            ballTransform.localPosition = Vector3.Lerp(ballTransform.localPosition, originalLocalPosition, t);
            
            yield return null;
        }

        ResetBallState();
    }
}
