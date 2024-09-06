﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PostagensApi.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    [JsonIgnore]
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    [JsonIgnore]
    public virtual ICollection<Post> Posts { get; set; } 

}