using System;

namespace EventManagement.Models.Models
{
    public class Event
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime Date { get; set; } 
        public string Location { get; set; } 
    }
}