using System;
using System.Collections.Generic;

namespace WebApplication8.Models;

public partial class Ispit
{
    public int IdIspita { get; set; }

    public int IdRoka { get; set; }

    public short IdPredmeta { get; set; }

    public DateOnly Datum { get; set; }

    public virtual IspitniRok IdRokaNavigation { get; set; } = null!;

    public virtual ICollection<Zapisnik> Zapisniks { get; set; } = new List<Zapisnik>();

    public virtual Predmet IdPredmetaNavigation { get; set; } = null!;

    public virtual ICollection<Prijava_brojIndeksa>? Prijava_BrojIndeksas { get; set; }
}
