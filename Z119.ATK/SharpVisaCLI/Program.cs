using System;
using System.Reflection;
using System.Diagnostics;
using NationalInstruments.Visa;
using Ivi.Visa;

namespace SharpVisaCLI
{
	public class Program
	{
		public static void List(Action<string> callback)
		{
			Exec("--list", (output)=> {
				var lines = output.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
				foreach(var line in lines) callback(line.Trim());
			});
		}
		
		public static void Send(string inst, string req, Action<string> callback)
		{
			Exec(string.Format("{0} {1}", inst, req), req.Contains("?") ? callback : null);
		}
		
		public static void Exec(string args, Action<string> callback)
		{
			using(var proc = new Process()) {
				proc.StartInfo.FileName = Assembly.GetAssembly(typeof(Program)).Location;
				proc.StartInfo.Arguments = args;
				proc.StartInfo.RedirectStandardError = true;
				proc.StartInfo.UseShellExecute = false;
				proc.StartInfo.RedirectStandardOutput = true;
				proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				proc.StartInfo.CreateNoWindow = true;
				proc.Start();
				var output = proc.StandardOutput.ReadToEnd();
				var error = proc.StandardError.ReadToEnd();
				proc.WaitForExit();
				if (proc.ExitCode != 0) {
					throw new Exception(string.Format("{0} error {1}\n{2}", 
					                                  proc.StartInfo.FileName, 
					                                  proc.ExitCode, error));
				}
				if (callback != null) callback(output);
			}
		}
		
		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += (sender, eargs) =>
			{
			    Console.Error.WriteLine("Unhandled exception: " + eargs.ExceptionObject);
			    Environment.Exit(1);
			};
			
			//some commands have spaces in them
			var cmdline = string.Join(" ", args);
			//Console.Error.WriteLine(cmdline);
			if (cmdline == "--list") {
				using(var manager = new ResourceManager()) {
					foreach(var inst in manager.Find("?*instr")) {
						Console.WriteLine(inst);
					}
				}
			} else {
				var parts = cmdline.Split(new char[]{' '}, 2);
				if (args.Length < 2 || parts.Length < 2) {
					Console.Error.WriteLine("Missing parameters");
					Environment.Exit(1);
				}
				var inst = parts[0];
				var req = parts[1];
				//Console.Error.WriteLine(string.Format("ID:{0} REQ:{1}", inst, req));
				using(var manager = new ResourceManager()) {
					using(var session = manager.Open(inst)) {
						var msg = (IMessageBasedSession)session;
						try {
							msg.FormattedIO.WriteLine(req);
							if (req.Contains("?")) {
								Console.Write(msg.FormattedIO.ReadLine());
							}
						} catch(Exception ex) {
							Console.Error.Write(ex.ToString());
							Environment.Exit(1);
						}
					}
				}			
			}
		}
	}
}