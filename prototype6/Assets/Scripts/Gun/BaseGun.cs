using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : ScriptableObject
{
    private static System.Random _rng = new System.Random();
    private Queue<Bullet> _magazine = new Queue<Bullet>();
    
    public Bullet defaultBullet;
    [Header("Stats")]
    public float attackSpeed = 1f;
    public uint capacity = 6;
    public float reloadTime = 2f;
    public bool IsReloading { get; private set; }

    private float _lastFireTime = -Mathf.Infinity;
    
    private void OnEnable()
    {
        Debug.Log($"[BaseGun] OnEnable — magazine had {_magazine.Count} leftover slots (should be 0 on first run)");
        _lastFireTime = -Mathf.Infinity;
        _magazine.Clear();
        Reload(1);
        Debug.Log($"[BaseGun] OnEnable complete — magazine now has {_magazine.Count} slots");
    }

    public void Reload(uint bullets = 1)
    {
        int beforeCount = _magazine.Count;
        _magazine.Clear(); // fix: was additive before

        var slots = new List<Bullet>();
        for (var i = 0; i < (int)bullets; i++) slots.Add(defaultBullet);
        for (var i = (int)bullets; i < (int)capacity; i++) slots.Add(null);

        // Fisher-Yates shuffle
        for (var i = slots.Count - 1; i > 0; i--)
        {
            var j = _rng.Next(i + 1);
            (slots[i], slots[j]) = (slots[j], slots[i]);
        }

        foreach (var slot in slots)
            _magazine.Enqueue(slot);

        // Log the full magazine layout
        int bulletCount = 0;
        var layout = new System.Text.StringBuilder();
        foreach (var slot in slots)
        {
            bool isLive = slot != null;
            if (isLive) bulletCount++;
            layout.Append(isLive ? "[LIVE]" : "[EMPTY]");
            layout.Append(" ");
        }

        Debug.Log($"[BaseGun] Reload — was {beforeCount} slots, cleared, loaded {bullets}/{capacity} live rounds");
        Debug.Log($"[BaseGun] Magazine layout: {layout}");
        Debug.Log($"[BaseGun] Total live rounds in mag: {bulletCount}");
    }

    public Bullet Fire()
    {
        if (_magazine.Count == 0)
        {
            Debug.LogWarning("[BaseGun] Fire called on empty magazine!");
            return null;
        }

        _lastFireTime = Time.time;

        Bullet bullet = _magazine.Dequeue();
        bool isLive = bullet != null;
        Debug.Log($"[BaseGun] Fire — chamber was {(isLive ? "LIVE 💥" : "EMPTY 🫥")} — {LiveCount()} live rounds remaining out of {_magazine.Count} slots");
        return bullet;
    }

    public bool IsCylinderSpent()
    {
        foreach (var slot in _magazine)
            if (slot != null) return false;

        Debug.Log("[BaseGun] IsCylinderSpent — no live rounds remaining, reload triggered");
        return true;
    }
    public bool IsOnCooldown() => Time.time - _lastFireTime < 1f / attackSpeed;

    public int LiveCount()
    {
        int count = 0;
        foreach (var slot in _magazine)
            if (slot != null) count++;
        return count;
    }

    public void SetReloading(bool value)
    {
        Debug.Log($"[BaseGun] SetReloading — {value}");
        IsReloading = value;
    }
}