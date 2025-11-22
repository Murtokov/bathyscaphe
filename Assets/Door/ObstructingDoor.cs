using UnityEngine;

public class ObstructingDoor : MonoBehaviour
{
    public enum MovementType { Vertical, Horizontal }
    
    [Header("Настройки движения")]
    public MovementType movementType = MovementType.Vertical;
    public float moveDistance = 2f;
    public float speed = 1f;
    
    private Vector3 startPosition;
    private Vector3 downPosition;
    private Vector3 upPosition;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private int currentPhase = 0;
    
    void Start()
    {
        startPosition = transform.position;
        CalculatePositions();
    }
    
    void Update()
    {
        Vector3 targetPosition = GetTargetPosition();
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPosition, 
            speed * Time.deltaTime
        );
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            currentPhase = (currentPhase + 1) % 4;
        }
    }
    
    private void CalculatePositions()
    {
        downPosition = startPosition + Vector3.down * moveDistance;
        upPosition = startPosition + Vector3.up * moveDistance;
        leftPosition = startPosition + Vector3.left * moveDistance;
        rightPosition = startPosition + Vector3.right * moveDistance;
    }
    
    private Vector3 GetTargetPosition()
    {
        if (movementType == MovementType.Vertical)
        {
            switch(currentPhase)
            {
                case 0: return downPosition;
                case 1: return startPosition;
                case 2: return upPosition;
                case 3: return startPosition;
                default: return startPosition;
            }
        }
        else // Horizontal
        {
            switch(currentPhase)
            {
                case 0: return leftPosition;
                case 1: return startPosition;
                case 2: return rightPosition;
                case 3: return startPosition;
                default: return startPosition;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Vector3 currentStartPos = Application.isPlaying ? startPosition : transform.position;
        float currentDistance = Application.isPlaying ? moveDistance : moveDistance;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(currentStartPos, Vector3.one * 0.5f);
        
        if (movementType == MovementType.Vertical)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(currentStartPos + Vector3.down * currentDistance, Vector3.one * 0.5f);
            Gizmos.DrawWireCube(currentStartPos + Vector3.up * currentDistance, Vector3.one * 0.5f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentStartPos + Vector3.down * currentDistance, 
                           currentStartPos + Vector3.up * currentDistance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(currentStartPos + Vector3.left * currentDistance, Vector3.one * 0.5f);
            Gizmos.DrawWireCube(currentStartPos + Vector3.right * currentDistance, Vector3.one * 0.5f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentStartPos + Vector3.left * currentDistance, 
                           currentStartPos + Vector3.right * currentDistance);
        }
    }
}