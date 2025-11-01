using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class QA
    {
        public string pregunta;
        public string respuestaMarkerId;
    }

    public QA[] banco = new QA[]
    {
        new QA{ pregunta = "¿Cuál es la figura 1?", respuestaMarkerId = "Figura1" },
        new QA{ pregunta = "¿Cuál es la figura 2?", respuestaMarkerId = "Figura2" }
    };

    private QA actual;

    // Ciclo del juego
    private int contadorCorrectas = 0;
    public int meta = 3;

    // UI
    [Header("Referencias UI")]
    public Text mensajeUI;
    public Text contadorUI;
    public Slider barraProgreso;


    // 🔊 Sonidos
    [Header("Audio")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    public AudioClip sonidoVictoria;
    public AudioSource fuenteAudio;

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
            ActualizarTexto($"✅ ¡Correcto! ({contadorCorrectas}/{meta})");

            // 🔊 Reproducir sonido correcto
            if (fuenteAudio && sonidoCorrecto)
                fuenteAudio.PlayOneShot(sonidoCorrecto);

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
            contadorCorrectas = 0;
            ActualizarTexto("❌ Incorrecto, contador reiniciado");

            // 🔊 Reproducir sonido incorrecto
            if (fuenteAudio && sonidoIncorrecto)
                fuenteAudio.PlayOneShot(sonidoIncorrecto);

            NuevaPregunta();
        }

        ActualizarUI();
    }

    void GanarJuego()
    {
        ActualizarTexto("🎉 ¡Ganaste el juego! 🎉");
        contadorCorrectas = 0;
        ActualizarUI();

        // 🔊 Reproducir sonido de victoria
        if (fuenteAudio && sonidoVictoria)
            fuenteAudio.PlayOneShot(sonidoVictoria);
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

        if (barraProgreso != null)
            barraProgreso.value = contadorCorrectas;
    }

}
