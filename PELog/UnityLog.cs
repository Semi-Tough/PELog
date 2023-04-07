using System;

namespace PELog {
	public class UnityLog : ILog {
		#region 反射
		// private readonly Type? type;
		// public UnityLog() {
		// 	type = Type.GetType("UnityEngine.Debug,UnityEngine") ?? null;
		// 	if(type != null) {
		// 		Log("PELog Init Done.", LogColor.None);
		// 	}
		// 	else {
		// 		Wain("PELog Init Failed.", LogColor.Yellow);
		// 	}
		// }
		// public void Log(string msg, LogColor color) {
		// 	if(color != LogColor.None) {
		// 		msg = AddColor(msg, color);
		// 	}
		// 	type?.GetMethod("Log", new[]{ typeof(object) })?.Invoke(null, new object[]{ msg });
		// }
		// public void Wain(string msg, LogColor color) {
		// 	if(color != LogColor.None) {
		// 		msg = AddColor(msg, color);
		// 	}
		// 	type?.GetMethod("LogWarning", new[]{ typeof(object) })?.Invoke(null, new object[]{ msg });
		// }
		// public void Error(string msg, LogColor color) {
		// 	if(color != LogColor.None) {
		// 		msg = AddColor(msg, color);
		// 	}
		// 	type?.GetMethod("LogError", new[]{ typeof(object) })?.Invoke(null, new object[]{ msg });
		// }
		// public void Debug(string msg, LogColor color) {
		// 	if(color != LogColor.None) {
		// 		msg = AddColor(msg, color);
		// 	}
		// 	type?.GetMethod("Log", new[]{ typeof(object) })?.Invoke(null, new object[]{ msg });
		// }
		// public void ColorLog(string msg, LogColor color) {
		// 	if(color != LogColor.None) {
		// 		msg = AddColor(msg, color);
		// 	}
		// 	type?.GetMethod("Log", new[]{ typeof(object) })?.Invoke(null, new object[]{ msg });
		// }
		#endregion
		private Action<string>? logFunc;
		private Action<string>? wainFunc;
		private Action<string>? errorFunc;
		public UnityLog(Action<string>? logFunc, Action<string>? wainFunc, Action<string>? errorFunc) {
			this.logFunc = logFunc;
			this.wainFunc = wainFunc;
			this.errorFunc = errorFunc;
		}

		public void Log(string msg, LogColor color) {
			if(color != LogColor.None) {
				msg = AddColor(msg, color);
			}
			logFunc?.Invoke(msg);
		}
		public void Wain(string msg, LogColor color) {
			if(color != LogColor.None) {
				msg = AddColor(msg, color);
			}
			wainFunc?.Invoke(msg);
		}
		public void Error(string msg, LogColor color) {
			if(color != LogColor.None) {
				msg = AddColor(msg, color);
			}
			errorFunc?.Invoke(msg);
		}
		public void Debug(string msg, LogColor color) {
			if(color != LogColor.None) {
				msg = AddColor(msg, color);
			}
			logFunc?.Invoke(msg);
		}
		public void ColorLog(string msg, LogColor color) {
			if(color != LogColor.None) {
				msg = AddColor(msg, color);
			}
			logFunc?.Invoke(msg);
		}

		private static string AddColor(string msg, LogColor color) {
			switch(color) {
				case LogColor.Red:
					msg = $"<color=#ff4757>{msg}</color>";
					break;
				case LogColor.Green:
					msg = $"<color=#2ed573>{msg}</color>";
					break;
				case LogColor.Blue:
					msg = $"<color#3742fa=>{msg}</color>";
					break;
				case LogColor.Cyan:
					msg = $"<color=#70a1ff>{msg}</color>";
					break;
				case LogColor.Magenta:
					msg = $"<color=#ff6b81>{msg}</color>";
					break;
				case LogColor.Yellow:
					msg = $"<color=#ffa502>{msg}</color>";
					break;
				case LogColor.Black:
					msg = $"<color=#2f3542>{msg}</color>";
					break;
			}
			return msg;
		}
	}
}