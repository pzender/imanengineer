IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Channel] (
    [id] bigint NOT NULL,
    [name] varchar(200) NOT NULL,
    [icon_url] varchar(200) NULL,
    CONSTRAINT [PK_Channel] PRIMARY KEY ([id])
);

GO

CREATE TABLE [FeatureTypes] (
    [id] bigint NOT NULL,
    [type_name] varchar(200) NOT NULL,
    CONSTRAINT [PK_FeatureTypes] PRIMARY KEY ([id])
);

GO

CREATE TABLE [GuideUpdate] (
    [id] bigint NOT NULL,
    [source] varchar(200) NOT NULL,
    [posted] datetime NOT NULL,
    CONSTRAINT [PK_GuideUpdate] PRIMARY KEY ([id])
);

GO

CREATE TABLE [Series] (
    [id] bigint NOT NULL,
    [title] varchar(200) NOT NULL,
    CONSTRAINT [PK_Series] PRIMARY KEY ([id])
);

GO

CREATE TABLE [User] (
    [login] varchar(200) NOT NULL,
    [weight_actor] float NOT NULL DEFAULT (((0.1))),
    [weight_category] float NOT NULL DEFAULT (((0.3))),
    [weight_country] float NOT NULL DEFAULT (((0.1))),
    [weight_year] float NOT NULL DEFAULT (((0.1))),
    [weight_keyword] float NOT NULL DEFAULT (((0.3))),
    [weight_director] float NOT NULL DEFAULT (((0.1))),
    CONSTRAINT [PK_User] PRIMARY KEY ([login])
);

GO

CREATE TABLE [Feature] (
    [id] bigint NOT NULL,
    [type] bigint NOT NULL,
    [value] varchar(200) NOT NULL,
    CONSTRAINT [PK_Feature] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Feature_FeatureTypes_type] FOREIGN KEY ([type]) REFERENCES [FeatureTypes] ([id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Programme] (
    [id] bigint NOT NULL,
    [title] varchar(400) NOT NULL,
    [icon_url] varchar(200) NULL,
    [seq_number] varchar(200) NULL,
    [series_id] bigint NULL,
    CONSTRAINT [PK_Programme] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Programme_Series_series_id] FOREIGN KEY ([series_id]) REFERENCES [Series] ([id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Description] (
    [id] bigint NOT NULL,
    [id_programme] bigint NOT NULL,
    [content] text NOT NULL,
    [GuideUpdateId] bigint NULL,
    CONSTRAINT [PK_Description] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Description_GuideUpdate_GuideUpdateId] FOREIGN KEY ([GuideUpdateId]) REFERENCES [GuideUpdate] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Description_Programme_id_programme] FOREIGN KEY ([id_programme]) REFERENCES [Programme] ([id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Emission] (
    [id] bigint NOT NULL,
    [start] datetime NOT NULL,
    [stop] datetime NOT NULL,
    [programme_id] bigint NOT NULL,
    [channel_id] bigint NOT NULL,
    CONSTRAINT [PK_Emission] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Emission_Channel_channel_id] FOREIGN KEY ([channel_id]) REFERENCES [Channel] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Emission_Programme_programme_id] FOREIGN KEY ([programme_id]) REFERENCES [Programme] ([id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [FeatureExample] (
    [feature_id] bigint NOT NULL,
    [programme_id] bigint NOT NULL,
    CONSTRAINT [PK_FeatureExample] PRIMARY KEY ([feature_id], [programme_id]),
    CONSTRAINT [FK_FeatureExample_Feature_feature_id] FOREIGN KEY ([feature_id]) REFERENCES [Feature] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FeatureExample_Programme_programme_id] FOREIGN KEY ([programme_id]) REFERENCES [Programme] ([id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Rating] (
    [user_login] varchar(200) NOT NULL,
    [programme_id] bigint NOT NULL,
    [rating_value] bigint NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY ([user_login], [programme_id]),
    CONSTRAINT [FK_Rating_Programme_programme_id] FOREIGN KEY ([programme_id]) REFERENCES [Programme] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Rating_User_user_login] FOREIGN KEY ([user_login]) REFERENCES [User] ([login]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_Channel_name] ON [Channel] ([name]);

GO

CREATE INDEX [IX_Description_GuideUpdateId] ON [Description] ([GuideUpdateId]);

GO

CREATE INDEX [IX_Description_id_programme] ON [Description] ([id_programme]);

GO

CREATE INDEX [IX_Emission_channel_id] ON [Emission] ([channel_id]);

GO

CREATE INDEX [IX_Emission_programme_id] ON [Emission] ([programme_id]);

GO

CREATE UNIQUE INDEX [IX_Emission_start_stop_programme_id_channel_id] ON [Emission] ([start], [stop], [programme_id], [channel_id]);

GO

CREATE UNIQUE INDEX [IND_feature_id] ON [Feature] ([id]);

GO

CREATE UNIQUE INDEX [IX_Feature_type_value] ON [Feature] ([type], [value]);

GO

CREATE INDEX [IX_FeatureExample_programme_id] ON [FeatureExample] ([programme_id]);

GO

CREATE UNIQUE INDEX [IX_GuideUpdate_posted] ON [GuideUpdate] ([posted]);

GO

CREATE UNIQUE INDEX [IND_programme_id] ON [Programme] ([id]);

GO

CREATE INDEX [IX_Programme_series_id] ON [Programme] ([series_id]);

GO

CREATE UNIQUE INDEX [IX_Programme_title] ON [Programme] ([title]);

GO

CREATE INDEX [IX_Rating_programme_id] ON [Rating] ([programme_id]);

GO

CREATE UNIQUE INDEX [IX_Series_title] ON [Series] ([title]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190725200304_InitialCreate', N'3.0.0-preview7.19362.6');

GO


