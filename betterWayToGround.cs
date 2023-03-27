using UnityEngine;
public class betterWayToGround : MonoBehaviour
{
    public float allowedGroundAngle = 30.0f;
    [SerializeField] private bool isGrounded = false;
    private float groundAngleRelative;
    private float groundAngle;

    private Vector3 groundNormal, groundPoint;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        Vector3 contactNormal = Vector3.zero;
        Vector3 contactPoint = Vector3.zero;

        foreach (ContactPoint cp in contactPoints)
        {
            contactNormal += cp.normal;
            contactPoint += cp.point;
        }
        contactNormal /= contactPoints.Length;
        contactPoint /= contactPoints.Length;

        groundAngleRelative = Mathf.Round(Vector3.SignedAngle(Vector3.Cross(contactNormal, -transform.right),
            Vector3.right,
            Vector3.forward) * 100.0f) / 100.0f;
        groundAngle = Mathf.Abs(groundAngleRelative);

        if (groundAngle <= allowedGroundAngle)
        {
            groundNormal = contactNormal;
            groundPoint = contactPoint;
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
