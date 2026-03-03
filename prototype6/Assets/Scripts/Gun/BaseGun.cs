using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : ScriptableObject
{
    private static System.Random _rng = new System.Random();
    private Queue<Bullet> _magazine = new Queue<Bullet>();

    public Bullet defaultBullet;
    public uint capacity = 8;
    public float reloadTime = 2f;
    public bool IsReloading { get; private set; }

    private void OnEnable()
    {
        _magazine.Clear();
        Reload(capacity); // full mag on load
    }

    public void Reload(uint bullets = 1)
    {
        var slots = new List<Bullet>();
        for (var i = 0; i < (int)bullets; i++) slots.Add(defaultBullet); // fixed: use defaultBullet
        for (var i = (int)bullets; i < (int)capacity; i++) slots.Add(null);
        for (var i = slots.Count - 1; i > 0; i--)
        {
            var j = _rng.Next(i + 1);
            (slots[i], slots[j]) = (slots[j], slots[i]);
        }
        foreach (var slot in slots)
            _magazine.Enqueue(slot);
    }

    public Bullet Fire()
    {
        if (_magazine.Count == 0) return null;
        return _magazine.Dequeue();
    }

    public bool IsEmpty() => _magazine.Count == 0;
    public void SetReloading(bool value) => IsReloading = value;
}