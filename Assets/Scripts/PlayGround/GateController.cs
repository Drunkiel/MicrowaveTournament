using UnityEngine;

public class GateController : MonoBehaviour
{
    public float cooldown;
    public float resCooldown;

    public bool[] steps;
    public int actualStep;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            actualStep++;
            OpenGate();
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void OpenGate()
    {
        switch (actualStep)
        {
            case 0:
                anim.Play("DoNothing");
                break;

            case 1:
                anim.Play("OpenGate_0");
                break;

            case 2:
                anim.Play("OpenGate_1");
                break;

            case 3:
                anim.Play("OpenGate_2");
                break;

            case 4:
                anim.Play("OpenGate_3");
                break;

            case 5:
                anim.Play("OpenGate_4");
                break;
        }
            
                
    }
}
