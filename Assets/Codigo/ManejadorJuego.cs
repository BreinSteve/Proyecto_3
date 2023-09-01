using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManejadorJuego : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform contenedorGemas;
    public int cantidadGemas;
    public UnityEvent recoleccionExitosa;

    private int gemasRecolecatas;
    private void Start()
    {
        cantidadGemas = contenedorGemas.childCount;
    }
    public void VerificarGemas()
    {
        gemasRecolecatas++;
        if(gemasRecolecatas == cantidadGemas)
            recoleccionExitosa.Invoke();
    }
}
