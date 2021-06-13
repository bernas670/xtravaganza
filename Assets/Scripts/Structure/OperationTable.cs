using UnityEngine;
using TMPro;

public class OperationTable : MonoBehaviour
{
    public Door _door;
    public TextMeshProUGUI text;

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            if (_door.isLocked)
            {
                text.text = "Press E to unlock a door...";
            }
            else
            {
                text.text = "Door is already unlocked...";
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
        if (Input.GetKeyDown(KeyCode.E) && _door.isLocked)
        {
            Renderer r = gameObject.GetComponent<MeshRenderer>();
            r.materials[2].color = Color.green;
            _door.unlockDoor();
            text.text = "Door unlocked!";
        }
    }
}
