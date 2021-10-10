using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed = 1f;
    public float SpeedPhase3 = 30f;

    // Update is called once per frame
    void Update()
    {
        var speed = MusicManager.index == 3 ? SpeedPhase3 : Speed;
        transform.Rotate(0,0,speed * Time.deltaTime);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.rotation = Quaternion.identity;
        }
    }
}
