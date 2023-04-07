using System;

namespace PELog {
	public class NetLog : ILog {
		public void Log(string msg, LogColor color) {
			ColorLog(msg, color);
		}
		public void Wain(string msg, LogColor color) {
			ColorLog(msg, color);
		}
		public void Error(string msg, LogColor color) {
			ColorLog(msg, color);
		}
		public void Debug(string msg, LogColor color) {
			ColorLog(msg, color);
		}
		public void ColorLog(string msg, LogColor color) {
			switch(color) {
				case LogColor.Red:
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
				case LogColor.Green:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case LogColor.Blue:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case LogColor.Magenta:
					Console.ForegroundColor = ConsoleColor.Magenta;
					break;
				case LogColor.Yellow:
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					break;
				case LogColor.Cyan:
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case LogColor.Black:
					Console.ForegroundColor = ConsoleColor.Black;
					break;
			}
			Console.WriteLine(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}