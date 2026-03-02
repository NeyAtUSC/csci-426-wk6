using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseGun : ScriptableObject
{
    private static System.Random _rng = new System.Random();
    
    private Queue<Bullet> _magazine = new Queue<Bullet>();

    public Bullet defaultBullet;
    public uint capacity = 8;       

    public void Reload(uint bullets = 1)
    {
        var slots = new List<Bullet>();
        for (var i = 0; i < (int)bullets; i++) slots.Add(ScriptableObject.CreateInstance<Bullet>());
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
}
