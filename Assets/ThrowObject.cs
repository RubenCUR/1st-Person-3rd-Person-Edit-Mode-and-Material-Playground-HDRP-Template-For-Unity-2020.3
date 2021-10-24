using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    public Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = _player.position - transform.position;
        direction.Normalize();

        Debug.Log("Magnitude: " + direction.magnitude);

        Debug.DrawRay(transform.position, direction, Color.green);

        transform.Translate(direction * Time.deltaTime);
    }
}
