using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("BottomGround") || 
            collision.gameObject.CompareTag("TopGround"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("BottomGround") || 
            collision.gameObject.CompareTag("TopGround"))
        {
            isGrounded = false;
        }
    }
}
