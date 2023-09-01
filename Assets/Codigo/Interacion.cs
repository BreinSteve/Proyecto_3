using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Interacion : MonoBehaviour {

    public string elemento;

    public UnityEvent entro;
    public UnityEvent salio;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == elemento)
        {
            entro.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == elemento)
        {
            salio.Invoke();
        }
    }
}
