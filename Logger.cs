namespace GameEngine;

public class Logger
{
    private string _prefix;
    
    private LogLevel _logLevel;
    private LogMode _logMode;
    
    private StreamWriter _logStream;

    public Logger(string prefix, LogMode logMode)
    {
        _prefix = prefix;
        _logMode = logMode;
        if (logMode == LogMode.FILE || logMode == LogMode.STANDARD)
        {
            try
            {
                if (!Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            _logStream =
                new StreamWriter(new FileStream("logs/" + DateTime.Now.ToString("s") + ".log", FileMode.Create));
        }
    }

    private string generateLogMessage(string message, LogLevel logLevel)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        
        return $"[{logLevel}] [{_prefix} {time}] {message}";
    }
    
    public void LogToFile(string message, LogLevel logLevel)
    {
        _logStream.WriteLine(generateLogMessage(message, logLevel));
        _logStream.Flush();
    }
    
    private void LogWithTime(string message, LogLevel level)
    {
        if (_logMode == LogMode.STANDARD || _logMode == LogMode.FILE)
        {
            LogToFile(message, level);
        }

        if (_logMode == LogMode.STANDARD || _logMode == LogMode.CONSOLE)
        {
            if (_logLevel > level)
            {
                return;
            }
            Console.WriteLine(generateLogMessage(message, level));
        }
    }

    public void SetPrefix(string prefix)
    {
        _prefix = prefix;
    }

    public string GetPrefix()
    {
        return _prefix;
    }

    public void Log(string message, LogLevel level)
    {
        LogWithTime(message, level);
    }

    public void Info(string message)
    {
        LogWithTime(message, LogLevel.INFO);
    }

    public void Debug(string message)
    {
        LogWithTime(message, LogLevel.DEBUG);
    }

    public void Warn(string message)
    {
        LogWithTime(message, LogLevel.WARN);
    }

    public void Error(string message)
    {
        LogWithTime(message, LogLevel.ERROR);
    }
}