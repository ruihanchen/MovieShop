IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617141709_IntitialMigration')
BEGIN
    CREATE TABLE [Genres] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617141709_IntitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617141709_IntitialMigration', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617142028_UpdatingGenreTable')
BEGIN
    ALTER TABLE [Genres] DROP CONSTRAINT [PK_Genres];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617142028_UpdatingGenreTable')
BEGIN
    EXEC sp_rename N'[Genres]', N'Genre';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617142028_UpdatingGenreTable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genre]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Genre] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Genre] ALTER COLUMN [Name] nvarchar(64) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617142028_UpdatingGenreTable')
BEGIN
    ALTER TABLE [Genre] ADD CONSTRAINT [PK_Genre] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617142028_UpdatingGenreTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617142028_UpdatingGenreTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617163912_CreatingMovieTable')
BEGIN
    CREATE TABLE [Movie] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(256) NULL,
        [Overview] nvarchar(max) NULL,
        [Tagline] nvarchar(512) NULL,
        [Budget] decimal(18,4) NULL DEFAULT 9.9,
        [Revenue] decimal(18,4) NULL DEFAULT 9.9,
        [ImdbUrl] nvarchar(2084) NULL,
        [TmdbUrl] nvarchar(2084) NULL,
        [PosterUrl] nvarchar(2084) NULL,
        [BackdropUrl] nvarchar(2084) NULL,
        [OriginalLanguage] nvarchar(64) NULL,
        [ReleaseDate] datetime2 NULL,
        [RunTime] int NULL,
        [Price] decimal(5,2) NULL DEFAULT 9.9,
        [CreatedDate] datetime2 NULL DEFAULT (getdate()),
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617163912_CreatingMovieTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617163912_CreatingMovieTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617165413_CreatingMovieGenreTable')
BEGIN
    CREATE TABLE [MovieGenre] (
        [MovieId] int NOT NULL,
        [GenreId] int NOT NULL,
        CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([MovieId], [GenreId]),
        CONSTRAINT [FK_MovieGenre_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieGenre_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617165413_CreatingMovieGenreTable')
BEGIN
    CREATE INDEX [IX_MovieGenre_GenreId] ON [MovieGenre] ([GenreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617165413_CreatingMovieGenreTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617165413_CreatingMovieGenreTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617180557_CreatingCastTable')
BEGIN
    CREATE TABLE [Cast] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(128) NULL,
        [Gender] nvarchar(max) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        [Profilepath] nvarchar(max) NULL,
        CONSTRAINT [PK_Cast] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617180557_CreatingCastTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617180557_CreatingCastTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618003536_CreatingMovieCastTable')
BEGIN
    CREATE TABLE [MovieCast] (
        [MovieId] int NOT NULL,
        [CastId] int NOT NULL,
        [Character] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_MovieCast] PRIMARY KEY ([MovieId], [CastId], [Character]),
        CONSTRAINT [FK_MovieCast_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieCast_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618003536_CreatingMovieCastTable')
BEGIN
    CREATE INDEX [IX_MovieCast_CastId] ON [MovieCast] ([CastId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618003536_CreatingMovieCastTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618003536_CreatingMovieCastTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618012137_CreatingRoleTable')
BEGIN
    CREATE TABLE [Role] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618012137_CreatingRoleTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618012137_CreatingRoleTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618013533_CreatingUserRoleTable')
BEGIN
    CREATE TABLE [UserRole] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618013533_CreatingUserRoleTable')
BEGIN
    CREATE INDEX [IX_UserRole_RoleId] ON [UserRole] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618013533_CreatingUserRoleTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618013533_CreatingUserRoleTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618014522_CreatingReviewTable')
BEGIN
    CREATE TABLE [Review] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [Rating] decimal(3,2) NOT NULL DEFAULT 9.9,
        [ReviewText] nvarchar(max) NULL,
        CONSTRAINT [PK_Review] PRIMARY KEY ([MovieId], [UserId]),
        CONSTRAINT [FK_Review_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618014522_CreatingReviewTable')
BEGIN
    CREATE INDEX [IX_Review_UserId] ON [Review] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618014522_CreatingReviewTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618014522_CreatingReviewTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618015250_CreatingFaveriteTable')
BEGIN
    CREATE TABLE [Favorite] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Favorite] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Favorite_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorite_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618015250_CreatingFaveriteTable')
BEGIN
    CREATE INDEX [IX_Favorite_MovieId] ON [Favorite] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618015250_CreatingFaveriteTable')
BEGIN
    CREATE INDEX [IX_Favorite_UserId] ON [Favorite] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618015250_CreatingFaveriteTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618015250_CreatingFaveriteTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619134051_CreatingTrailerTable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cast]') AND [c].[name] = N'Profilepath');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Cast] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Cast] ALTER COLUMN [Profilepath] nvarchar(2084) NOT NULL;
    ALTER TABLE [Cast] ADD DEFAULT N'' FOR [Profilepath];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619134051_CreatingTrailerTable')
BEGIN
    CREATE TABLE [Trailer] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [TrailerUrl] nvarchar(2084) NULL,
        [Name] nvarchar(2084) NULL,
        CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Trailer_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619134051_CreatingTrailerTable')
BEGIN
    CREATE INDEX [IX_Trailer_MovieId] ON [Trailer] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619134051_CreatingTrailerTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220619134051_CreatingTrailerTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619142945_CreatingPurchaseTable')
BEGIN
    CREATE TABLE [Purchase] (
        [Id] int NOT NULL IDENTITY,
        [UerId] int NOT NULL,
        [PurchaseNumber] uniqueidentifier NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [PurchaseDateTime] datetime2 NOT NULL DEFAULT (getdate()),
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchase_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619142945_CreatingPurchaseTable')
BEGIN
    CREATE INDEX [IX_Purchase_MovieId] ON [Purchase] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619142945_CreatingPurchaseTable')
BEGIN
    CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619142945_CreatingPurchaseTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220619142945_CreatingPurchaseTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619143834_UpdatingTable')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'UerId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Purchase] ALTER COLUMN [UerId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619143834_UpdatingTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220619143834_UpdatingTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620020744_newUpdatingTable')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Role]') AND [c].[name] = N'Name');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Role] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Role] ALTER COLUMN [Name] nvarchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620020744_newUpdatingTable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'UerId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Purchase] ALTER COLUMN [UerId] int NOT NULL;
    ALTER TABLE [Purchase] ADD DEFAULT 0 FOR [UerId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620020744_newUpdatingTable')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genre]') AND [c].[name] = N'Name');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Genre] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Genre] ALTER COLUMN [Name] nvarchar(64) NOT NULL;
    ALTER TABLE [Genre] ADD DEFAULT N'' FOR [Name];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620020744_newUpdatingTable')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cast]') AND [c].[name] = N'Profilepath');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Cast] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Cast] ALTER COLUMN [Profilepath] nvarchar(2084) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620020744_newUpdatingTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220620020744_newUpdatingTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620151451_CreatingCrewTable')
