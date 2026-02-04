using UnityEngine;

public abstract class PassiveObject : ScriptableObject
{
    public virtual void OnApply(Entity entity) { }
    public virtual void OnRemove(Entity entity) { }
}
