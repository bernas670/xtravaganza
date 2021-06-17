using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class ShipTable : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject screen;
    private int totalEnemies = 1;

    private void OnCollisionEnter(Collision col)
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (col.transform.tag == "Player")
        {
            if (totalEnemies == 0)
            {
                text.text = "Press E to escape...";
                Renderer r = gameObject.GetComponent<MeshRenderer>();
                r.materials[2].color = Color.green;
            }
            else
            {
                text.text = "You have to clear the room first...";
            }
            text.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            text.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay()
    {
        if (Input.GetKeyDown(KeyCode.E) && totalEnemies == 0)
        {
            screen.SetActive(true);
            StartCoroutine(Fadeout());
        }
    }

    private IEnumerator Fadeout()
    {
        Image img = screen.GetComponent<Image>();

        while (img.color.a < 1)
        {
            Color color = img.color;
            color.a += 0.01f;
            img.color = color;

            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
