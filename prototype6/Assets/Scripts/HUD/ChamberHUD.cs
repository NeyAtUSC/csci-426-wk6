using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChamberHUD : MonoBehaviour
{
    [Header("Chamber Slots")]
    public Image[] chamberSlots; // 6 UI Images assigned in Inspector

    [Header("Player Identity")]
    public Color playerColor = Color.red;       // set per player instance
    public Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);    // dark gray - fired empty
    public Color unknownColor = new Color(0.45f, 0.38f, 0.3f, 1f); // dull bronze - unresolved
    public Color reloadCycleColor = new Color(1f, 0.85f, 0.2f, 1f); // gold flash during spin

    private int _currentChamber = 0;
    private ChamberState[] _states;

    private enum ChamberState { Unknown, Live, Empty }

    private void Awake()
    {
        _states = new ChamberState[chamberSlots.Length];
        ResetAll();
    }

    // Call this after every Fire() — pass whether the chamber was live or not
    public void OnChamberFired(bool wasLive)
    {
        Debug.Log($"[ChamberHUD] OnChamberFired called — wasLive: {wasLive}, chamber: {_currentChamber}, slots assigned: {chamberSlots.Length}");
        _states[_currentChamber] = wasLive ? ChamberState.Live : ChamberState.Empty;
        chamberSlots[_currentChamber].color = wasLive ? playerColor : emptyColor;
        _currentChamber = (_currentChamber + 1) % chamberSlots.Length;
    }

    // Call this when reload starts
    public void OnReloadStart(float reloadTime)
    {
        StartCoroutine(ReloadSpinAnimation(reloadTime));
    }

    private IEnumerator ReloadSpinAnimation(float reloadTime)
    {
        float stepInterval = reloadTime / (chamberSlots.Length * 4f);

        int totalSteps = chamberSlots.Length * 4;
        for (int i = 0; i < totalSteps; i++)
        {
            for (int j = 0; j < chamberSlots.Length; j++)
                chamberSlots[j].color = unknownColor;

            chamberSlots[i % chamberSlots.Length].color = reloadCycleColor;

            yield return new WaitForSeconds(stepInterval);
        }

        ResetAll();
    }

    private void ResetAll()
    {
        _currentChamber = 0;
        for (int i = 0; i < chamberSlots.Length; i++)
        {
            _states[i] = ChamberState.Unknown;
            chamberSlots[i].color = unknownColor;
        }
    }
}