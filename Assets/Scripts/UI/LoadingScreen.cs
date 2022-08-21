using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject UI;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator Load_Start()
    {
        StartAnimation();

        yield return new WaitForSeconds(1.2f);

        Deactivate();
    }

    public void StartAnimation()
    {
        UI.SetActive(true);
        anim.Play("StartLoading");
    }

    public void Deactivate()
    {
        UI.SetActive(false);
    }
}
