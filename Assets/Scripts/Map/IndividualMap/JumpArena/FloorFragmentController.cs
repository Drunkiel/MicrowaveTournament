using UnityEngine;

public class FloorFragmentController : MonoBehaviour
{
    public float maxHeightToLift;
    public float defaultHeight;

    public bool hide;

    // Start is called before the first frame update
    void Start()
    {
        maxHeightToLift = transform.position.y + 1.5f;
        defaultHeight = transform.position.y;
    }

    void Update()
    {
        CheckPosition();
    }

    void CheckPosition()
    {
        if (transform.position.y >= maxHeightToLift)
        {
            hide = true;
        }
        else
        {
            print(transform.position.y);
            hide = false;
        }

        if (hide)
        {
            Hide();
        }
    }

    public void Lift()
    {
        while (transform.position.y <= maxHeightToLift)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 4);
        }
    }

    public void Hide()
    {
        while (transform.position.y >= defaultHeight)
        {
            transform.Translate(Vector3.back * Time.deltaTime * 4);
        }
    }
}
