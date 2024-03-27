using System;
namespace Net7.Wasm.Models
{
	public class BookingModel
	{
		public Int64 prod_oid { get; set; }
        public string prod_name { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }
    }
}

