using System;
namespace aladang_server_api.Models
{
	public class DataResponse
	{
        public int countPage { get; set; }
        public int currenPage { get; set; }
        public object? data { get; set; }
    }
}

