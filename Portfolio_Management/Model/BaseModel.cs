using System;

namespace Portfolio_Management.Model
{
    public class BaseModel
    {
        public long Id { get; set; }
        public string AuditLog { get; set; }
        public DateTime RecDate { get; set; } = DateTime.Now;
    }
}