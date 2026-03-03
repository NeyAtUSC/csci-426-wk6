using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffectController : MonoBehaviour
{
    private ParticleSystem ps;
    
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    
    void Start()
    {
        // Auto-destroy this GameObject after particle system finishes
        float duration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(gameObject, duration);
    }
}
