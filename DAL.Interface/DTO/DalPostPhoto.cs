namespace DAL.Interface.DTO
{
    public class DalPostPhoto : DalEntity
    {
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
    }
}
