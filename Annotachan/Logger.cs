using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.conf", Watch = true)]

namespace Annotachan {
    public class Logger {
        private static string GetClassName(string path) {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }
        public static void Info(string text, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Info($"{GetClassName(path)} - {text}");
        }
        public static void Info(string text, Exception ex, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Info($"{GetClassName(path)} - {text}", ex);
        }

        public static void Warn(string text, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Warn($"{GetClassName(path)} - {text}");
        }
        public static void Warn(string text, Exception ex, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Warn($"{GetClassName(path)} - {text}", ex);
        }

        public static void Debug(string text, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Debug($"{GetClassName(path)} - {text}");
        }
        public static void Debug(string text, Exception ex, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Debug($"{GetClassName(path)} - {text}", ex);
        }


        public static void Error(string text, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Error($"{GetClassName(path)} - {text}");
        }
        public static void Error(string text, Exception ex, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Error($"{GetClassName(path)} - {text}", ex);
        }

        public static void Fatal(string text, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Error($"{GetClassName(path)} - {text}");
        }
        public static void Fatal(string text, Exception ex, [CallerFilePath] string path = "", [CallerMemberName] string name = "") {
            log4net.LogManager.GetLogger(name).Error($"{GetClassName(path)} - {text}", ex);
        }
    }
}