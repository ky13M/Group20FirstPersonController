using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();  
    }
    public void Pickup(Transform holdpoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetParent(holdpoint);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        rb.useGravity = true;
        transform.SetParent(null);
    }
    public void Throw(Vector3 impulse)
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(impulse, ForceMode.Impulse);

    }

    public void MoveToHoldPoint(Vector3 targetPostion)
    {
        rb.MovePosition(targetPostion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
