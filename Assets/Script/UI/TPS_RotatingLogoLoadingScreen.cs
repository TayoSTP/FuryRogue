using System.Numerics;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using Vector3 = UnityEngine.Vector3;

public class TPS_RotatingLogoLoadingScreen : MonoBehaviour
{
    
    [SerializeField] private GameObject loadingScreenLogo;
    [SerializeField] private float rotationSpeed = 350f;

    private void Update()
    {
        Vector3 rotation = Vector3.zero;
        rotation += Vector3.forward;
        
        loadingScreenLogo.transform.Rotate(rotation *  rotationSpeed * Time.deltaTime);
    }
    
    
    
}
