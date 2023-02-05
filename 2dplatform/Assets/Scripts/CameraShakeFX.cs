using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeFX : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 ShakeDir;
    [SerializeField] private float forceMultiplier;

    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector3(ShakeDir.x * facingDir, ShakeDir.y) * forceMultiplier;
        impulse.GenerateImpulse();
    }
    
}
