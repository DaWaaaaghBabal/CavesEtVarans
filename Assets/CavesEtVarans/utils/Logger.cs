using System;
namespace CavesEtVarans.utils
{
	public class Logger {
		public class Level {
			public static readonly Level TRACE = new Level(0, "TRACE");
			public static readonly Level DEBUG = new Level(1, "DEBUG");
			public static readonly Level INFO = new Level(2, "INFO");
			public static readonly Level WARNING = new Level(3, "WARNING");
			public static readonly Level ERROR = new Level(4, "ERROR");
			public readonly int severity;
			public readonly string label;

			public Level(int severity, string label) {
				this.severity = severity;
				this.label = label;
			}

		}
		private String name;
		public int threshold;

		public static Logger GetLogger(string name) {
			Logger logger = new Logger();
			logger.name = name;
			logger.threshold = 0;
			return logger;
		}


		public void Log(String message, Level level) {
			if (level.severity >= threshold) {
				DisplayMessage (level, message);
			}
		}
		public void Debug(string message) {
			Log(message, Level.DEBUG);
		}
		public void Trace(string message) {
			Log(message, Level.TRACE);
		}
		public void Info(string message) {
			Log(message, Level.INFO);
		}
		public void Warn(string message) {
			Log(message, Level.WARNING);
		}
		public void Error(string message) {
			Log(message, Level.ERROR);
		}



		private void DisplayMessage(Level level, string message) {
			string line = level.label + " | " + name + " : " + message;
#if (UNITY)
			UnityEngine.Debug.Log(line);
#else
            Console.Out.WriteLine(line);
#endif
		}
	}
}
