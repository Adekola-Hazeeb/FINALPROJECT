namespace FINALPROJECT.Domain.Entities
{
    public  abstract class Auditables
    {
        public string Id = Guid.NewGuid().ToString().Substring(0,5);
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
