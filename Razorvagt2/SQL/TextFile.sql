CREATE TABLE [dbo].[AssignmentType] (
    [AssignmentType_ID] INT          IDENTITY (1, 1) NOT NULL,
    [Type]              VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AssignmentType_ID] ASC)
);




CREATE TABLE [dbo].[Users] (
    [User_ID]  INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Username] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    [Phone]    VARCHAR (50) NULL,
    [Email]    VARCHAR (50) NOT NULL,
    [Admin]    BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([User_ID] ASC)
);




CREATE TABLE [dbo].[Assignment] (
    [Assignment_ID]     INT      IDENTITY (1, 1) NOT NULL,
    [User_ID]           INT      NULL,
    [Date]              DATETIME NULL,
    [Length]            BIGINT   NULL,
    [AssignmentType_ID] INT      NOT NULL,
    [Schedule_ID]       INT      NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Assignment_ID] ASC),
    CONSTRAINT [FK_Assignment_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[Users] ([User_ID]),
    CONSTRAINT [FK_Assignment_Schedule] FOREIGN KEY ([Schedule_ID]) REFERENCES [dbo].[Schedule] ([Schedule_ID]),
    CONSTRAINT [FK_Assignment_AssignmentType] FOREIGN KEY ([AssignmentType_ID]) REFERENCES [dbo].[AssignmentType] ([AssignmentType_ID])
);

CREATE TABLE [dbo].[Preference] (
    [Preference_ID]  INT          IDENTITY (1, 1) NOT NULL,
    [PreferenceName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Preference_ID] ASC)
);

CREATE TABLE [dbo].[Schedule] (
    [Schedule_ID] INT      IDENTITY (1, 1) NOT NULL,
    [Start_Date]  DATETIME NULL,
    [End-Date]    DATETIME NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED ([Schedule_ID] ASC)
);

