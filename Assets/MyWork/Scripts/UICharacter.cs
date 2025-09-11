using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
    [SerializeField] private Canvas UI;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Character character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthBarImage != null)
        {
            character.health.OnHealthChange += UpdateHealthBar;
        }
    }

    private void OnDestroy()
    {
        if (character != null)
        {
            character.health.OnHealthChange -= UpdateHealthBar;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UI.transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp01(character.health.GetHealthInFraction());
    }
}
