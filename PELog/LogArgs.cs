using System;

namespace PELog {
	public enum LogType {
		None,
		Net,
		Unity,
	}
	public class LogArgs {
		public string logPrefix = "#";
		public string logSeparate = ">>";

		public bool enableLog = true;
		public bool enableWain = true;
		public bool enableError = true;
		public bool enableDebug = true;

		public bool enableTime = true;
		public bool enableThreadId = true;
		public bool enableTrace = true;
		public bool enableSave = true;
		public bool enableCover = true;

		public LogType logType = LogType.None;
		public Action<string>? logFunc;
		public Action<string>? wainFunc;
		public Action<string>? errorFunc;

		public string? SaveName {
			get {
				switch(logType) {
					case LogType.Net:
						return"ConsolePELog.txt";
					case LogType.Unity:
						return"UnityPELog.txt";
					default:
						return null;
				}
			}
		}
		public string? SavePath {
			get {
				switch(logType) {
					case LogType.Net:
						return$"{AppDomain.CurrentDomain.BaseDirectory}Logs\\";
					case LogType.Unity: {
						Type? type = Type.GetType("UnityEngine.Application,UnityEngine") ?? null;
						return$"{type?.GetProperty("persistentDataPath")?.GetValue(null)}/PELogs/";
					}
					default:
						return null;
				}
			}
		}
	}
}