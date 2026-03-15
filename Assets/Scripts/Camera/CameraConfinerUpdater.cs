using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraConfinerUpdater : MonoBehaviour
{
    CinemachineConfiner2D confiner;

    void Awake()
    {
        confiner = GetComponentInChildren<CinemachineConfiner2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject bounds = GameObject.FindWithTag("CameraConfiner");

        if (bounds != null)
        {
            confiner.BoundingShape2D = bounds.GetComponent<Collider2D>();
            confiner.InvalidateBoundingShapeCache();
        }
    }
}