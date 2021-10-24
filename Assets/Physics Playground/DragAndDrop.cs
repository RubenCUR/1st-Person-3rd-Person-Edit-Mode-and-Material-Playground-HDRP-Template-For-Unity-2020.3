using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    private bool _mouseState;
    public GameObject Target;
    public Vector3 screenSpace;
    public Vector3 offset;


    
    public Vector3 currentPosition;
    public Vector3 previousPosition;

    public int Speed = 3000;

    public float timeCounter;

    //[SerializeField]
    //private Transform _player;

    public Vector3 direction = Vector3.forward;

    // Use this for initialization
    void Start()
    {
        Target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Debug.Log(_mouseState);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Target == GetClickedObject(out hitInfo))
            {
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(Target.transform.position);
                offset = Target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));



                


                if(Target.GetComponent<Rigidbody>() != null)
                    Target.GetComponent<Rigidbody>().AddForce(offset * Speed, ForceMode.Impulse);  
            }

            direction = Target.transform.position - transform.position;
            direction.Normalize();

            Debug.Log("Magnitude: " + direction.magnitude);

            Debug.DrawRay(transform.position, direction, Color.green);

            //transform.Translate(direction * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
        }
        if (_mouseState)
        {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            Target.transform.position = curPosition;





            //currentPosition = Target.transform.position;

            //timeCounter += Time.deltaTime;

            //if(timeCounter > .5)
            //{
            //    previousPosition = currentPosition;

            //    timeCounter = 0;
            //}

            //direction = previousPosition - currentPosition;
        }
    }


    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }
}
