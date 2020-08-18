using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float scale;
    public Vector2 offset;
    public Vector2 verticalLimit;
    public Transform target;

    void Start()
    {
        if (target == null) //IF TARGET IS NULL
            return; //EXIT METHOD

        Vector2 desiredPosition = new Vector2(target.position.x, target.position.y) + offset; //CALCULATE DESIRED POSITION 
        transform.position = desiredPosition; //SET CAMERA START POSITION
    }

    void LateUpdate()
    {
        if (target == null)//IF TARGET IS NULL
            return;//EXIT METHOD

        Vector2 desiredPosition = new Vector2(target.position.x,target.position.y) + offset;//CALCULATE DESIRED POSITION 
        Vector2 smoothedPosition = Vector2.Lerp(new Vector2(transform.position.x,transform.position.y),desiredPosition,scale*Time.deltaTime);//CALCULATE SMOOTHED POSITION 
        transform.position = new Vector3(smoothedPosition.x, Mathf.Clamp(smoothedPosition.y,verticalLimit.x,verticalLimit.y), transform.position.z); //SET CAMERA POSITION

    }
}
