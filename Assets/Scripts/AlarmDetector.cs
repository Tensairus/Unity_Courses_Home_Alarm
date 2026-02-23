using System;
using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    public event Action IntruderEntered;
    public event Action IntruderLeft;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<UnitMover>(out _))
        {
            IntruderEntered?.Invoke();
        }        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<UnitMover>(out _))
        {
            IntruderLeft?.Invoke();
        }        
    }
}
