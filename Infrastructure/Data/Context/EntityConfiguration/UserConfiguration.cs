using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

/// <summary>
/// Конфигурация пользователя
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Guid).HasName("user_pk");

        entity.ToTable("user");
        
        entity.HasIndex(e => e.Login, "user_un").IsUnique();

        entity.Property(e => e.Guid)
            .ValueGeneratedNever()
            .HasComment("Уникальный идентификатор пользователя")
            .HasColumnName("guid");
        entity.Property(e => e.Admin)
            .HasComment("Указание - является ли пользователь админом")
            .HasColumnName("admin");
        entity.Property(e => e.Birthday)
            .HasComment("Поле даты рождения может быть Null")
            .HasColumnName("birthday");
        entity.Property(e => e.CreatedBy)
            .HasComment("Логин Пользователя, от имени которого этот пользователь создан")
            .HasColumnType("character varying")
            .HasColumnName("createdby");
        entity.Property(e => e.CreatedOn)
            .HasComment("Дата создания пользователя")
            .HasColumnName("createdon");
        entity.Property(e => e.Gender)
            .HasComment("Пол 0 - женщина, 1 - мужчина, 2 - неизвестно")
            .HasColumnName("gender");
        entity.Property(e => e.Login)
            .HasComment("Уникальный Логин (запрещены все символы кроме латинских букв и цифр)")
            .HasColumnType("character varying")
            .HasColumnName("login");
        entity.Property(e => e.ModifiedBy)
            .HasComment("Логин Пользователя, от имени которого этот пользователь изменён")
            .HasColumnType("character varying")
            .HasColumnName("modifiedby");
        entity.Property(e => e.ModifiedOn)
            .HasComment("Дата изменения пользователя")
            .HasColumnName("modifiedon");
        entity.Property(e => e.Name)
            .HasComment("Имя (запрещены все символы кроме латинских и русских букв)")
            .HasColumnType("character varying")
            .HasColumnName("name");
        entity.Property(e => e.Password)
            .HasComment("Пароль(запрещены все символы кроме латинских букв и цифр)")
            .HasColumnType("character varying")
            .HasColumnName("password");
        entity.Property(e => e.RevokedBy)
            .HasComment("Логин Пользователя, от имени которого этот пользователь удалён")
            .HasColumnType("character varying")
            .HasColumnName("revokedby");
        entity.Property(e => e.RevokedOn)
            .HasComment("Дата удаления пользователя")
            .HasColumnName("revokedon");     
    }
}