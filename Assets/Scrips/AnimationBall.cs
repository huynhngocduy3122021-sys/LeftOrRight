using UnityEngine;
using System.Collections;

public class AnimationBall : MonoBehaviour
{
    [Header("Ball Component")]
    [SerializeField] private Transform ballTransform;

    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 originalLocalPosition;
    
    private Coroutine currentAnimation;

    void Start()
    {
        if (ballTransform == null) ballTransform = transform;
        
        originalScale = ballTransform.localScale;
        originalRotation = ballTransform.localRotation;
        originalLocalPosition = ballTransform.localPosition;
    }

    public void PlayCorrectAnimation()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        ResetBallState();
        currentAnimation = StartCoroutine(CorrectRoutine());
    }

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

    // --- HIỆU ỨNG THẮNG (Tổng 2.0s) ---
    private IEnumerator CorrectRoutine()
    {
        float duration = 1.5f; 
        float elapsed = 0f;
        Vector3 targetScale = originalScale * 1.5f;

        // Bước 1: Phóng to và xoay lộn vòng (1.5 giây)
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            ballTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            ballTransform.Rotate(0, 0, -1080 * Time.deltaTime / duration); 
            
            yield return null;
        }

        // Bước 2: Nảy thu nhỏ về kích thước ban đầu (0.5 giây)
        elapsed = 0f;
        duration = 0.5f; 
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            ballTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }
        
        ResetBallState();
    }

    // --- HIỆU ỨNG THUA: XẸP VÀ LẢO ĐẢO (Tổng 2.0s) ---
    private IEnumerator IncorrectRoutine()
    {
        // Thông số mục tiêu khi bị xẹp
        Vector3 targetSquashScale = new Vector3(originalScale.x * 1.3f, originalScale.y * 0.4f, originalScale.z);
        Vector3 targetDropPos = originalLocalPosition + new Vector3(0, -0.5f, 0);

        // Giai đoạn 1: Rớt xuống và bóp dẹt (0.5 giây)
        float elapsed = 0f;
        float duration1 = 0.5f;
        while (elapsed < duration1)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration1;
            
            ballTransform.localScale = Vector3.Lerp(originalScale, targetSquashScale, t);
            ballTransform.localPosition = Vector3.Lerp(originalLocalPosition, targetDropPos, t);
            
            yield return null;
        }

        // Đảm bảo thông số chuẩn xác khi kết thúc Phase 1
        ballTransform.localScale = targetSquashScale;
        ballTransform.localPosition = targetDropPos;

        // Giai đoạn 2: Lắc lư lảo đảo "chóng mặt" (1.0 giây)
        elapsed = 0f;
        float duration2 = 1.0f;
        while (elapsed < duration2)
        {
            elapsed += Time.deltaTime;
            
            // Dùng sóng Sin để tạo góc xoay trục Z qua lại (biên độ 30 độ)
            float angleZ = Mathf.Sin(elapsed * Mathf.PI * 6) * 30f; 
            ballTransform.localRotation = originalRotation * Quaternion.Euler(0, 0, angleZ);
            
            yield return null;
        }

        // Đưa góc xoay về thẳng đứng trước khi nhảy lên
        ballTransform.localRotation = originalRotation;

        // Giai đoạn 3: Bật dậy, khôi phục trạng thái cũ (0.5 giây)
        elapsed = 0f;
        float duration3 = 0.5f;
        while (elapsed < duration3)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration3;
            
            // Làm mềm chuyển động (Ease Out) bằng hàm Sin
            float easeT = Mathf.Sin(t * Mathf.PI * 0.5f);
            
            ballTransform.localScale = Vector3.Lerp(targetSquashScale, originalScale, easeT);
            ballTransform.localPosition = Vector3.Lerp(targetDropPos, originalLocalPosition, easeT);
            
            yield return null;
        }

        ResetBallState();
    }
}