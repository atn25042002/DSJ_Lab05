using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour{
    // Configuración de la cámara
    public float followDistance = 10f; // Distancia fija desde arriba
    public Vector3 offset = new Vector3(0, 30, 0); // Offset de la cámara

    // Lista de objetos a seguir
    public List<Transform> targets;
    private Transform currentTarget;

    // Intervalo para cambiar el objetivo aleatoriamente
    public float changeTargetInterval = 5f;
    private float timer;

    private void Start()
    {
        if (targets.Count > 0)
        {
            // Establece el primer objetivo aleatorio
            SelectRandomTarget();
            timer = changeTargetInterval;
        }
        else
        {
            Debug.LogWarning("La lista de targets está vacía.");
        }
    }

    private void Update()
    {
        // Verifica si hay un objetivo actual y lo sigue
        if (currentTarget != null)
        {
            FollowTarget();
        }

        // Temporizador para cambiar el objetivo aleatoriamente
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SelectRandomTarget();
            timer = changeTargetInterval;
        }
    }

    private void FollowTarget()
    {
        // Calcula la posición deseada de la cámara
        Vector3 desiredPosition = currentTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 12f);

        // Ajusta la rotación Y de la cámara para que coincida con la del objetivo
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, currentTarget.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
    }

    private void SelectRandomTarget()
    {
        int randomIndex = Random.Range(0, targets.Count);
        currentTarget = targets[randomIndex];
    }
}