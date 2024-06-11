public abstract class DataBox
{
    public const string Default_Id = "Data";
    public virtual string Id => Default_Id;

    public virtual void Initialize()
    {

    }
}