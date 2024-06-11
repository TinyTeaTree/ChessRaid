using UnityEngine;

public abstract class BaseContext : MonoBehaviour
{
    protected ContextGroup<IController> _controllerGroup = new();

    private void Start()
    {
        CreateControllers();

        InitializeControllers(_controllerGroup);

        PostStart();
    }

    private void InitializeControllers(ContextGroup<IController> group)
    {
        foreach (var controller in _controllerGroup.Group)
        {
            controller.Awake(group);
        }

        foreach (var controller in _controllerGroup.Group)
        {
            controller.Start();
        }
    }

    protected abstract void CreateControllers();

    protected virtual void PostStart()
    {

    }
}