using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField]
    public float velocidadHorizontal = 6f;

    [SerializeField]
    public float gravedad = -9.81f;

    [SerializeField]
    public float fuerzaSalto = 3f;

    [SerializeField]
    public float distanciaPiso = 0.4f;

    [SerializeField]
    public float tiempoGiro = 0.1f;

    [SerializeField]
    public LayerMask capaPiso;

    public bool activar { get; set; }

    private Vector3 velocidad;
    private bool estaEnPiso;
    private float velocidadGiro;
    private float horizontal;
    private float vertical;
    private CharacterController cuerpo;
    private Animator animaciones;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private void Start()
    {
        activar = true;
        animaciones = GetComponent<Animator>();
        cuerpo = GetComponent<CharacterController>();

        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        estaEnPiso = Physics.CheckSphere(transform.position, distanciaPiso, capaPiso);

        if (activar)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;
       

        cuerpo.Move(velocidad * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && estaEnPiso)
            velocidad.y = fuerzaSalto;

        
        if (direccion.magnitude >= 0.1f)
        {
            float anguloDeseado = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloDeseado, ref velocidadGiro, tiempoGiro);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloDeseado, 0f) * Vector3.forward;
            cuerpo.Move(direccionMovimiento.normalized * velocidadHorizontal * Time.deltaTime);
        }

        if (!estaEnPiso)
            velocidad.y += gravedad * Time.deltaTime;
        EstadosAnimaciones();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Muerte"))
        {
            velocidad.y = 0;
            transform.position = posicionInicial;
            transform.rotation = rotacionInicial;
        }
    }

    public void AsiginarPosicion(Transform posicion)
    {
        posicionInicial = posicion.position;
        rotacionInicial = posicion.rotation;
    }
    private void EstadosAnimaciones()
    {
        float direccion = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
        direccion = Mathf.Clamp(direccion, 0, 1);
        animaciones.SetFloat("Direccion", direccion);
        animaciones.SetBool("EnElPiso", estaEnPiso);
        if (!estaEnPiso)
            animaciones.SetInteger("Salto", (int)velocidad.y);
    }
}
