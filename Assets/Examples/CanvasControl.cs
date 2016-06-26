using UnityEngine;
using System.Collections;
using System;
using Amazon;
using UnityEngine.SceneManagement;


public class CanvasControl : MonoBehaviour {
    public Canvas myCanvas;
    
    public RectTransform imageTopLeft;
    public RectTransform imageTopRight;
    public RectTransform imageBottomLeft;
    public RectTransform imageBottomRight;
    public RectTransform imageLogo;

    public SQSMessageListener msgListener;

    private bool panelReady = false;
    private string IdentityPoolId = "us-east-1:05c9e46e-8369-46b4-b0fc-359c3186dab0";
    private string QueueName = "colorexpert";
    private string queueUrl = "https://sqs.us-east-1.amazonaws.com/209409240985/colorexpert" + "/?Action=SetQueueAttributes&Attribute.Name=ReceiveMessageWaitTimeSeconds&Attribute.Value=20";

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

        this.msgListener = new SQSMessageListener(IdentityPoolId, QueueName, queueUrl);
        UnityInitializer.AttachToGameObject(this.gameObject);

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

        if (0 == updateImageCount && !this.panelReady)
        {
            Debug.Log("Finishing moving panels, and start messaging");
            StartCoroutine(this.msgListener.RepeatRetrieveMessage(0.1F));
            this.LoadModel("temple");
            this.panelReady = true;
        }
    }

    private void LoadModel(string modelName)
    {
        Debug.Log("@Come to load scene " + modelName);
        //var ourObj = Resources.Load(modelName);
        //ourObj = Resources.Load("fence");
        //var initiated = Object.Instantiate(ourObj);
        SceneManager.LoadScene(modelName.ToLower(), LoadSceneMode.Additive);
        Debug.Log("@After loading the " + modelName);
    }
}
