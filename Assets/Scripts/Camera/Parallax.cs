using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform CameraTransform;
    private Transform[] Layers;
    private int LeftIndex;
    private int RightIndex;
    private Vector3 LastCameraPos;
    private Vector3 OffsetToCam;

    [Tooltip("Viewzone of the camera. Does not need to be changed if the default camera has not been changed.")]
    public float ViewZone = 10;

    [Tooltip("Background size for scrolling. Can usually be automatically retrieved if set to 0.")]
    public float BackgroundSize = 0;

    [Tooltip("Should the parallax effect occur on the y-axis?")]
    public bool MoveY = false;

    [Tooltip(
        "Enables background-scrolling. This script must be placed on parent object with 3 children, aligned next to each other.")]
    public bool Scroll = false;

    [Tooltip(
        "Parallax factor. Positive values results in slower parallax, negative values in faster. 1=fixed in world. 0=fixed with cam")]
    public float ParallaxFactor = 0;


    // Use this for initialization
    void Start()
    {
        CameraTransform = Camera.main.transform;
        LastCameraPos = CameraTransform.position;
        OffsetToCam = CameraTransform.position - transform.position;

        if (Scroll)
        {
            Layers = new Transform[transform.childCount];

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = transform.GetChild(i);
            }

            LeftIndex = 0;
            RightIndex = Layers.Length - 1;

            if (BackgroundSize == 0 && Layers.Length > 1)
                BackgroundSize = Mathf.Abs(Layers[1].position.x - Layers[0].position.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = CameraTransform.position.x - LastCameraPos.x;
        float deltaY = CameraTransform.position.y - LastCameraPos.y;
        LastCameraPos = CameraTransform.position;

        if (ParallaxFactor == 0)
        {
            transform.position = CameraTransform.position - OffsetToCam;
        }
        else
        {
            transform.position +=
                new Vector3(deltaX * (1 - ParallaxFactor), MoveY ? deltaY * (1 - ParallaxFactor) : deltaY);
        }

        if (Scroll && CameraTransform.position.x < (Layers[LeftIndex].transform.position.x + ViewZone))
        {
            ScrollLeft();
        }
        else if (Scroll && CameraTransform.position.x > (Layers[RightIndex].transform.position.x - ViewZone))
        {
            ScrollRight();
        }
    }

    void ScrollLeft()
    {
        Layers[RightIndex].position = new Vector3(Layers[LeftIndex].position.x - BackgroundSize,
            Layers[RightIndex].position.y, Layers[RightIndex].position.z);

        LeftIndex = RightIndex;
        RightIndex--;

        if (RightIndex < 0)
            RightIndex = Layers.Length - 1;
    }

    void ScrollRight()
    {
        Layers[LeftIndex].position = new Vector3(Layers[RightIndex].position.x + BackgroundSize,
            Layers[LeftIndex].position.y, Layers[LeftIndex].position.z);

        RightIndex = LeftIndex;
        LeftIndex++;

        if (LeftIndex == Layers.Length)
            LeftIndex = 0;
    }
}