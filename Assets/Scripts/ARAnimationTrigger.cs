using UnityEngine;
using Vuforia;

public class ARAnimationTrigger : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    private Animator animator;
    private GameManager gameManager; // ✅ referencia al GameManager

    void Start()
    {
        // Buscar componentes de Vuforia y animación
        observerBehaviour = GetComponent<ObserverBehaviour>();
        animator = GetComponentInChildren<Animator>();

        // Buscar automáticamente el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;

        // La animación empieza desactivada
        if (animator != null)
            animator.enabled = false;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            Debug.Log($"✅ Marcador detectado: {gameObject.name}");

            // Activa animación
            if (animator != null)
                animator.enabled = true;

            // Envía el nombre del marcador al GameManager para validar
            if (gameManager != null)
                gameManager.ValidarRespuesta(gameObject.name);
        }
        else
        {
            Debug.Log($"❌ Marcador perdido: {gameObject.name}");

            // Detiene la animación
            if (animator != null)
                animator.enabled = false;
        }
    }
}
