using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int mapX, mapY;

    public GameObject _gridObject;

    private MapController _map;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        _map = _gridObject.GetComponent<MapController>();
        mapX = _map.mapSizeX;
        mapY = _map.mapSizeY;
        

        camera = gameObject.GetComponent<Camera>();
        gameObject.transform.position = new Vector3(((float)mapX/2),((float)mapY/2),-10);
        
        if (mapX <= mapY) camera.orthographicSize = (float)mapY / 2;
        else camera.orthographicSize = (float)(mapX) / 2.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
