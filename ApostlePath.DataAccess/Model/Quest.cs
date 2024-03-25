using System.ComponentModel.DataAnnotations;

namespace ApostlePath.DataAccess.Model
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        public string Title { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Experience { get; set; }

        [MaxLength(100)]
        public string Challenge { get; set; } = string.Empty;
        public DateTime LastProgress { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
