namespace JsonCSV.Api.Services
{
	public class CloudEmailService : IMailService
	{
		private string? _mailTo { get; set; }
		private string? _mailFrom { get; set; }
		private string? _server { get;} = "localhost:9090";

		public CloudEmailService(string? server)
		{
			_server = server;
		}

		public void SendEmail()
		{
			Console.WriteLine($"Enviando reporte de error de {_mailFrom} a {_mailTo} en {nameof(CloudEmailService)} "  +
				$"en el Server {_server}");
		}
	}
}
