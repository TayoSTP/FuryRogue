using System;
using UnityEngine;
using UnityEngine.VFX;

// ReSharper disable once InconsistentNaming
public class TPS_GroundDetector : MonoBehaviour
{
    [SerializeField] private float distance = 0.5f;
    [SerializeField] public VisualEffectAsset explosionEffect;
    
    public bool touched;


    private void Start()
    {
            explosionEffect = GetComponent<VisualEffectAsset>();
    }
    
    private void Update()
    {
        touched = Physics.Raycast(transform.position, Vector3.down, distance);

        if (touched)
        {
            //play Explosion VFX
            
        }
    }

    private void OnDrawGizmos()
    {
        if (touched)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawRay(transform.position, Vector3.down * distance);
        
    }
}
