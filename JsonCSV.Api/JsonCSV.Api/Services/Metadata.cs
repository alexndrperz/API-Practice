namespace JsonCSV.Api.Services
{
	public class Metadata
	{
		public int numberPage { get; set; }	
		public int totalPages { get; set; }
		public int sizePage { get; set; }
        public int CollectioSize { get; set; }

		public Metadata(int number_page, int size_page, int Collection_size) {
			numberPage = number_page;
			totalPages = (int)Math.Ceiling((decimal)Collection_size/size_page);
			sizePage = size_page;
			CollectioSize = Collection_size;	
		}
    }
}
