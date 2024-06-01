public class WagMonoton<T> : WagBehaviour
    where T : WagMonoton<T>
{
    public static T _ { get; private set; }


    
    protected override void Awake()
    {
        base.Awake();

        if (_ == null )
        {
            _ = (T) this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (this == _)
        {
            _ = null;
        }
    }
}
