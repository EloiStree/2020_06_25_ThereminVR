using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QAD_ActionOnCollision : MonoBehaviour
{

    public UnityEvent m_action;

    private void OnCollisionEnter(Collision collision)
    {
        m_action.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_action.Invoke();

    }
}
