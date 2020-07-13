
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/12/2020 20:34:23
-- Generated from EDMX file: C:\Users\darwi\source\repos\darwin09afp\SIREON\SIREON\SIREONModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_Aplicativo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_ListaNegra_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListaNegra] DROP CONSTRAINT [FK_ListaNegra_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_R_Salas-Usuarios_Salas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_Salas-Usuarios] DROP CONSTRAINT [FK_R_Salas-Usuarios_Salas];
GO
IF OBJECT_ID(N'[dbo].[FK_R_Salas-Usuarios_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_Salas-Usuarios] DROP CONSTRAINT [FK_R_Salas-Usuarios_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_R_Usuarios-Roles_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_Usuarios-Roles] DROP CONSTRAINT [FK_R_Usuarios-Roles_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_R_Usuarios-Roles_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_Usuarios-Roles] DROP CONSTRAINT [FK_R_Usuarios-Roles_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Reservaciones_Cubiculos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservaciones] DROP CONSTRAINT [FK_Reservaciones_Cubiculos];
GO
IF OBJECT_ID(N'[dbo].[FK_Reservaciones_Salas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservaciones] DROP CONSTRAINT [FK_Reservaciones_Salas];
GO
IF OBJECT_ID(N'[dbo].[FK_Reservaciones_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservaciones] DROP CONSTRAINT [FK_Reservaciones_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Seguridad_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Seguridad] DROP CONSTRAINT [FK_Seguridad_Usuarios];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Cubiculos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cubiculos];
GO
IF OBJECT_ID(N'[dbo].[ListaNegra]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListaNegra];
GO
IF OBJECT_ID(N'[dbo].[Parametros]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parametros];
GO
IF OBJECT_ID(N'[dbo].[R_Salas-Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_Salas-Usuarios];
GO
IF OBJECT_ID(N'[dbo].[R_Usuarios-Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_Usuarios-Roles];
GO
IF OBJECT_ID(N'[dbo].[Reservaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservaciones];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Salas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Salas];
GO
IF OBJECT_ID(N'[dbo].[Seguridad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Seguridad];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cubiculos'
CREATE TABLE [dbo].[Cubiculos] (
    [ID_Cubiculo] int IDENTITY(1,1) NOT NULL,
    [Descripcion] varchar(10)  NOT NULL,
    [Capacidad] decimal(1,0)  NOT NULL
);
GO

-- Creating table 'ListaNegras'
CREATE TABLE [dbo].[ListaNegras] (
    [ID_ListaN] int IDENTITY(1,1) NOT NULL,
    [ID_Usuario] int  NOT NULL,
    [Descripcion] varchar(30)  NOT NULL
);
GO

-- Creating table 'R_Salas_Usuarios'
CREATE TABLE [dbo].[R_Salas_Usuarios] (
    [ID_RSalas] int IDENTITY(1,1) NOT NULL,
    [ID_Sala] int  NOT NULL,
    [ID_Usuario] int  NOT NULL
);
GO

-- Creating table 'Reservaciones'
CREATE TABLE [dbo].[Reservaciones] (
    [ID_Reservacion] int IDENTITY(1,1) NOT NULL,
    [ID_Usuario] int  NOT NULL,
    [ID_Sala] int  NOT NULL,
    [ID_Empleado] varchar(9)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [ID_Cubiculo] int  NOT NULL,
    [FechaSolicitada] datetime  NOT NULL,
    [HSolicitada] time  NOT NULL,
    [HEntrada] time  NOT NULL,
    [HSalida] time  NOT NULL,
    [Estatus] char(10)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Rol] char(10)  NULL,
    [ID_Rol] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Salas'
CREATE TABLE [dbo].[Salas] (
    [ID_Sala] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [ID_Usuario] int IDENTITY(1,1) NOT NULL,
    [Usuario1] varchar(9)  NOT NULL,
    [Contrasena] varchar(16)  NOT NULL
);
GO

-- Creating table 'R_Usuarios_Roles'
CREATE TABLE [dbo].[R_Usuarios_Roles] (
    [ID_RolUsuario] int IDENTITY(1,1) NOT NULL,
    [ID_Usuario] int  NOT NULL,
    [ID_Rol] int  NOT NULL
);
GO

-- Creating table 'Seguridads'
CREATE TABLE [dbo].[Seguridads] (
    [ID_Seguridad] int IDENTITY(1,1) NOT NULL,
    [ID_Usuario] int  NULL,
    [pregunta1] varchar(50)  NOT NULL,
    [respuesta1] varchar(50)  NOT NULL,
    [pregunta2] varchar(50)  NOT NULL,
    [respuesta2] varchar(50)  NOT NULL,
    [status] bit  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [UserPhoto] varbinary(max)  NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Parametros'
CREATE TABLE [dbo].[Parametros] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(30)  NOT NULL,
    [Valor] varchar(15)  NOT NULL,
    [NombreCorto] varchar(10)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Cubiculo] in table 'Cubiculos'
ALTER TABLE [dbo].[Cubiculos]
ADD CONSTRAINT [PK_Cubiculos]
    PRIMARY KEY CLUSTERED ([ID_Cubiculo] ASC);
GO

-- Creating primary key on [ID_ListaN] in table 'ListaNegras'
ALTER TABLE [dbo].[ListaNegras]
ADD CONSTRAINT [PK_ListaNegras]
    PRIMARY KEY CLUSTERED ([ID_ListaN] ASC);
GO

-- Creating primary key on [ID_RSalas] in table 'R_Salas_Usuarios'
ALTER TABLE [dbo].[R_Salas_Usuarios]
ADD CONSTRAINT [PK_R_Salas_Usuarios]
    PRIMARY KEY CLUSTERED ([ID_RSalas] ASC);
GO

-- Creating primary key on [ID_Reservacion] in table 'Reservaciones'
ALTER TABLE [dbo].[Reservaciones]
ADD CONSTRAINT [PK_Reservaciones]
    PRIMARY KEY CLUSTERED ([ID_Reservacion] ASC);
GO

-- Creating primary key on [ID_Rol] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([ID_Rol] ASC);
GO

-- Creating primary key on [ID_Sala] in table 'Salas'
ALTER TABLE [dbo].[Salas]
ADD CONSTRAINT [PK_Salas]
    PRIMARY KEY CLUSTERED ([ID_Sala] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID_Usuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([ID_Usuario] ASC);
GO

-- Creating primary key on [ID_RolUsuario] in table 'R_Usuarios_Roles'
ALTER TABLE [dbo].[R_Usuarios_Roles]
ADD CONSTRAINT [PK_R_Usuarios_Roles]
    PRIMARY KEY CLUSTERED ([ID_RolUsuario] ASC);
GO

-- Creating primary key on [ID_Seguridad] in table 'Seguridads'
ALTER TABLE [dbo].[Seguridads]
ADD CONSTRAINT [PK_Seguridads]
    PRIMARY KEY CLUSTERED ([ID_Seguridad] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [ID] in table 'Parametros'
ALTER TABLE [dbo].[Parametros]
ADD CONSTRAINT [PK_Parametros]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Cubiculo] in table 'Reservaciones'
ALTER TABLE [dbo].[Reservaciones]
ADD CONSTRAINT [FK_Reservaciones_Cubiculos]
    FOREIGN KEY ([ID_Cubiculo])
    REFERENCES [dbo].[Cubiculos]
        ([ID_Cubiculo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reservaciones_Cubiculos'
CREATE INDEX [IX_FK_Reservaciones_Cubiculos]
ON [dbo].[Reservaciones]
    ([ID_Cubiculo]);
GO

-- Creating foreign key on [ID_Usuario] in table 'ListaNegras'
ALTER TABLE [dbo].[ListaNegras]
ADD CONSTRAINT [FK_ListaNegra_Usuarios]
    FOREIGN KEY ([ID_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([ID_Usuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListaNegra_Usuarios'
CREATE INDEX [IX_FK_ListaNegra_Usuarios]
ON [dbo].[ListaNegras]
    ([ID_Usuario]);
GO

-- Creating foreign key on [ID_Sala] in table 'R_Salas_Usuarios'
ALTER TABLE [dbo].[R_Salas_Usuarios]
ADD CONSTRAINT [FK_R_Salas_Usuarios_Salas]
    FOREIGN KEY ([ID_Sala])
    REFERENCES [dbo].[Salas]
        ([ID_Sala])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_Salas_Usuarios_Salas'
CREATE INDEX [IX_FK_R_Salas_Usuarios_Salas]
ON [dbo].[R_Salas_Usuarios]
    ([ID_Sala]);
GO

-- Creating foreign key on [ID_Usuario] in table 'R_Salas_Usuarios'
ALTER TABLE [dbo].[R_Salas_Usuarios]
ADD CONSTRAINT [FK_R_Salas_Usuarios_Usuarios]
    FOREIGN KEY ([ID_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([ID_Usuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_Salas_Usuarios_Usuarios'
CREATE INDEX [IX_FK_R_Salas_Usuarios_Usuarios]
ON [dbo].[R_Salas_Usuarios]
    ([ID_Usuario]);
GO

-- Creating foreign key on [ID_Sala] in table 'Reservaciones'
ALTER TABLE [dbo].[Reservaciones]
ADD CONSTRAINT [FK_Reservaciones_Salas]
    FOREIGN KEY ([ID_Sala])
    REFERENCES [dbo].[Salas]
        ([ID_Sala])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reservaciones_Salas'
CREATE INDEX [IX_FK_Reservaciones_Salas]
ON [dbo].[Reservaciones]
    ([ID_Sala]);
GO

-- Creating foreign key on [ID_Usuario] in table 'Reservaciones'
ALTER TABLE [dbo].[Reservaciones]
ADD CONSTRAINT [FK_Reservaciones_Usuarios]
    FOREIGN KEY ([ID_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([ID_Usuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reservaciones_Usuarios'
CREATE INDEX [IX_FK_Reservaciones_Usuarios]
ON [dbo].[Reservaciones]
    ([ID_Usuario]);
GO

-- Creating foreign key on [ID_Rol] in table 'R_Usuarios_Roles'
ALTER TABLE [dbo].[R_Usuarios_Roles]
ADD CONSTRAINT [FK_R_Usuarios_Roles_Roles]
    FOREIGN KEY ([ID_Rol])
    REFERENCES [dbo].[Roles]
        ([ID_Rol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_Usuarios_Roles_Roles'
CREATE INDEX [IX_FK_R_Usuarios_Roles_Roles]
ON [dbo].[R_Usuarios_Roles]
    ([ID_Rol]);
GO

-- Creating foreign key on [ID_Usuario] in table 'R_Usuarios_Roles'
ALTER TABLE [dbo].[R_Usuarios_Roles]
ADD CONSTRAINT [FK_R_Usuarios_Roles_Usuarios]
    FOREIGN KEY ([ID_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([ID_Usuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_Usuarios_Roles_Usuarios'
CREATE INDEX [IX_FK_R_Usuarios_Roles_Usuarios]
ON [dbo].[R_Usuarios_Roles]
    ([ID_Usuario]);
GO

-- Creating foreign key on [ID_Seguridad] in table 'Seguridads'
ALTER TABLE [dbo].[Seguridads]
ADD CONSTRAINT [FK_Seguridad_Usuarios]
    FOREIGN KEY ([ID_Seguridad])
    REFERENCES [dbo].[Usuarios]
        ([ID_Usuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------