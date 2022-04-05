using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
