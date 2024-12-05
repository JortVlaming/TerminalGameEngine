using System.Diagnostics;

namespace GameEngine;

public class GameEngine
{
    private Logger _logger;
    private bool _isRunning;

    private const int _frameTarget = 60;
    private readonly int _frameTime = 1000 / _frameTarget;
    
    public GameEngine()
    {
        _logger = new Logger("GameEngine", LogMode.FILE);
    }

    public void start()
    {
        _isRunning = true;
        loop();
        _logger.Info("Running Engine at " + _frameTarget + " frames per second.");
    }

    public void stop()
    {
        _isRunning = false;
    }

    private void loop()
    {
        var stopwatch = new Stopwatch();

        while (_isRunning)
        {
            stopwatch.Restart();

            _logger.Debug("FRAME");
            
            // Game logic and rendering would go here

            stopwatch.Stop();

            int elapsedTime = (int)stopwatch.ElapsedMilliseconds;
            int sleepTime = _frameTime - elapsedTime;

            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }
    }
}