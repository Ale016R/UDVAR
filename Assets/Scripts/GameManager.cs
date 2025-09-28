using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class QA
    {
        public string pregunta;
        // usa el nombre EXACTO del target en la base (normalmente minúsculas)
        public string respuestaMarkerId; 
    }

    // Ajusta estos nombres a los de tu dataset (parece que son "geometrico" y "geometrico2")
    public QA[] banco = new QA[]
    {
        new QA{ pregunta = "Which one is the Sphere?", respuestaMarkerId = "gemetrico" },
        new QA{ pregunta = "Which one is the Cube?",   respuestaMarkerId = "geometrico2" }
    };

    private QA actual;

    void Start()
    {
        NuevaPregunta();
    }

    void NuevaPregunta()
    {
        actual = banco[Random.Range(0, banco.Length)];
        Debug.Log($"Pregunta: {actual.pregunta} (esperada: {actual.respuestaMarkerId})");
    }

    public void ValidarRespuesta(string markerIdDetectado)
    {
        string a = (markerIdDetectado ?? "").Trim().ToLowerInvariant();
        string b = (actual.respuestaMarkerId ?? "").Trim().ToLowerInvariant();

        Debug.Log($"Detectado: {a} | Esperado: {b}");

        if (a == b)
        {
            Debug.Log("✅ Correcto!");
            // Si quieres que pase a la siguiente:
            // NuevaPregunta();
        }
        else
        {
            Debug.Log("❌ Incorrecto!");
        }
    }
}
