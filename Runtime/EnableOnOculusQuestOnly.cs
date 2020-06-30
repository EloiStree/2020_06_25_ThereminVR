using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnOculusQuestOnly : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_EDITOR
        gameObject.SetActive(false);
#elif !UNITY_ANDROID
        gameObject.SetActive(false);
#endif
    }
}
