using UnityEngine;

public interface ITouchReceivable
{
    void OnTouchBegin(Vector2 position);
    void OnTouchDrag(Vector2 position);
    void OnEnd(Vector2 position);
}
