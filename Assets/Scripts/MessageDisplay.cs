using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] IntegerVariable state;

    void Update()
    {
        if (state.value == 0) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
