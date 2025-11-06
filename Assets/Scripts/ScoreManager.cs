using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text levelPassedText; // Assign in Inspector
    private int score = 0;
    private Vector3 originalScale;  

    [Header("Level Settings")]
    public int scoreToPass = 3; // Score needed to pass
    public string nextLevelName = "Level2"; // Next scene name

    [Header("Level Passed Animation Settings")]
    public float fadeDuration = 0.5f; // Fade-in duration
    public float popScale = 1.5f;     // Pop scale
    public float displayTime = 1.5f;  // Time text stays before scene change

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        originalScale = scoreText.transform.localScale;
        UpdateScoreText();

        if (levelPassedText != null)
        {
            levelPassedText.gameObject.SetActive(false);
            levelPassedText.alpha = 0;
            levelPassedText.transform.localScale = Vector3.one;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        StartCoroutine(PopAnimation());

        if (score >= scoreToPass)
            StartCoroutine(LevelPassedAnimation());
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"<color=#FFD700><b>★ {score}</b></color>";
    }

    private IEnumerator PopAnimation()
    {
        scoreText.transform.localScale = originalScale * 1.3f;
        yield return new WaitForSeconds(0.15f);
        scoreText.transform.localScale = originalScale;
    }

    // 🌟 Professional Level Passed Animation
    private IEnumerator LevelPassedAnimation()
    {
        if (levelPassedText == null) yield break;

        // Activate text
        levelPassedText.gameObject.SetActive(true);
        // TMP Rich Text with stars
     levelPassedText.text = "<gradient=#FFD700,#FFA500><b> LEVEL PASSED! </b></gradient>";


        // Fade-in + Pop
        float t = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * popScale;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            levelPassedText.alpha = alpha;
            levelPassedText.transform.localScale = Vector3.Lerp(startScale, targetScale, t / fadeDuration);
            yield return null;
        }

        // Bounce back to normal scale
        t = 0f;
        Vector3 normalScale = Vector3.one;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            levelPassedText.transform.localScale = Vector3.Lerp(targetScale, normalScale, t / 0.2f);
            yield return null;
        }

        // Optional: subtle rotation for fun
        float rotationTime = 0.5f;
        t = 0f;
        Quaternion startRot = Quaternion.Euler(0, 0, -5);
        Quaternion endRot = Quaternion.Euler(0, 0, 5);
        while (t < rotationTime)
        {
            t += Time.deltaTime;
            levelPassedText.transform.rotation = Quaternion.Lerp(startRot, endRot, Mathf.PingPong(t * 4, 1));
            yield return null;
        }

        // Hold for display
        yield return new WaitForSeconds(displayTime);

        // Fade out screen before loading next level
        if (FadeManager.instance != null)
            yield return StartCoroutine(FadeManager.instance.FadeOut(1f));

        // Load next level
        SceneManager.LoadScene(nextLevelName);
    }
}
