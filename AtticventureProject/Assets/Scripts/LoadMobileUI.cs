using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class LoadMobileUI : MonoBehaviour
{
    // [SerializeField] private List<GameObject> optionsList = new List<GameObject>();
    [SerializeField] private GameObject onScreenControls;

    void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        onScreenControls.SetActive(true);
#else
        onScreenControls.SetActive(false);
#endif
    }
}
