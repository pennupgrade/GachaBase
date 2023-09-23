using UnityEngine;

public class Collision : MonoBehaviour
{

    // Update is called once per frame
    public GameObject player;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.gameObject != player) return;
        if(!player.GetComponent<Movement>().tackleable) return;
        Debug.Log("Collision entered");
    }
}
