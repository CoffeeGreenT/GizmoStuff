using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform target; //player position
    [SerializeField]
    private Vector3 targetOffset;
    [SerializeField]
    private float moveSpeed; //rate of follow


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
    }
    
    void moveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, moveSpeed * Time.deltaTime);
        //slowly fix camera position from current point to target point (player position)
    }
}
