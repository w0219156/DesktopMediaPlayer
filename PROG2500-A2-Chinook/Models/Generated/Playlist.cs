﻿using System;
using System.Collections.Generic;

namespace PROG2500_A2_Chinook.Models;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; } = new List<Track>();
}
