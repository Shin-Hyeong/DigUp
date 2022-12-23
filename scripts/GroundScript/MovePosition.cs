using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    Camera sceneCam;

    const float maxZoomIn = 6f;
    const float maxZoonOut = 12f;

    const float limitedMaxHorizontal = 19f;
    const float limitedMinHorizontal = 7.5f;
    const float limitedMaxVertical = 10f;
    const float limitedMinVertical = 4.5f;

    float maximumHorizontal = 9f;
    float maximumVertical = 5f;

    void Start()
    {
        sceneCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float scrollScale = Input.GetAxis("Mouse ScrollWheel");
        Vector2 keyMoving = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), -10);
        Vector2 mouseMoving = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), -10);

        //이동
        transform.Translate(keyMoving);
        //드래그
        if (Input.GetMouseButton(1))
        {
            transform.Translate(mouseMoving);
        }

        //이동 한계
        if (transform.position.x > maximumHorizontal)
            transform.position = new Vector3(maximumHorizontal, transform.position.y, -10);
        else if (transform.position.x < -maximumHorizontal)
            transform.position = new Vector3(-maximumHorizontal, transform.position.y, -10);

        if (transform.position.y > maximumVertical)
            transform.position = new Vector3(transform.position.x, maximumVertical, -10);
        else if (transform.position.y < -maximumVertical)
            transform.position = new Vector3(transform.position.x, -maximumVertical, -10);
        
        //확대
        if (sceneCam.orthographicSize >= maxZoomIn && sceneCam.orthographicSize <= maxZoonOut)
        {
            sceneCam.orthographicSize += scrollScale;
        }
        //확대 한계
        if (sceneCam.orthographicSize < maxZoomIn) sceneCam.orthographicSize = maxZoomIn;
        else if (sceneCam.orthographicSize > maxZoonOut) sceneCam.orthographicSize = maxZoonOut;

        if((maximumHorizontal >= limitedMinHorizontal && maximumHorizontal <= limitedMaxHorizontal) ||
            (maximumVertical >= limitedMinVertical && maximumVertical <= limitedMaxVertical))
            maximumHorizontal -= scrollScale * 2.2f;

        if (maximumHorizontal < limitedMinHorizontal) maximumHorizontal = (int)limitedMinHorizontal;
        else if (maximumHorizontal > limitedMaxHorizontal) maximumHorizontal = (int)limitedMaxHorizontal;
        if (maximumVertical < limitedMinVertical) maximumVertical = (int)limitedMinVertical;
        else if (maximumVertical > limitedMaxVertical) maximumVertical = (int)limitedMaxVertical;
    }


}
