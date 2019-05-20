using UnityEngine;

public class EventEffect : ScriptableObject {

    public virtual void ApplyEffect() {
        GameManager.instance.eventEffect = this;
    }

    public virtual void UnapplyEffect() { }
}
