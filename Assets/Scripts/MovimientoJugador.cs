using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // --- VARIABLES DE MECÁNICAS ---
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;

    // --- REFERENCIAS ---
    private Rigidbody2D rb;
    private float movimientoHorizontal;

    // --- VARIABLES DE FÍSICAS (Detector de Suelo) ---
    public Transform controladorSuelo; // El punto en los pies del personaje
    public float radioSuelo = 0.2f;    // Tamaño del detector
    public LayerMask queEsSuelo;       // Etiqueta para diferenciar el piso del aire
    private bool enSuelo;              // ¿Está tocando el piso?

    void Start()
    {
        // Conectamos el código con el componente físico del jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. INPUT: Leer las teclas A/D o Flechas Izquierda/Derecha
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");

        // 2. FÍSICAS DE SALTO: Crear un mini círculo invisible en los pies para detectar el piso
        enSuelo = Physics2D.OverlapCircle(controladorSuelo.position, radioSuelo, queEsSuelo);

        // Si presionas Espacio Y además estás tocando el suelo...
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            // Aplicamos fuerza hacia arriba manteniendo la inercia horizontal
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    void FixedUpdate()
    {
        // 3. FÍSICAS DE MOVIMIENTO: Se hace en FixedUpdate para que el movimiento sea fluido y no tiemble
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.linearVelocity.y);
    }
}