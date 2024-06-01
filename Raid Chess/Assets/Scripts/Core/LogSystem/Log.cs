using System;

public class Log : WagSingleton<Log>
{
    ILog _defaultLogger = new DefaultLogger();

    public void SetDefaultLogger(ILog logger)
    {
        _defaultLogger = logger;
    }

    public static void Critical(string critical)
    {
        Instance._defaultLogger.Critical(critical);
    }

    public static void Editor(string editorTrace)
    {
        Instance._defaultLogger.Editor(editorTrace);
    }

    public static void Error(string error)
    {
        Instance._defaultLogger.Error(error);
    }

    public static void Exception(Exception e)
    {
        Instance._defaultLogger.Exception(e);
    }

    public static void Message(string message)
    {
        Instance._defaultLogger.Message(message);
    }

    public static void Warning(string warning)
    {
        Instance._defaultLogger.Warning(warning);
    }
}
