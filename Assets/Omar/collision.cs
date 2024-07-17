using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffSet, rightOffset, leftOffset, upOffset;
  
    private Color debugCollisionColor = Color.red;
    public bool onGround, onWall,onRightWall,onLeftWall;
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var poistions = new Vector2[] { bottomOffSet, rightOffset, leftOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffSet, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + upOffset, collisionRadius);


    }
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffSet, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer);

    }
}
