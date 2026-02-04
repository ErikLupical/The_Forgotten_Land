using UnityEngine;

public abstract class StatusObject : ScriptableObject
{
    public float duration;

    public virtual void OnApply(Entity entity) { }
    public virtual void OnExpire(Entity entity) { }
}
