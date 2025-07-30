using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f;       // how fast it rises
    public float lifetime = 1f;         // seconds before it disappears
    public float fadeDuration = 0.5f;   // how quickly it fades at the end

    private TextMeshProUGUI textMesh;
    private CanvasGroup canvasGroup;
    private float timer;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
        timer = lifetime;
    }

    void Update()
    {
        // Move upwards
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Countdown
        timer -= Time.deltaTime;

        // Fade out
        if (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
        }

        // Destroy when time’s up
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
