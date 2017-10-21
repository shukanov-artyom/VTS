
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/06/2015 18:08:30
-- Generated from EDMX file: D:\dev\VSO5\VTS Webservice\VTSWebService.DataAccess\VtsDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VTS2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AnalyticRuleSettingsEntitySettingsMoleculeEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SettingsMolecule] DROP CONSTRAINT [FK_AnalyticRuleSettingsEntitySettingsMoleculeEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_AnalyticStatisticsItemEntityAnalyticStatisticsValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnalyticStatisticsValue] DROP CONSTRAINT [FK_AnalyticStatisticsItemEntityAnalyticStatisticsValue];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerVehicleAssociationEntityLicenseEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[License] DROP CONSTRAINT [FK_PartnerVehicleAssociationEntityLicenseEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_PsaDatasetEntityPsaTraceEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PsaTrace] DROP CONSTRAINT [FK_PsaDatasetEntityPsaTraceEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleEntityPsaDatasetEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PsaDataset] DROP CONSTRAINT [FK_VehicleEntityPsaDatasetEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_PsaParametersSetEntityPsaParameterDataEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PsaParameterData] DROP CONSTRAINT [FK_PsaParametersSetEntityPsaParameterDataEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_PsaTraceEntityPsaParametersSetEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PsaParametersSet] DROP CONSTRAINT [FK_PsaTraceEntityPsaParametersSetEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingsMoleculeEntitySettingsAtomEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SettingsAtom] DROP CONSTRAINT [FK_SettingsMoleculeEntitySettingsAtomEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemNewsSystemNewsLocalized]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemNewsLocalized] DROP CONSTRAINT [FK_SystemNewsSystemNewsLocalized];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerVehicleAssociationEntityUserEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserVehicleAssociation] DROP CONSTRAINT [FK_PartnerVehicleAssociationEntityUserEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerVehicleAssociationEntityVehicleEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserVehicleAssociation] DROP CONSTRAINT [FK_PartnerVehicleAssociationEntityVehicleEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleEntityVehicleEventEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehicleEvent] DROP CONSTRAINT [FK_VehicleEntityVehicleEventEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleCharacteristicsEntityVehicleCharacteristicsItemsGroupEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehicleCharacteristicsItemGroup] DROP CONSTRAINT [FK_VehicleCharacteristicsEntityVehicleCharacteristicsItemsGroupEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleCharacteristicsItemsGroupEntityVehicleCharacteristicsItemEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehicleCharacteristicsItem] DROP CONSTRAINT [FK_VehicleCharacteristicsItemsGroupEntityVehicleCharacteristicsItemEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEntityUserProfileEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserEntityUserProfileEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_User_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_0];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AnalyticRuleSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnalyticRuleSettings];
GO
IF OBJECT_ID(N'[dbo].[AnalyticStatisticsItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnalyticStatisticsItem];
GO
IF OBJECT_ID(N'[dbo].[AnalyticStatisticsValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnalyticStatisticsValue];
GO
IF OBJECT_ID(N'[dbo].[License]', 'U') IS NOT NULL
    DROP TABLE [dbo].[License];
GO
IF OBJECT_ID(N'[dbo].[PsaDataset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PsaDataset];
GO
IF OBJECT_ID(N'[dbo].[PsaParameterData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PsaParameterData];
GO
IF OBJECT_ID(N'[dbo].[PsaParametersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PsaParametersSet];
GO
IF OBJECT_ID(N'[dbo].[PsaTrace]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PsaTrace];
GO
IF OBJECT_ID(N'[dbo].[SettingsAtom]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingsAtom];
GO
IF OBJECT_ID(N'[dbo].[SettingsMolecule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingsMolecule];
GO
IF OBJECT_ID(N'[dbo].[SystemNews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemNews];
GO
IF OBJECT_ID(N'[dbo].[SystemNewsLocalized]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemNewsLocalized];
GO
IF OBJECT_ID(N'[dbo].[UnrecognizedVin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnrecognizedVin];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserVehicleAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserVehicleAssociation];
GO
IF OBJECT_ID(N'[dbo].[Vehicle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehicle];
GO
IF OBJECT_ID(N'[dbo].[VehicleCharacteristics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehicleCharacteristics];
GO
IF OBJECT_ID(N'[dbo].[VehicleCharacteristicsItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehicleCharacteristicsItem];
GO
IF OBJECT_ID(N'[dbo].[VehicleCharacteristicsItemGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehicleCharacteristicsItemGroup];
GO
IF OBJECT_ID(N'[dbo].[VehicleEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehicleEvent];
GO
IF OBJECT_ID(N'[dbo].[AgentVersion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgentVersion];
GO
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AnalyticRuleSettings'
CREATE TABLE [dbo].[AnalyticRuleSettings] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [RuleType] int  NOT NULL,
    [EngineFamilyType] int  NULL,
    [EngineType] int  NULL,
    [Reliability] int  NOT NULL
);
GO

-- Creating table 'AnalyticStatisticsItem'
CREATE TABLE [dbo].[AnalyticStatisticsItem] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [VersionGenerated] nvarchar(max)  NOT NULL,
    [DateGenerated] datetime  NOT NULL,
    [Type] int  NOT NULL,
    [TargetEngineFamilyType] int  NOT NULL,
    [TargetEngineType] int  NOT NULL
);
GO

-- Creating table 'AnalyticStatisticsValue'
CREATE TABLE [dbo].[AnalyticStatisticsValue] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [AnalyticStatisticsItemId] bigint  NOT NULL,
    [Value] float  NOT NULL,
    [SourceVehicleVin] nvarchar(max)  NOT NULL,
    [SourcePsaParametersSetId] bigint  NOT NULL,
    [SourceDataCaptureDateTime] datetime  NOT NULL
);
GO

-- Creating table 'License'
CREATE TABLE [dbo].[License] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [State] int  NOT NULL,
    [ServerDateIssued] datetime  NOT NULL,
    [Type] int  NOT NULL,
    [Period] int  NOT NULL,
    [PartnerVehicleAssociationEntityLicenseEntity_LicenseEntity_Id] bigint  NOT NULL
);
GO

-- Creating table 'PsaDataset'
CREATE TABLE [dbo].[PsaDataset] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [DateSaved] datetime  NOT NULL,
    [Guid] uniqueidentifier  NOT NULL,
    [DateExported] datetime  NOT NULL,
    [VehicleEntityId] bigint  NOT NULL
);
GO

-- Creating table 'PsaParameterData'
CREATE TABLE [dbo].[PsaParameterData] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [PsaParametersSetEntityId] bigint  NOT NULL,
    [HasTimestamps] bit  NOT NULL,
    [Type] int  NOT NULL,
    [Units] int  NOT NULL,
    [Values] nvarchar(max)  NOT NULL,
    [Timestamps] nvarchar(max)  NULL,
    [OriginalTypeId] nvarchar(max)  NULL,
    [AdditionalSourceInfo] nvarchar(max)  NULL
);
GO

-- Creating table 'PsaParametersSet'
CREATE TABLE [dbo].[PsaParametersSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [PsaTraceEntityId] bigint  NOT NULL,
    [Type] int  NOT NULL,
    [EcuName] nvarchar(max)  NULL,
    [EcuLabel] nvarchar(max)  NULL,
    [OriginalTypeId] nvarchar(max)  NULL,
    [AdditionalSourceInfo] nvarchar(max)  NULL
);
GO

-- Creating table 'PsaTrace'
CREATE TABLE [dbo].[PsaTrace] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [PsaDatasetEntityId] bigint  NOT NULL,
    [Date] datetime  NOT NULL,
    [Application] nvarchar(max)  NULL,
    [Format] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [PhoneChannel] nvarchar(max)  NULL,
    [Vin] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [ToolSerialNumber] nvarchar(max)  NULL,
    [SavesetId] nvarchar(max)  NULL,
    [Mileage] int  NOT NULL,
    [Manufacturer] int  NOT NULL,
    [VehicleModelName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SettingsAtom'
CREATE TABLE [dbo].[SettingsAtom] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [MinOptimal] float  NOT NULL,
    [MaxOptimal] float  NOT NULL,
    [MinAcceptable] float  NOT NULL,
    [MaxAcceptable] float  NOT NULL,
    [Type] int  NOT NULL,
    [SettingsMoleculeEntityId] bigint  NOT NULL
);
GO

-- Creating table 'SettingsMolecule'
CREATE TABLE [dbo].[SettingsMolecule] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [OverrideOptimal] bit  NOT NULL,
    [OverrideAcceptable] bit  NOT NULL,
    [AnalyticRuleSettingsEntityId] bigint  NOT NULL
);
GO

-- Creating table 'SystemNews'
CREATE TABLE [dbo].[SystemNews] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [IsBlocked] bit  NOT NULL,
    [DatePublished] datetime  NOT NULL
);
GO

-- Creating table 'SystemNewsLocalized'
CREATE TABLE [dbo].[SystemNewsLocalized] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Header] nvarchar(max)  NOT NULL,
    [NewsContentText] nvarchar(max)  NOT NULL,
    [Language] nvarchar(max)  NOT NULL,
    [SystemNewsId] bigint  NOT NULL
);
GO

-- Creating table 'UnrecognizedVin'
CREATE TABLE [dbo].[UnrecognizedVin] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Vin] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [RegisteredDate] datetime  NOT NULL,
    [Role] int  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PrincipalId] bigint  NULL
);
GO

-- Creating table 'UserVehicleAssociation'
CREATE TABLE [dbo].[UserVehicleAssociation] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [VehicleEntityId] bigint  NOT NULL,
    [UserEntityId] bigint  NOT NULL
);
GO

-- Creating table 'Vehicle'
CREATE TABLE [dbo].[Vehicle] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Vin] nvarchar(max)  NOT NULL,
    [Manufacturer] int  NOT NULL,
    [ModelName] nvarchar(max)  NOT NULL,
    [Registered] datetime  NOT NULL,
    [ProductionYear] int  NOT NULL,
    [Mileage] int  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'VehicleCharacteristics'
CREATE TABLE [dbo].[VehicleCharacteristics] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Language] nvarchar(50)  NOT NULL,
    [GeneralVehicleInfo] nvarchar(max)  NULL,
    [Vin] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VehicleCharacteristicsItem'
CREATE TABLE [dbo].[VehicleCharacteristicsItem] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [VehicleCharacteristicsItemsGroupEntityId] bigint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VehicleCharacteristicsItemGroup'
CREATE TABLE [dbo].[VehicleCharacteristicsItemGroup] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [VehicleCharacteristicsEntityId] bigint  NOT NULL
);
GO

