using UnityEngine;

public class Portal : MonoBehaviour
{
    [HideInInspector] public GameObject partnerPortal;
    [HideInInspector] public bool outPortal = false;
    [SerializeField] float lifeSpan;
    private void Awake()
    {
        Destroy(this.gameObject, lifeSpan);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            outPortal = false;
    }
    public void Teleport(GameObject player) 
    {
        if (partnerPortal == null || outPortal)
            return;

        partnerPortal.GetComponent<Portal>().outPortal = true;

        player.transform.position = partnerPortal.transform.position;
    }
}
