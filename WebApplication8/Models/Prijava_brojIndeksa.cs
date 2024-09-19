namespace WebApplication8.Models
{
    public class Prijava_brojIndeksa
    {
        public int IdStudenta { get; set; }
        public int IdIspita { get; set; }

        public virtual Ispit IdIspitaNavigation { get; set; } = null!;
        public virtual Student IdStudentaNavigation { get; set; } = null!;
    }
}
