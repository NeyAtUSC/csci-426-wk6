using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunLoadUIController : MonoBehaviour
{
    [Header("UI References")]
    public Button loadGunButton;
    public TMP_Text loadGunText;
    public RectTransform bulletUI;
    public RectTransform gunBarrelUI;

    [Header("Gun References")]
    public GunController gunController;

    [Header("Animation Settings")]
    public float bulletMoveDuration = 0.5f;
    public float barrelSpinDuration = 2f;
    public float barrelSpinSpeed = 900f;
    public bool hideBulletAfterLoad = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip barrelSpinSound;
    public AudioClip loadClickSound;

    private bool _isAnimating;
    private bool _hasCachedInitialState;
    private Vector2 _initialBulletAnchoredPosition;
    private Quaternion _initialBarrelRotation;

    private void Awake()
    {
        if (loadGunButton != null)
            loadGunButton.onClick.AddListener(OnLoadGunPressed);

        CacheInitialState();
        EnsureLoadGunTextVisible();
    }

    private void OnDestroy()
    {
        if (loadGunButton != null)
            loadGunButton.onClick.RemoveListener(OnLoadGunPressed);
    }

    public void OnLoadGunPressed()
    {
        if (_isAnimating)
            return;

        StartCoroutine(LoadGunSequence());
    }

    private IEnumerator LoadGunSequence()
    {
        _isAnimating = true;
        CacheInitialState();
        ResetLoadVisuals();
        EnsureLoadGunTextVisible();

        if (loadGunButton != null)
            loadGunButton.interactable = false;

        if (gunController != null)
            gunController.OnLoadGun();

        if (bulletUI != null)
            bulletUI.gameObject.SetActive(true);
        if (gunBarrelUI != null)
            gunBarrelUI.gameObject.SetActive(true);

        // Move bullet left by 65 on X
        if (bulletUI != null)
        {
            var startPos = bulletUI.anchoredPosition;
            var targetPos = new Vector2(startPos.x - 65f, startPos.y);
            var effectiveMoveDuration = Mathf.Max(0.01f, bulletMoveDuration * 0.5f);
            var elapsed = 0f;

            while (elapsed < effectiveMoveDuration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / effectiveMoveDuration);
                bulletUI.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
                yield return null;
            }

            bulletUI.anchoredPosition = targetPos; // Ensure it ends exactly at target
        }

        if (gunBarrelUI != null)
        {
            // Play barrel spin sound
            if (audioSource != null && barrelSpinSound != null)
            {
                audioSource.PlayOneShot(barrelSpinSound);
            }
            
            var elapsed = 0f;
            
            while (elapsed < barrelSpinDuration)
            {
                elapsed += Time.deltaTime;
                var rotationDelta = -barrelSpinSpeed * Time.deltaTime;
                var newRotation = gunBarrelUI.localRotation;
                newRotation *= Quaternion.Euler(0f, 0f, rotationDelta);
                gunBarrelUI.localRotation = newRotation;
                yield return null;
            }

            // Reset barrel to center before hiding
            gunBarrelUI.localRotation = Quaternion.identity;
            gunBarrelUI.gameObject.SetActive(false);
        }

        if (hideBulletAfterLoad && bulletUI != null)
            bulletUI.gameObject.SetActive(false);

        // Play load click sound after loading finishes
        if (audioSource != null && loadClickSound != null)
        {
            audioSource.PlayOneShot(loadClickSound);
        }

        if (loadGunButton != null)
            loadGunButton.interactable = true;

        _isAnimating = false;
    }

    public void ResetLoadVisuals()
    {
        CacheInitialState();

        if (bulletUI != null)
        {
            bulletUI.gameObject.SetActive(true);
            bulletUI.anchoredPosition = _initialBulletAnchoredPosition;
        }

        if (gunBarrelUI != null)
        {
            gunBarrelUI.gameObject.SetActive(true);
            gunBarrelUI.localRotation = _initialBarrelRotation;
        }
    }

    private void CacheInitialState()
    {
        if (_hasCachedInitialState)
            return;

        if (bulletUI != null)
            _initialBulletAnchoredPosition = bulletUI.anchoredPosition;

        if (gunBarrelUI != null)
            _initialBarrelRotation = gunBarrelUI.localRotation;

        _hasCachedInitialState = true;
    }

    private void EnsureLoadGunTextVisible()
    {
        if (loadGunText == null && loadGunButton != null)
            loadGunText = loadGunButton.GetComponentInChildren<TMP_Text>(true);

        if (loadGunText == null)
            return;

        if (string.IsNullOrWhiteSpace(loadGunText.text))
            loadGunText.text = "Load Gun";
        else
            loadGunText.text = "Load Gun";

        if (loadGunText.font == null && TMP_Settings.defaultFontAsset != null)
            loadGunText.font = TMP_Settings.defaultFontAsset;

        if (!loadGunText.gameObject.activeSelf)
            loadGunText.gameObject.SetActive(true);

        loadGunText.enabled = true;
        loadGunText.color = Color.white;
    }
}