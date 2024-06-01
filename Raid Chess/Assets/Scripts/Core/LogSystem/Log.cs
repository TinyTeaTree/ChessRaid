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
        _._defaultLogger.Critical(critical);
    }

    public static void Editor(string editorTrace)
    {
        _._defaultLogger.Editor(editorTrace);
    }

    public static void Error(string error)
    {
        _._defaultLogger.Error(error);
    }

    public static void Exception(Exception e)
    {
        _._defaultLogger.Exception(e);
    }

    public static void Message(string message)
    {
        _._defaultLogger.Message(message);
    }

    public static void Warning(string warning)
    {
        _._defaultLogger.Warning(warning);
    }
}
