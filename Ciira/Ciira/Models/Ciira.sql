CREATE TABLE [dbo].[User] (
    [Code]      VARCHAR (36)   NOT NULL,
    [Email]     VARCHAR (1000) NOT NULL,
    [Password]  VARCHAR (1000) NOT NULL,
    [Name]      VARCHAR (1000) NOT NULL,
    [AvatarUrl] VARCHAR (1000) NOT NULL,
    [Kind] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Code] ASC)
);

