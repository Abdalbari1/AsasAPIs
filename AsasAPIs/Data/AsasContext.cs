using System;
using System.Collections.Generic;
using AsasAPIs.Models;
using Microsoft.EntityFrameworkCore;
namespace AsasAPIs.Data;

public partial class AsasContext : DbContext
{
    public AsasContext()
    {
    }

    public AsasContext(DbContextOptions<AsasContext> options, ICompanyService companyService)
            : base(options)
    {
        _currentComId = companyService.GetCurrentCompanyId();
    }

    public virtual DbSet<AddEmp> AddEmps { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<InternalMessage> InternalMessages { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<MessageAttachment> MessageAttachments { get; set; }

    public virtual DbSet<MessageRecipient> MessageRecipients { get; set; }

    public virtual DbSet<Models.Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HP\\SQLEXPRESS;Initial Catalog=SKY;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasQueryFilter(d => d.ComId == _currentComId);
        modelBuilder.Entity<AddEmp>().HasQueryFilter(e => e.ComId == _currentComId);
        modelBuilder.Entity<Employee>().HasQueryFilter(u => u.ComId == _currentComId);
        modelBuilder.Entity<Models.Task>().HasQueryFilter(t => t.ComId == _currentComId);
        modelBuilder.Entity<InternalMessage>().HasQueryFilter(m => m.ComId == _currentComId);

        modelBuilder.Entity<AddEmp>(entity =>
        {
            entity.HasKey(e => e.EmpAutoId).HasName("PK__AddEmp__93CB913E03011B79");

            entity.ToTable("AddEmp");

            entity.HasIndex(e => e.ComId, "IX_AddEmp_Com_id");

            entity.HasIndex(e => new { e.EmpId, e.ComId }, "UQ_Emp_id_Com_id").IsUnique();

            entity.Property(e => e.EmpAutoId).HasColumnName("Emp_auto_id");
            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.DepAutoId).HasColumnName("Dep_auto_id");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Emp_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Job_title");
            entity.Property(e => e.Role).HasColumnType("decimal(5, 0)");

            entity.HasOne(d => d.Com).WithMany(p => p.AddEmps)
                .HasForeignKey(d => d.ComId)
                .HasConstraintName("FK_Company_to_AddEmp");

            entity.HasOne(d => d.DepAuto).WithMany(p => p.AddEmps)
                .HasForeignKey(d => d.DepAutoId)
                .HasConstraintName("FK_Departments_to_AddEmp");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.ComId).HasName("PK__Company__D85724E46AA3CC58");

            entity.ToTable("Company");

            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.DateOfEnd)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_end");
            entity.Property(e => e.Price).HasColumnType("decimal(4, 0)");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepAutoId).HasName("PK__Departme__2A4A966C5537A3B0");

            entity.HasIndex(e => e.ComId, "IX_Departments_Com_id");

            entity.HasIndex(e => new { e.DepName, e.ComId }, "UQ_Dep_name_Com_id").IsUnique();

            entity.Property(e => e.DepAutoId).HasColumnName("Dep_auto_id");
            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.DepName)
                .HasMaxLength(100)
                .HasColumnName("Dep_name");
            entity.Property(e => e.SupervisorId).HasColumnName("Supervisor_id");

            entity.HasOne(d => d.Com).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ComId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_to_Departments");

              

        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__781228D9C093690E");

            entity.HasIndex(e => e.ComId, "IX_Employees_Com_id");

            entity.HasIndex(e => new { e.Email, e.ComId, e.PhoneNumber }, "UQ_Email_Com_id_Phone_Number").IsUnique();

            entity.HasIndex(e => e.EmpAutoId, "UQ_Emp_auto_id").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmpAutoId).HasColumnName("Emp_auto_id");
            entity.Property(e => e.HashingPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Hashing_Password");
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.Com).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ComId)
                .HasConstraintName("FK_Company_to_Employees");

            entity.HasOne(d => d.EmpAuto).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.EmpAutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AddEmp_to_Employees");
        });

        modelBuilder.Entity<InternalMessage>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("PK__Internal__26A24E62BD3F6CF1");

            entity.HasIndex(e => e.ComId, "IX_InternalMessages_Com_id");

            entity.Property(e => e.MsgId).HasColumnName("Msg_id");
            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentMsgId).HasColumnName("Parent_msg_id");
            entity.Property(e => e.SenderId).HasColumnName("Sender_id");
            entity.Property(e => e.Subject).HasMaxLength(255);

            entity.HasOne(d => d.Com).WithMany(p => p.InternalMessages)
                .HasForeignKey(d => d.ComId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_to_InternalMessages");

            entity.HasOne(d => d.ParentMsg).WithMany(p => p.InverseParentMsg)
                .HasForeignKey(d => d.ParentMsgId)
                .HasConstraintName("FK_InternalMessages_to_InternalMessages");

            entity.HasOne(d => d.Sender).WithMany(p => p.InternalMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AddEmp_to_InternalMessages");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Issue__3214EC0762229ECA");

            entity.ToTable("Issue");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MessageAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__MessageA__97E3B2DF6844E878");

            entity.Property(e => e.AttachmentId).HasColumnName("Attachment_id");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FileType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MsgId).HasColumnName("Msg_id");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Msg).WithMany(p => p.MessageAttachments)
                .HasForeignKey(d => d.MsgId)
                .HasConstraintName("FK_InternalMessages_to_MessageAttachments");
        });

        modelBuilder.Entity<MessageRecipient>(entity =>
        {
            entity.HasKey(e => new { e.MsgId, e.RecipientId }).HasName("PK__MessageR__CECB78E311CE62FB");

            entity.Property(e => e.MsgId).HasColumnName("Msg_id");
            entity.Property(e => e.RecipientId).HasColumnName("Recipient_id");
            entity.Property(e => e.ReadAt).HasColumnType("datetime");

            entity.HasOne(d => d.Msg).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.MsgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternalMessages_to_MessageRecipients");

            entity.HasOne(d => d.Recipient).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AddEmp_to_MessageRecipients");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__716846B55821CE6E");

            entity.HasIndex(e => e.ComId, "IX_Tasks_Com_id");

            entity.Property(e => e.TaskId).HasColumnName("Task_id");
            entity.Property(e => e.ComId).HasColumnName("Com_id");
            entity.Property(e => e.CompletionDate).HasColumnName("Completion_date");
            entity.Property(e => e.DateOfEnd).HasColumnName("Date_of_end");
            entity.Property(e => e.DateOfStart).HasColumnName("Date_of_start");
            entity.Property(e => e.DepAutoId).HasColumnName("Dep_auto_id");
            entity.Property(e => e.EmpAutoId).HasColumnName("Emp_auto_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.SupervisorId).HasColumnName("Supervisor_id");
            entity.Property(e => e.TaskDetails).HasColumnName("Task_details");
            entity.Property(e => e.TaskName)
                .HasMaxLength(50)
                .HasColumnName("Task_name");
            entity.Property(e => e.TaskPriority)
                .HasColumnType("decimal(3, 0)")
                .HasColumnName("Task_priority");

            entity.HasOne(d => d.Com).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ComId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_to_Tasks");

            entity.HasOne(d => d.DepAuto).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.DepAutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Departments_to_Tasks");

            entity.HasOne(d => d.EmpAuto).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.EmpAutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AddEmp_to_Tasks");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
