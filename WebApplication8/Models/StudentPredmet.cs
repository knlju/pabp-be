using System;
using System.Collections.Generic;

namespace WebApplication8.Models;

public partial class StudentPredmet
{
    public int IdStudenta { get; set; }

    public short IdPredmeta { get; set; }

    public string SkolskaGodina { get; set; } = null!;

    public virtual Predmet? IdPredmetaNavigation { get; set; }

    public virtual Student? IdStudentaNavigation { get; set; }
}
