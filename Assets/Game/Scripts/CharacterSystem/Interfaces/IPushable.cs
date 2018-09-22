using UnityEngine;

public interface IPushable
{ 
    Transform GetTransform();

    void Push(Vector2 targetDirection); 
}
