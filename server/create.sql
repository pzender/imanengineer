CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Channel` (
    `id` bigint NOT NULL,
    `name` STRING NOT NULL,
    `icon_url` STRING NULL,
    CONSTRAINT `PK_Channel` PRIMARY KEY (`id`)
);

CREATE TABLE `FeatureTypes` (
    `id` bigint NOT NULL,
    `type_name` STRING NOT NULL,
    CONSTRAINT `PK_FeatureTypes` PRIMARY KEY (`id`)
);

CREATE TABLE `GuideUpdate` (
    `id` bigint NOT NULL,
    `source` STRING NOT NULL,
    `posted` DATETIME NOT NULL,
    CONSTRAINT `PK_GuideUpdate` PRIMARY KEY (`id`)
);

CREATE TABLE `Series` (
    `id` bigint NOT NULL,
    `title` STRING NOT NULL,
    CONSTRAINT `PK_Series` PRIMARY KEY (`id`)
);

CREATE TABLE `User` (
    `login` STRING NOT NULL,
    `weight_actor` DOUBLE NOT NULL DEFAULT 0.1,
    `weight_category` DOUBLE NOT NULL DEFAULT 0.3,
    `weight_country` DOUBLE NOT NULL DEFAULT 0.1,
    `weight_year` DOUBLE NOT NULL DEFAULT 0.1,
    `weight_keyword` DOUBLE NOT NULL DEFAULT 0.3,
    `weight_director` DOUBLE NOT NULL DEFAULT 0.1,
    CONSTRAINT `PK_User` PRIMARY KEY (`login`)
);

CREATE TABLE `Feature` (
    `id` bigint NOT NULL,
    `type` bigint NOT NULL,
    `value` STRING NOT NULL,
    CONSTRAINT `PK_Feature` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Feature_FeatureTypes_type` FOREIGN KEY (`type`) REFERENCES `FeatureTypes` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `Programme` (
    `id` bigint NOT NULL,
    `title` STRING NOT NULL,
    `icon_url` STRING NULL,
    `seq_number` STRING NULL,
    `series_id` bigint NULL,
    CONSTRAINT `PK_Programme` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Programme_Series_series_id` FOREIGN KEY (`series_id`) REFERENCES `Series` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `Description` (
    `id` bigint NOT NULL,
    `id_programme` bigint NOT NULL,
    `source` bigint NULL,
    `content` varchar(255) NOT NULL,
    CONSTRAINT `PK_Description` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Description_Programme_id_programme` FOREIGN KEY (`id_programme`) REFERENCES `Programme` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Description_GuideUpdate_source` FOREIGN KEY (`source`) REFERENCES `GuideUpdate` (`id`) ON DELETE SET NULL
);

CREATE TABLE `Emission` (
    `id` bigint NOT NULL,
    `start` DATETIME NOT NULL,
    `stop` DATETIME NOT NULL,
    `programme_id` bigint NOT NULL,
    `channel_id` bigint NOT NULL,
    CONSTRAINT `PK_Emission` PRIMARY KEY (`id`),
    CONSTRAINT `FK_Emission_Channel_channel_id` FOREIGN KEY (`channel_id`) REFERENCES `Channel` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Emission_Programme_programme_id` FOREIGN KEY (`programme_id`) REFERENCES `Programme` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `FeatureExample` (
    `feature_id` bigint NOT NULL,
    `programme_id` bigint NOT NULL,
    CONSTRAINT `PK_FeatureExample` PRIMARY KEY (`feature_id`, `programme_id`),
    CONSTRAINT `FK_FeatureExample_Feature_feature_id` FOREIGN KEY (`feature_id`) REFERENCES `Feature` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_FeatureExample_Programme_programme_id` FOREIGN KEY (`programme_id`) REFERENCES `Programme` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `Rating` (
    `user_login` STRING NOT NULL,
    `programme_id` bigint NOT NULL,
    `rating_value` bigint NOT NULL,
    CONSTRAINT `PK_Rating` PRIMARY KEY (`user_login`, `programme_id`),
    CONSTRAINT `FK_Rating_Programme_programme_id` FOREIGN KEY (`programme_id`) REFERENCES `Programme` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Rating_User_user_login` FOREIGN KEY (`user_login`) REFERENCES `User` (`login`) ON DELETE CASCADE
);

CREATE UNIQUE INDEX `IX_Channel_name` ON `Channel` (`name`);

CREATE UNIQUE INDEX `IX_Description_content` ON `Description` (`content`);

CREATE INDEX `IX_Description_source` ON `Description` (`source`);

CREATE UNIQUE INDEX `IX_Description_id_programme_source` ON `Description` (`id_programme`, `source`);

CREATE INDEX `IX_Emission_channel_id` ON `Emission` (`channel_id`);

CREATE INDEX `IX_Emission_programme_id` ON `Emission` (`programme_id`);

CREATE UNIQUE INDEX `IX_Emission_start_stop_programme_id_channel_id` ON `Emission` (`start`, `stop`, `programme_id`, `channel_id`);

CREATE UNIQUE INDEX `IND_feature_id` ON `Feature` (`id`);

CREATE UNIQUE INDEX `IX_Feature_type_value` ON `Feature` (`type`, `value`);

CREATE INDEX `IX_FeatureExample_programme_id` ON `FeatureExample` (`programme_id`);

CREATE UNIQUE INDEX `IX_GuideUpdate_posted` ON `GuideUpdate` (`posted`);

CREATE UNIQUE INDEX `IND_programme_id` ON `Programme` (`id`);

CREATE INDEX `IX_Programme_series_id` ON `Programme` (`series_id`);

CREATE UNIQUE INDEX `IX_Programme_title` ON `Programme` (`title`);

CREATE INDEX `IX_Rating_programme_id` ON `Rating` (`programme_id`);

CREATE UNIQUE INDEX `IX_Series_title` ON `Series` (`title`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190611231726_Initial', '2.2.2-servicing-10034');


