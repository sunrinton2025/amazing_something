using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float leftX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;
        float rightX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x;

        if (transform.position.x > leftX && transform.position.x < rightX)
        {
            SceneManager.LoadScene("BossScene");
        }
    }
}
