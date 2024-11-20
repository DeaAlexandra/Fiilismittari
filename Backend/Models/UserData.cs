using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProject.Models
{
    [Table("UserDatas")]
    public class UserData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}