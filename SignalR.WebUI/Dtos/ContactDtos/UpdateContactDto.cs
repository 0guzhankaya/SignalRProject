﻿namespace SignalR.WebUI.Dtos.ContactDtos
{
	public class UpdateContactDto
	{
		public int ContactId { get; set; }
		public string Location { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string FooterDescription { get; set; }
	}
}
