using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class QA
    {
        public string pregunta;
        public string respuestaMarkerId; // nombre EXACTO del marcador
    }

    // Banco de preguntas y respuestas
    public QA[] banco = new QA[]
    {
        new QA{ pregunta = "¿Cuál es la figura 1?", respuestaMarkerId = "Figura1" },
        new QA{ pregunta = "¿Cuál es la figura 2?", respuestaMarkerId = "Figura2" }
    };

    private QA actual;

    // 🔹 Variables del ciclo del juego
    private int contadorCorrectas = 0;
    public int meta = 3; // Cuántas correctas seguidas para ganar

    // 🔹 Referencias UI
    [Header("Referencias UI")]
    public Text mensajeUI;   // Texto principal de mensajes
    public Text contadorUI;  // Texto del contador (1/3, 2/3, etc.)

    void Start()
    {
        NuevaPregunta();
        ActualizarUI();
    }

    void NuevaPregunta()
    {
        actual = banco[Random.Range(0, banco.Length)];
        Debug.Log($"Nueva pregunta: {actual.pregunta} (esperada: {actual.respuestaMarkerId})");
        ActualizarTexto($"Pregunta: {actual.pregunta}");
    }

    public void ValidarRespuesta(string markerIdDetectado)
    {
        string a = (markerIdDetectado ?? "").Trim().ToLowerInvariant();
        string b = (actual.respuestaMarkerId ?? "").Trim().ToLowerInvariant();

        Debug.Log($"Detectado: {a} | Esperado: {b}");

        if (a == b)
        {
            contadorCorrectas++;
            Debug.Log($"✅ Correcto ({contadorCorrectas}/{meta})");
            ActualizarTexto($"✅ ¡Correcto! ({contadorCorrectas}/{meta})");

            if (contadorCorrectas >= meta)
            {
                GanarJuego();
            }
            else
            {
                NuevaPregunta();
            }
        }
        else
        {
            Debug.Log("❌ Incorrecto → contador reiniciado");
            ActualizarTexto("❌ Incorrecto, contador reiniciado");
            contadorCorrectas = 0;
            NuevaPregunta();
        }

        ActualizarUI();
    }

    void GanarJuego()
    {
        Debug.Log("🎉 ¡Ganaste el juego!");
        ActualizarTexto("🎉 ¡Ganaste el juego! 🎉");
        contadorCorrectas = 0;
        ActualizarUI();
    }

    void ActualizarTexto(string texto)
    {
        if (mensajeUI != null)
            mensajeUI.text = texto;
    }

    void ActualizarUI()
    {
        if (contadorUI != null)
            contadorUI.text = $"{contadorCorrectas} / {meta}";
    }
}
