using UnityEngine;

/**
    \brief Klasa, która odpowiada za kierowanie kamerą.  
 */
public class cameraMove : MonoBehaviour
{
    public float sensitivityHor = 7.0f;
    public float sensitivityVert = 7.0f;
    public float minimumVert = -90.0f;
    public float maximumVert = 90.0f;
    private float _rotationX = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        
        transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue) * Time.deltaTime * 350);
        
    }
}
