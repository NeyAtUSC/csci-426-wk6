using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("Hearts")]
    public Image[] heartSlots;
    public Sprite heartFull;
    public Sprite heartEmpty;

    private void Awake()
    {
        // Initialize all hearts as full
        for (int i = 0; i < heartSlots.Length; i++)
            heartSlots[i].sprite = heartFull;
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < heartSlots.Length; i++)
        {
            if (i < maxHealth)
            {
                heartSlots[i].gameObject.SetActive(true);
                heartSlots[i].sprite = i < currentHealth ? heartFull : heartEmpty;
            }
            else
            {
                heartSlots[i].gameObject.SetActive(false);
            }
        }
        Debug.Log($"[HealthHUD] Updated — {currentHealth}/{maxHealth} hearts");
    }
}