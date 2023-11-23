﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Common.Entities;

[Index("CategoryName", Name = "CategoryName")]
public partial class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int CategoryId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar (15)")]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Picture { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}