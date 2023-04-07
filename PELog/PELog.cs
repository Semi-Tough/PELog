using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace PELog {
	public static class ExtensionMethod {
		public static void Log(this object o, object obj) {
			PELog.Log(obj);
		}
		public static void Wain(this object o, object obj) {
			PELog.Wain(obj);
		}
		public static void Error(this object o, object obj) {
			PELog.Error(obj);
		}
		public static void Debug(this object o, object obj) {
			PELog.Debug(obj);
		}
		public static void Trace(this object o, object obj) {
			PELog.Trace(obj);
		}
		public static void ColorLog(this object o, object obj, LogColor color) {
			PELog.ColorLog(obj, color);
		}
	}

	public static class PELog {
		private const string LogLock = "PELog";

		private static ILog logger = null!;
		private static LogArgs logArgs = null!;
		private static StreamWriter? streamWriter;

		public static void InitLog(LogArgs args) {
			logArgs = args;
			
			switch(logArgs.logType) {
				case LogType.Net:
					logger = new NetLog();
					break;
				case LogType.Unity:
					logger = new UnityLog(
						logArgs.logFunc,
						logArgs.wainFunc,
						logArgs.errorFunc
					);
					break;
			}
			CreateLocalFile();
		}

		public static void Log(object obj) {
			if(logArgs.enableLog == false) {
				return;
			}
			string msg = DecorateLog(obj.ToString());
			lock(LogLock) {
				logger.Log(msg);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[L]{msg}");
				}
			}
		}
		public static void Wain(object obj) {
			if(logArgs.enableWain == false) {
				return;
			}

			string msg = DecorateLog(obj.ToString(),logArgs.enableTrace);

			lock(LogLock) {
				logger.Wain(msg);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[W]{msg}");
				}
			}
		}
		public static void Error(object obj) {
			if(logArgs.enableError == false) {
				return;
			}

			string msg = DecorateLog(obj.ToString(), logArgs.enableTrace);
			lock(LogLock) {
				logger.Error(msg);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[E]{msg}");
				}
			}
		}
		public static void Debug(object obj) {
			if(logArgs.enableDebug == false) {
				return;
			}

			string msg = DecorateLog(obj.ToString(),logArgs.enableTrace);

			lock(LogLock) {
				logger.Debug(msg);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[D]{msg}");
				}
			}
		}
		public static void ColorLog(object obj, LogColor color) {
			if(logArgs.enableLog == false) {
				return;
			}

			string msg = DecorateLog(obj.ToString());

			lock(LogLock) {
				logger.Log(msg, color);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[L]{msg}");
				}
			}
		}
		public static void Trace(object obj) {
			if(logArgs.enableLog == false) {
				return;
			}

			string msg = DecorateLog(obj.ToString(), logArgs.enableTrace);

			lock(LogLock) {
				logger.ColorLog(msg, LogColor.Blue);
				if(logArgs is{ enableSave: true }) {
					WriteToFile($"[T]{msg}");
				}
			}
		}

		private static string DecorateLog(string msg, bool isTrac = false) {
			StringBuilder sb = new StringBuilder(logArgs.logPrefix, 100);
			if(logArgs.enableTime) {
				sb.Append(GetTime());
			}
			if(logArgs.enableThreadId) {
				sb.Append(GetThreadId());
			}

			sb.Append($" {logArgs.logSeparate} {msg}");

			if(isTrac) {
				sb.Append(GetStackTrace());
			}
			return sb.ToString();
		}

		private static string GetTime() {
			return DateTime.Now.ToString("hh:mm:ss--fff");
		}
		private static string GetThreadId() {
			return$"ThreadID:{Thread.CurrentThread.ManagedThreadId.ToString()}";
		}
		private static string GetStackTrace() {
			StackTrace st = new StackTrace(3, true);
			StringBuilder trackInfo = new StringBuilder(100);

			for(int i = 0; i < st.FrameCount; i++) {
				StackFrame sf = st.GetFrame(i);
				trackInfo.Append($"\n{sf.GetFileName()}::{sf.GetMethod()} line:{sf.GetFileLineNumber().ToString()}");
			}

			return$"\nStackTrace: {trackInfo}";
		}


		private static void CreateLocalFile() {
			if(logArgs.enableSave) {
				if(logArgs.enableCover) {
					string path = logArgs.SavePath + logArgs.SaveName;
					try {
						if(Directory.Exists(logArgs.SavePath)) {
							if(File.Exists(path)) {
								File.Delete(path);
							}
						}
						else {
							if(logArgs.SavePath != null) {
								Directory.CreateDirectory(logArgs.SavePath);
								streamWriter = File.AppendText(path);
								streamWriter.AutoFlush = true;
							}
						}
					}
					catch {
						streamWriter = null;
					}
				}
				else {
					string prefix = DateTime.Now.ToString("yyyyMMdd@HH-mm-ss");
					string path = logArgs.SavePath + prefix + logArgs.SaveName;
					try {
						if(Directory.Exists(logArgs.SavePath) == false) {
							if(logArgs.SavePath != null) Directory.CreateDirectory(logArgs.SavePath);
							streamWriter = File.AppendText(path);
							streamWriter.AutoFlush = true;
						}
					}
					catch {
						streamWriter = null;
					}
				}
			}
		}
		private static void WriteToFile(string msg) {
			try {
				streamWriter?.WriteLine($"{msg}");
			}
			catch {
				streamWriter = null;
			}
		}
	}
}