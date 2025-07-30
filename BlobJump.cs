using UnityEngine;

public class BlobJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float minJumpHeight = 200f;
    public float maxJumpHeight = 300f;
    public float minJumpDuration = 0.6f;
    public float maxJumpDuration = 1f;
    public float styleChangeInterval = 4f;

    [Header("Wiggle Settings")]
    public float wiggleAngle = 5f;
    public float wiggleSpeed = 4f;

    private Vector3 startPos;
    private Quaternion startRotation;

    // Current and target styles
    private float currentHeight;
    private float currentDuration;
    private float targetHeight;
    private float targetDuration;

    private float styleTimer;
    private float jumpTimer;
    private bool isJumpingUp = true;

    void Start()
    {
        startPos = transform.localPosition;
        startRotation = transform.localRotation;

        // Initialize with a random style
        currentHeight = Random.Range(minJumpHeight, maxJumpHeight);
        currentDuration = Random.Range(minJumpDuration, maxJumpDuration);

        targetHeight = currentHeight;
        targetDuration = currentDuration;
        styleTimer = styleChangeInterval;
    }

    void Update()
    {
        // Update style over time
        styleTimer -= Time.deltaTime;
        if (styleTimer <= 0f)
        {
            // pick new target style
            targetHeight = Random.Range(minJumpHeight, maxJumpHeight);
            targetDuration = Random.Range(minJumpDuration, maxJumpDuration);
            styleTimer = styleChangeInterval + Random.Range(-0.5f, 0.5f);
        }

        // Smoothly transition toward the target style
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime);
        currentDuration = Mathf.Lerp(currentDuration, targetDuration, Time.deltaTime);

        // Jump logic
        jumpTimer += Time.deltaTime;
        float t = jumpTimer / currentDuration;

        float yOffset;
        if (isJumpingUp)
        {
            yOffset = Mathf.Lerp(0, currentHeight, t);
            if (t >= 1f)
            {
                isJumpingUp = false;
                jumpTimer = 0f;
            }
        }
        else
        {
            yOffset = Mathf.Lerp(currentHeight, 0, t);
            if (t >= 1f)
            {
                isJumpingUp = true;
                jumpTimer = 0f;
            }
        }

        transform.localPosition = startPos + new Vector3(0, yOffset, 0);

        // Wiggle cheer
        float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAngle;
        transform.localRotation = startRotation * Quaternion.Euler(0, 0, wiggle);
    }
}
