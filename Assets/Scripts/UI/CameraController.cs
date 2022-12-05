using UnityEngine;

public class CameraController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void BlueGoal()
    {
        anim.Play("Blue_Goal");
    }

    public void RedGoal()
    {
        anim.Play("Red_Goal");
    }
}
