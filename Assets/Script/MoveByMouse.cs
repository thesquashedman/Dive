using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByMouse : MonoBehaviour
{

    public Vector3 mousePosition;
    public Vector3 localPosition;
    public Vector3 worldPosition;
    public Vector3 parentPosition;
    public float spacing;
    // Start is called before the first frame update
    void Start()
    {
        // initialize space between character and weapon
        spacing = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        localPosition  = transform.localPosition;
        worldPosition = transform.position;
        // Debug.Log(mousePosition.x + " " + mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Debug.Log(transform.rotation);
        
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        parentPosition = transform.parent.position;
        float angle = Mathf.Atan(mousePosition.y-parentPosition.y/mousePosition.x-parentPosition.x);
        Vector2 curPosition = new Vector2(
            spacing*Mathf.Cos(angle),
            spacing*Mathf.Sin(angle)
        );
        transform.localPosition = curPosition;
        transform.right = direction;

    } 
}
