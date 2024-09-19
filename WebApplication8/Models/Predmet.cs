using System;
using System.Collections.Generic;

namespace WebApplication8.Models;

public partial class Predmet
{
    public short IdPredmeta { get; set; }

    public short IdProfesora { get; set; }

    public string Naziv { get; set; } = null!;

    public short Espb { get; set; }

    public string Status { get; set; } = null!;

    public virtual Profesor IdProfesoraNavigation { get; set; } = null!;

    public virtual ICollection<StudentPredmet> StudentPredmets { get; set; } = new List<StudentPredmet>();

    public virtual ICollection<Ispit>? Ispits { get; set; }
}
