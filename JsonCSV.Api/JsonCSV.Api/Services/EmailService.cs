namespace JsonCSV.Api.Services
{
	public class EmailService : IMailService
	{
		private readonly string? _mailTo = string.Empty;
		private readonly string? _mailFrom = string.Empty;

		public EmailService(IConfiguration config)
		{
			_mailTo = config["ServerMailSettings:mailFromSending"];
			_mailFrom = config["ServerMailSettings:mailToSend"] ;
		}
		public void SendEmail()
		{
			Console.WriteLine($"Enviando reporte de error de {_mailFrom} a {_mailTo} en {nameof(EmailService)}");
		}
	}
}