BEGIN
    CREATE TABLE [Crew] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(128) NULL,
        [Gender] nvarchar(max) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        [ProfilePath] nvarchar(2084) NULL,
        CONSTRAINT [PK_Crew] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620151451_CreatingCrewTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220620151451_CreatingCrewTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620153844_CreatingMovieCrewTable')
BEGIN
    CREATE TABLE [MovieCrew] (
        [MovieId] int NOT NULL,
        [CrewId] int NOT NULL,
        [Department] nvarchar(128) NOT NULL,
        [Job] nvarchar(128) NOT NULL,
        CONSTRAINT [PK_MovieCrew] PRIMARY KEY ([MovieId], [CrewId]),
        CONSTRAINT [FK_MovieCrew_Crew_CrewId] FOREIGN KEY ([CrewId]) REFERENCES [Crew] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieCrew_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620153844_CreatingMovieCrewTable')
BEGIN
    CREATE INDEX [IX_MovieCrew_CrewId] ON [MovieCrew] ([CrewId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620153844_CreatingMovieCrewTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220620153844_CreatingMovieCrewTable', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    ALTER TABLE [Favorite] DROP CONSTRAINT [FK_Favorite_User_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    ALTER TABLE [Purchase] DROP CONSTRAINT [FK_Purchase_User_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    ALTER TABLE [Review] DROP CONSTRAINT [FK_Review_User_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    ALTER TABLE [UserRole] DROP CONSTRAINT [FK_UserRole_User_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    DROP TABLE [User];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    DROP INDEX [IX_Review_UserId] ON [Review];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    DROP INDEX [IX_Purchase_UserId] ON [Purchase];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    DROP INDEX [IX_Favorite_UserId] ON [Favorite];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'UserId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Purchase] DROP COLUMN [UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621054304_updatingtables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621054304_updatingtables', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    ALTER TABLE [Purchase] ADD [UserId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    CREATE TABLE [User] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(128) NULL,
        [LastName] nvarchar(128) NULL,
        [DateOfBirth] datetime2 NULL DEFAULT (getdate()),
        [Email] nvarchar(256) NULL,
        [HashedPassword] nvarchar(1024) NULL,
        [Salt] nvarchar(1024) NULL,
        [PhoneNumber] nvarchar(16) NULL,
        [TwoFactorEnabled] bit NULL,
        [lockoutEndDate] datetime2 NULL DEFAULT (getdate()),
        [LastLoginDateTime] datetime2 NULL DEFAULT (getdate()),
        [IsLocked] bit NULL,
        [AccessFailedCount] int NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    CREATE INDEX [IX_Review_UserId] ON [Review] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    CREATE INDEX [IX_Favorite_UserId] ON [Favorite] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    ALTER TABLE [Favorite] ADD CONSTRAINT [FK_Favorite_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    ALTER TABLE [Purchase] ADD CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    ALTER TABLE [UserRole] ADD CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621141018_CreatingUserTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621141018_CreatingUserTable', N'6.0.6');
END;
GO

COMMIT;
GO

