namespace PELog {
	public interface ILog {
		void Log(string msg, LogColor color = LogColor.None);
		void Wain(string msg, LogColor color = LogColor.Yellow);
		void Error(string msg, LogColor color = LogColor.Red);
		void Debug(string msg, LogColor color = LogColor.Green);
		void ColorLog(string msg, LogColor color);
	}
	public enum LogColor {
		None,
		Red,
		Green,
		Blue,
		Cyan,
		Magenta,
		Yellow,
		Black
	}
	
}