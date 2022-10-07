using UnityEngine;

public class GalaxyMap : MonoBehaviour
{
    public float cooldown;
    public int minSpeed;
    public int maxSpeed;

    WindController _windController;

    // Start is called before the first frame update
    void Start()
    {
        _windController = GetComponent<WindController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            ChangeWindSpeed();
            cooldown = 8;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        _windController.Wind(1);
    }

    void ChangeWindSpeed()
    {
        int newSpeed = Random.Range(minSpeed, maxSpeed);
        _windController.windYSpeed = newSpeed;
    }
}
