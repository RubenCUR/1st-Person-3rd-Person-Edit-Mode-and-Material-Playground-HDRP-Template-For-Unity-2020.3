using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectsInArea : MonoBehaviour
{
    public string grabTag, enemyTag;
    public float grabRadius, damageRadius, damage, airSpeed, airHeight, maxDistance, yFix;
    public LayerMask LayerMask;

    Transform tossUnit;
    List<Transform> possibleTossUnit = new List<Transform>();
    Vector3 launchPosition, landPosition;
    float incrementor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Toss();
        }

        if (!tossUnit) return;

        incrementor += airSpeed * Time.deltaTime;

        Vector3 currentPosition = Vector3.Lerp(launchPosition, landPosition, incrementor);

        currentPosition.y += airHeight * Mathf.Sin(Mathf.Clamp01(incrementor) * Mathf.PI);

        tossUnit.position = currentPosition;

        float travelDistance = Vector3.Distance(tossUnit.position, landPosition);

        if(travelDistance < 0.2f)
        {
            Land();
        }

    }

    void Toss()
    {
        tossUnit = null;

        incrementor = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            landPosition = hit.point + new Vector3(0, yFix, 0);

            if(hit.transform)
            {
                Collider[] tossObjectsInRange = Physics.OverlapSphere(transform.position, grabRadius);

                foreach (Collider col in tossObjectsInRange)
                {
                    if(col.tag == grabTag)
                    {
                        possibleTossUnit.Add(col.transform);
                    }
                }

                if(possibleTossUnit.Count > 0)
                {
                    var random = Random.Range(0, possibleTossUnit.Count);

                    tossUnit = possibleTossUnit[random];
                    launchPosition = tossUnit.position;
                }
            }


        }
    }

    void Land()
    {
        Collider[] enemiseInRange = Physics.OverlapSphere(transform.position, grabRadius);

        foreach (Collider col in enemiseInRange)
        {
            if(col.tag == enemyTag)
            {
                //Damage Enemy here
            }

        }

        tossUnit = null;
    }
}
