using UnityEngine;
using Vuforia;

public class TargetHandler : MonoBehaviour
{
    private ObserverBehaviour observer;
    private GameManager gameManager;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();
        gameManager = FindObjectOfType<GameManager>();

        if (observer)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            gameManager.ValidarRespuesta(behaviour.TargetName);
        }
    }
}
