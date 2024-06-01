using System;
using System.Collections.Generic;


public class WagSingleton<T>
    where T : class, new()
{
    static T _instance;
    

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                if (_instance is IResetable resetable)
                {
                    WagSingletonSystem.Resetables.Add(resetable);
                }
            }

            return _instance;
        }
    }
}
