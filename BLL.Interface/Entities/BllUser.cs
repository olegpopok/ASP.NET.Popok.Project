using System;

namespace BLL.Interface.Entities
{
    public class BllUser : BllEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public int PostCount { get; set; }
    }
}
