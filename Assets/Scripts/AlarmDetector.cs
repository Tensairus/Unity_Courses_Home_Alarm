using System;
using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    public event Action intruderEntered;
    public event Action intruderLeft;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<UnitMover>(out UnitMover _))
        {
            intruderEntered?.Invoke();
        }        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<UnitMover>(out UnitMover _))
        {
            intruderLeft?.Invoke();
        }        
    }
}
