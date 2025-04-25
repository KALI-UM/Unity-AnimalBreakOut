using UnityEngine;

public class DelayNode<T> : DecoratorNode<T> where T : MonoBehaviour
{
    private float delayTime;
    private float startTime;
    private bool waiting;

    public DelayNode(T context, float delayTime) : base(context)
    {
        this.delayTime = delayTime;
        
        waiting = true;
    }

    protected override void OnStart()
    {
        base.OnStart();
        
        startTime = Time.time;
        
        waiting = true;
    }

    public override void Reset()
    {
        base.Reset();
        
        waiting = true;
    }

    protected override BTNodeState ProcessChild()
    {
        if (waiting)
        {
            if (Time.time - startTime >= delayTime)
            {
                waiting = false;
            }
            else
            {
                return BTNodeState.Running;
            }
        }

        return child.Execute();
    }
}