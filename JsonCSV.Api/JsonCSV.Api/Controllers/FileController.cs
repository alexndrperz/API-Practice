using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace JsonCSV.Api.Controllers
{
	[ApiController]
	public class FileController : Controller
	{
		private readonly FileExtensionContentTypeProvider _extensionContentProvider;

		public FileController(FileExtensionContentTypeProvider extensionContentProvider)
		{
			_extensionContentProvider = extensionContentProvider ?? throw new ArgumentNullException(nameof(extensionContentProvider));	
		}

		[HttpGet("api/files/{fileid}")]
		public ActionResult GetFile(string fileid)
		{
			string? path = "Report_Equality.pdf";

			if (!System.IO.File.Exists(path))
			{
				return NotFound();
			}

			if(!_extensionContentProvider.TryGetContentType(path, out var contentType))
			{
				contentType = "application/octet-stream";
			}
			var bytes = System.IO.File.ReadAllBytes(path);
			return File(bytes, contentType, "asd");

		}
	}
}
