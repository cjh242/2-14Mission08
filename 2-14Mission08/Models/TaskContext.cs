using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _2_14Mission08.Models;

public partial class TaskContext : DbContext
{
    public TaskContext()
    {
    }

    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<TaskList> TaskLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=task.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryID");
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(e => e.TaskId);

            entity.ToTable("TaskList");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.DueDate).HasColumnName("Due Date");

            entity.HasOne(d => d.Category).WithMany(p => p.TaskLists).HasForeignKey(d => d.CategoryId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
