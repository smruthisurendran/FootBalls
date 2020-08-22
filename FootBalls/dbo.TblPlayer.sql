﻿CREATE TABLE [dbo].[TblPlayer] (
    [PlayerReferenceNumber] INT NOT NULL,
    [PlayerId]              INT           IDENTITY (1, 1) NOT NULL,
    [Player]                VARCHAR (100) NULL,
    [DOB]                   DATETIME      NULL,
    [CityId]                INT           NULL,
    [Length]                INT           NULL,
    [Weight]                INT           NULL,
    [PlayingFoot]           INT           NULL,
    [PlayingPlace]          INT           NULL,
    [Photo]                 IMAGE NULL,
    [Confirmed]             BIT           NULL,
    [RegistrationDate]      DATETIME      NULL,
    [ExpirationDate]        DATETIME      NULL,
    [SponsorId]             INT           NULL,
    [Mobile]                VARCHAR (100) NULL,
    [UserId]                INT           NULL,
    [Status]                INT           NULL,
    [CreatedId]             INT           NULL,
    [CreatedDate]           DATETIME      NULL,
    [ModifiedId]            INT           NULL,
    [ModifiedDate]          DATETIME      NULL,
    CONSTRAINT [PK_TblPlayer] PRIMARY KEY CLUSTERED ([PlayerId] ASC),
    CONSTRAINT [FK_TblPlayer_TblCity] FOREIGN KEY ([CityId]) REFERENCES [dbo].[TblCity] ([CityId]),
    CONSTRAINT [FK_TblPlayer_TblUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[TblUser] ([UserId]),
    CONSTRAINT [FK_TblPlayer_TblPlayingPlace] FOREIGN KEY ([PlayingPlace]) REFERENCES [dbo].[TblPlayingPlace] ([PlayingPlaceId])
);

