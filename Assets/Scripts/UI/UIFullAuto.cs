using UnityEngine;
using UnityEngine.UI;

public class UIFullAuto : MonoBehaviour
{
    [SerializeField] private Image radialImage;
    private float duration;
    private float timer;
    private bool isActive;

    public void StartCountdown(float powerUpDuration)
    {
        duration = powerUpDuration;
        timer = duration;
        isActive = true;
        radialImage.fillAmount = 1f; // start full
    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        radialImage.fillAmount = Mathf.Clamp01(timer / duration);

        if (timer <= 0f)
        {
            isActive = false;
            radialImage.fillAmount = 0f;
        }
    }
}