-- Creating table 'VehicleEvent'
CREATE TABLE [dbo].[VehicleEvent] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [Mileage] int  NOT NULL,
    [Type] int  NOT NULL,
    [YellowMileage] int  NULL,
    [RedMileage] int  NULL,
    [VehicleEntityId] bigint  NOT NULL,
    [NextReplacementPeriod] int  NOT NULL
);
GO

-- Creating table 'AgentVersion'
CREATE TABLE [dbo].[AgentVersion] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [VersionString] nvarchar(max)  NOT NULL,
    [DownloadLink] nvarchar(max)  NOT NULL,
    [ReleasedDate] datetime  NOT NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [dbo].[UserProfile] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AnalyticRuleSettings'
ALTER TABLE [dbo].[AnalyticRuleSettings]
ADD CONSTRAINT [PK_AnalyticRuleSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnalyticStatisticsItem'
ALTER TABLE [dbo].[AnalyticStatisticsItem]
ADD CONSTRAINT [PK_AnalyticStatisticsItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnalyticStatisticsValue'
ALTER TABLE [dbo].[AnalyticStatisticsValue]
ADD CONSTRAINT [PK_AnalyticStatisticsValue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'License'
ALTER TABLE [dbo].[License]
ADD CONSTRAINT [PK_License]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PsaDataset'
ALTER TABLE [dbo].[PsaDataset]
ADD CONSTRAINT [PK_PsaDataset]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PsaParameterData'
ALTER TABLE [dbo].[PsaParameterData]
ADD CONSTRAINT [PK_PsaParameterData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PsaParametersSet'
ALTER TABLE [dbo].[PsaParametersSet]
ADD CONSTRAINT [PK_PsaParametersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PsaTrace'
ALTER TABLE [dbo].[PsaTrace]
ADD CONSTRAINT [PK_PsaTrace]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SettingsAtom'
ALTER TABLE [dbo].[SettingsAtom]
ADD CONSTRAINT [PK_SettingsAtom]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SettingsMolecule'
ALTER TABLE [dbo].[SettingsMolecule]
ADD CONSTRAINT [PK_SettingsMolecule]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemNews'
ALTER TABLE [dbo].[SystemNews]
ADD CONSTRAINT [PK_SystemNews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemNewsLocalized'
ALTER TABLE [dbo].[SystemNewsLocalized]
ADD CONSTRAINT [PK_SystemNewsLocalized]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UnrecognizedVin'
ALTER TABLE [dbo].[UnrecognizedVin]
ADD CONSTRAINT [PK_UnrecognizedVin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserVehicleAssociation'
ALTER TABLE [dbo].[UserVehicleAssociation]
ADD CONSTRAINT [PK_UserVehicleAssociation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vehicle'
ALTER TABLE [dbo].[Vehicle]
ADD CONSTRAINT [PK_Vehicle]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehicleCharacteristics'
ALTER TABLE [dbo].[VehicleCharacteristics]
ADD CONSTRAINT [PK_VehicleCharacteristics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehicleCharacteristicsItem'
ALTER TABLE [dbo].[VehicleCharacteristicsItem]
ADD CONSTRAINT [PK_VehicleCharacteristicsItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehicleCharacteristicsItemGroup'
ALTER TABLE [dbo].[VehicleCharacteristicsItemGroup]
ADD CONSTRAINT [PK_VehicleCharacteristicsItemGroup]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehicleEvent'
ALTER TABLE [dbo].[VehicleEvent]
ADD CONSTRAINT [PK_VehicleEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AgentVersion'
ALTER TABLE [dbo].[AgentVersion]
ADD CONSTRAINT [PK_AgentVersion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [PK_UserProfile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AnalyticRuleSettingsEntityId] in table 'SettingsMolecule'
ALTER TABLE [dbo].[SettingsMolecule]
ADD CONSTRAINT [FK_AnalyticRuleSettingsEntitySettingsMoleculeEntity]
    FOREIGN KEY ([AnalyticRuleSettingsEntityId])
    REFERENCES [dbo].[AnalyticRuleSettings]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnalyticRuleSettingsEntitySettingsMoleculeEntity'
CREATE INDEX [IX_FK_AnalyticRuleSettingsEntitySettingsMoleculeEntity]
ON [dbo].[SettingsMolecule]
    ([AnalyticRuleSettingsEntityId]);
GO

-- Creating foreign key on [AnalyticStatisticsItemId] in table 'AnalyticStatisticsValue'
ALTER TABLE [dbo].[AnalyticStatisticsValue]
ADD CONSTRAINT [FK_AnalyticStatisticsItemEntityAnalyticStatisticsValue]
    FOREIGN KEY ([AnalyticStatisticsItemId])
    REFERENCES [dbo].[AnalyticStatisticsItem]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnalyticStatisticsItemEntityAnalyticStatisticsValue'
CREATE INDEX [IX_FK_AnalyticStatisticsItemEntityAnalyticStatisticsValue]
ON [dbo].[AnalyticStatisticsValue]
    ([AnalyticStatisticsItemId]);
GO

-- Creating foreign key on [PartnerVehicleAssociationEntityLicenseEntity_LicenseEntity_Id] in table 'License'
ALTER TABLE [dbo].[License]
ADD CONSTRAINT [FK_PartnerVehicleAssociationEntityLicenseEntity]
    FOREIGN KEY ([PartnerVehicleAssociationEntityLicenseEntity_LicenseEntity_Id])
    REFERENCES [dbo].[UserVehicleAssociation]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerVehicleAssociationEntityLicenseEntity'
CREATE INDEX [IX_FK_PartnerVehicleAssociationEntityLicenseEntity]
ON [dbo].[License]
    ([PartnerVehicleAssociationEntityLicenseEntity_LicenseEntity_Id]);
GO

-- Creating foreign key on [PsaDatasetEntityId] in table 'PsaTrace'
ALTER TABLE [dbo].[PsaTrace]
ADD CONSTRAINT [FK_PsaDatasetEntityPsaTraceEntity]
    FOREIGN KEY ([PsaDatasetEntityId])
    REFERENCES [dbo].[PsaDataset]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PsaDatasetEntityPsaTraceEntity'
CREATE INDEX [IX_FK_PsaDatasetEntityPsaTraceEntity]
ON [dbo].[PsaTrace]
    ([PsaDatasetEntityId]);
GO

-- Creating foreign key on [VehicleEntityId] in table 'PsaDataset'
ALTER TABLE [dbo].[PsaDataset]
ADD CONSTRAINT [FK_VehicleEntityPsaDatasetEntity]
    FOREIGN KEY ([VehicleEntityId])
    REFERENCES [dbo].[Vehicle]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleEntityPsaDatasetEntity'
CREATE INDEX [IX_FK_VehicleEntityPsaDatasetEntity]
ON [dbo].[PsaDataset]
    ([VehicleEntityId]);
GO

-- Creating foreign key on [PsaParametersSetEntityId] in table 'PsaParameterData'
ALTER TABLE [dbo].[PsaParameterData]
ADD CONSTRAINT [FK_PsaParametersSetEntityPsaParameterDataEntity]
    FOREIGN KEY ([PsaParametersSetEntityId])
    REFERENCES [dbo].[PsaParametersSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PsaParametersSetEntityPsaParameterDataEntity'
CREATE INDEX [IX_FK_PsaParametersSetEntityPsaParameterDataEntity]
ON [dbo].[PsaParameterData]
    ([PsaParametersSetEntityId]);
GO

-- Creating foreign key on [PsaTraceEntityId] in table 'PsaParametersSet'
ALTER TABLE [dbo].[PsaParametersSet]
ADD CONSTRAINT [FK_PsaTraceEntityPsaParametersSetEntity]
    FOREIGN KEY ([PsaTraceEntityId])
    REFERENCES [dbo].[PsaTrace]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PsaTraceEntityPsaParametersSetEntity'
CREATE INDEX [IX_FK_PsaTraceEntityPsaParametersSetEntity]
ON [dbo].[PsaParametersSet]
    ([PsaTraceEntityId]);
GO

-- Creating foreign key on [SettingsMoleculeEntityId] in table 'SettingsAtom'
ALTER TABLE [dbo].[SettingsAtom]
ADD CONSTRAINT [FK_SettingsMoleculeEntitySettingsAtomEntity]
    FOREIGN KEY ([SettingsMoleculeEntityId])
    REFERENCES [dbo].[SettingsMolecule]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingsMoleculeEntitySettingsAtomEntity'
CREATE INDEX [IX_FK_SettingsMoleculeEntitySettingsAtomEntity]
ON [dbo].[SettingsAtom]
    ([SettingsMoleculeEntityId]);
GO

-- Creating foreign key on [SystemNewsId] in table 'SystemNewsLocalized'
ALTER TABLE [dbo].[SystemNewsLocalized]
ADD CONSTRAINT [FK_SystemNewsSystemNewsLocalized]
    FOREIGN KEY ([SystemNewsId])
    REFERENCES [dbo].[SystemNews]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemNewsSystemNewsLocalized'
CREATE INDEX [IX_FK_SystemNewsSystemNewsLocalized]
ON [dbo].[SystemNewsLocalized]
    ([SystemNewsId]);
GO

-- Creating foreign key on [UserEntityId] in table 'UserVehicleAssociation'
ALTER TABLE [dbo].[UserVehicleAssociation]
ADD CONSTRAINT [FK_PartnerVehicleAssociationEntityUserEntity]
    FOREIGN KEY ([UserEntityId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerVehicleAssociationEntityUserEntity'
CREATE INDEX [IX_FK_PartnerVehicleAssociationEntityUserEntity]
ON [dbo].[UserVehicleAssociation]
    ([UserEntityId]);
GO

-- Creating foreign key on [VehicleEntityId] in table 'UserVehicleAssociation'
ALTER TABLE [dbo].[UserVehicleAssociation]
ADD CONSTRAINT [FK_PartnerVehicleAssociationEntityVehicleEntity]
    FOREIGN KEY ([VehicleEntityId])
    REFERENCES [dbo].[Vehicle]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerVehicleAssociationEntityVehicleEntity'
CREATE INDEX [IX_FK_PartnerVehicleAssociationEntityVehicleEntity]
ON [dbo].[UserVehicleAssociation]
    ([VehicleEntityId]);
GO

-- Creating foreign key on [VehicleEntityId] in table 'VehicleEvent'
ALTER TABLE [dbo].[VehicleEvent]
ADD CONSTRAINT [FK_VehicleEntityVehicleEventEntity]
    FOREIGN KEY ([VehicleEntityId])
    REFERENCES [dbo].[Vehicle]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleEntityVehicleEventEntity'
CREATE INDEX [IX_FK_VehicleEntityVehicleEventEntity]
ON [dbo].[VehicleEvent]
    ([VehicleEntityId]);
GO

-- Creating foreign key on [VehicleCharacteristicsEntityId] in table 'VehicleCharacteristicsItemGroup'
ALTER TABLE [dbo].[VehicleCharacteristicsItemGroup]
ADD CONSTRAINT [FK_VehicleCharacteristicsEntityVehicleCharacteristicsItemsGroupEntity]
    FOREIGN KEY ([VehicleCharacteristicsEntityId])
    REFERENCES [dbo].[VehicleCharacteristics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleCharacteristicsEntityVehicleCharacteristicsItemsGroupEntity'
CREATE INDEX [IX_FK_VehicleCharacteristicsEntityVehicleCharacteristicsItemsGroupEntity]
ON [dbo].[VehicleCharacteristicsItemGroup]
    ([VehicleCharacteristicsEntityId]);
GO

-- Creating foreign key on [VehicleCharacteristicsItemsGroupEntityId] in table 'VehicleCharacteristicsItem'
ALTER TABLE [dbo].[VehicleCharacteristicsItem]
ADD CONSTRAINT [FK_VehicleCharacteristicsItemsGroupEntityVehicleCharacteristicsItemEntity]
    FOREIGN KEY ([VehicleCharacteristicsItemsGroupEntityId])
    REFERENCES [dbo].[VehicleCharacteristicsItemGroup]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleCharacteristicsItemsGroupEntityVehicleCharacteristicsItemEntity'
CREATE INDEX [IX_FK_VehicleCharacteristicsItemsGroupEntityVehicleCharacteristicsItemEntity]
ON [dbo].[VehicleCharacteristicsItem]
    ([VehicleCharacteristicsItemsGroupEntityId]);
GO

-- Creating foreign key on [UserId] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [FK_UserEntityUserProfileEntity]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEntityUserProfileEntity'
CREATE INDEX [IX_FK_UserEntityUserProfileEntity]
ON [dbo].[UserProfile]
    ([UserId]);
GO

-- Creating foreign key on [PrincipalId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_0]
    FOREIGN KEY ([PrincipalId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_0'
CREATE INDEX [IX_FK_User_0]
ON [dbo].[User]
    ([PrincipalId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------