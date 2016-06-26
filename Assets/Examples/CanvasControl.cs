using UnityEngine;
using System.Collections;
using System;

public class CanvasControl : MonoBehaviour {
    public Canvas myCanvas;

    public RectTransform imageTopLeft;
    public RectTransform imageTopRight;
    public RectTransform imageBottomLeft;
    public RectTransform imageBottomRight;
    public RectTransform imageLogo;

	// Use this for initialization
	void Start () {
        myCanvas = this.GetComponent<Canvas>();
        Transform transformObj = this.imageTopLeft.transform;
        if (null == transformObj)
        {
            Debug.Log("the imagetopleft has no transform");
        }

        this.imageBottomLeft.transform.Translate(new Vector3(-500, -250, 0));
        this.imageBottomRight.transform.Translate(new Vector3(500, -250, 0));
        this.imageTopLeft.transform.Translate(new Vector3(-500, 250, 0));
        this.imageTopRight.transform.Translate(new Vector3(500, 250, 0));

        Debug.Log("The transformation is there");
    }
	
	// Update is called once per frame
	void Update () {
        int updateImageCount = 0;
        if (Math.Abs(this.imageBottomLeft.localPosition.x + 100) > 0.001)
        {
            updateImageCount++;
            this.imageBottomLeft.transform.Translate(new Vector3(10, 0, 0));
        }

        if (Math.Abs(this.imageBottomRight.localPosition.x - 100) > 0.001)
        {
            updateImageCount++;
            this.imageBottomRight.transform.Translate(new Vector3(-10, 0, 0));
        }

        if (Math.Abs(this.imageTopLeft.localPosition.x + 100) > 0.001)
        {
            updateImageCount++;
            this.imageTopLeft.transform.Translate(new Vector3(10, 0, 0));
        }

        if (Math.Abs(this.imageTopRight.localPosition.x - 100) > 0.001)
        {
            updateImageCount++;
            this.imageTopRight.transform.Translate(new Vector3(-10, 0, 0));
        }

        if (Math.Abs(this.imageBottomLeft.localPosition.y + 100) > 0.001)
        {
            updateImageCount++;
            this.imageBottomLeft.transform.Translate(new Vector3(0, 5, 0));
        }

        if (Math.Abs(this.imageBottomRight.localPosition.y + 100) > 0.001)
        {
            updateImageCount++;
            this.imageBottomRight.transform.Translate(new Vector3(0, 5, 0));
        }

        if (Math.Abs(this.imageTopLeft.localPosition.y - 100) > 0.001)
        {
            updateImageCount++;
            this.imageTopLeft.transform.Translate(new Vector3(0, -5, 0));
        }

        if (Math.Abs(this.imageTopRight.localPosition.y - 100) > 0.001)
        {
            updateImageCount++;
            this.imageTopRight.transform.Translate(new Vector3(0, -5, 0));
        }

        if (0 == updateImageCount )
        {
            
        }
    }
}
