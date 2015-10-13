namespace DAL.Interface.DTO
{
    public class DalAvatar : DalEntity
    {
        public string MineType { get; set; }
        public byte[] Image { get; set; }
    }
}
