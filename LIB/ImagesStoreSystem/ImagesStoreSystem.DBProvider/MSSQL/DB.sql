print Current_Timestamp
print '---”ƒ¿Àﬂ≈Ã “¿¡À»÷€-----------------------------------------' 

DROP TRIGGER ReceivePlainTasks_AI

DROP TABLE [RecivePlainTasks]
DROP TABLE [ImageAttribute]
DROP TABLE [Images]
DROP TABLE [ImageLevels]
DROP TABLE [ReciveSession]
DROP TABLE [Stations]
DROP TABLE [Sensors]
DROP TABLE [Satellites]
DROP TABLE [RemovableFiles]
DROP TABLE [RemovableStorages]
DROP TABLE [Files]
DROP TABLE [FileGroups]
DROP TABLE [AttributeType]

print '”ƒ¿À≈Õ»≈ œ–Œ»«ŒÿÀŒ ”—œ≈ÿÕŒ (ÌÂ Ó·‡˘‡Ú¸ ‚ÌËÏ‡ÌËˇ Ì‡ Ó¯Ë·ÍË)' 
print Current_Timestamp

print '---—Œ«ƒ¿≈Ã “¿¡À»÷€-----------------------------------------'
create table ReciveSession(
	RSessionID BIGINT IDENTITY NOT NULL, 
	RSessionStartTime DATETIME2 default Sysutcdatetime()  not null, 
	RSessionEndTime DATETIME2 default Sysutcdatetime()  not null, 
	Sat_Number BIGINT not null,	 
	St_Number BIGINT not null, 
	DATA_ID BIGINT null, 
	[RSessionCoor] geography NULL,
	[RSessionCoorText] as [RSessionCoor].STAsText(),

	primary key (RSessionID)
) ON [PRIMARY] 


create table Images(
	ImageID BIGINT IDENTITY NOT NULL, 
	ImageCreateTime DATETIME2 default Sysutcdatetime()  not null, 
	ILevel_ID BIGINT not null, 
	DATA_ID BIGINT null, 
	
	Sen_ID BIGINT null,
	ImageSurveyTime DATETIME2 null,
	RSession_ID BIGINT null, 
	Sat_Number BIGINT null,
	
	[ImagePolygon] geography NULL,
	[ImagePolygonText] as [ImagePolygon].STAsText(),
	[ImageCloudiness] float default 0  NOT NULL,
	
	
	primary key (ImageID)
) ON [PRIMARY]

create table AttributeType(
	AttTypeID BIGINT IDENTITY not null, 
	AttTitle NVARCHAR(250) not null unique,
	 
	primary key (AttTypeID)
) ON [STATIC]


create table ImageAttribute(
	IAttID BIGINT IDENTITY not null, 
	Image_ID BIGINT not null, 
	AttType_ID BIGINT not null, 
	IAttValue NVARCHAR(250) not null,
	
	primary key (IAttID)
) ON [PRIMARY]


create table Sensors(
	SenID BIGINT IDENTITY NOT NULL, 
	SenTitle NVARCHAR(25) not null unique, 
	primary key (SenID)
) ON [STATIC] 


create table RemovableStorages(
	RStorageID BIGINT IDENTITY NOT NULL, 
	RStorageTitle NVARCHAR(250) not null unique, 
	primary key (RStorageID)
) ON [STATIC]


create table ImageLevels(
	ILevelID BIGINT IDENTITY NOT NULL, 
	ILevelTitle NVARCHAR(25) not null unique, 
	primary key (ILevelID)
)  ON [STATIC] 


create table RemovableFiles(
	RFileID BIGINT IDENTITY NOT NULL, 
	RFileStoreTime DATETIME2 default Sysutcdatetime()  not null, 
	File_ID BIGINT not null, 
	RStorage_ID BIGINT not null, 
	primary key (RFileID)
)  ON [PRIMARY] 


create table Stations(
	StID BIGINT IDENTITY NOT NULL, 
	StTitle NVARCHAR(25) not null unique, 
	StNumber BIGINT not null unique, 
	primary key (StID)
) ON [STATIC] 


create table Files(
	FileID BIGINT IDENTITY NOT NULL, 
	FileTitle NVARCHAR(250) not null, 
	FileTypeInfo INT DEFAULT 0 not null,
	FileDescription NVARCHAR(250)  null, 
	FileGUID UNIQUEIDENTIFIER ROWGUIDCOL default NEWSEQUENTIALID()  not null unique, 
	FileCreateTime DATETIME2 default Sysutcdatetime()  not null, 
	FileSize BIGINT default 0  not null, 
	FGroup_ID BIGINT not null, 
	[FileContent] varbinary(max) FILESTREAM  NULL,
	primary key (FileID)
) ON [PRIMARY] FILESTREAM_ON [STREAM]


create table FileGroups(
	FGroupID BIGINT IDENTITY NOT NULL, 
	primary key (FGroupID)
) ON [PRIMARY] 


create table Satellites(
	SatID BIGINT IDENTITY NOT NULL, 
	SatTitle NVARCHAR(25) not null unique, 
	SatNumber BIGINT not null unique, 
	primary key (SatID)
) ON [STATIC] 

 


create table RecivePlainTasks(
	RPlainID BIGINT IDENTITY NOT NULL,
	RPlainPackIdentity BIGINT not null,
	
	Sat_Number BIGINT not null, 
	St_Number BIGINT not null, 	
	RPlainStartTime DATETIME2 default Sysutcdatetime()  not null, 
	RPlainEndTime DATETIME2 default Sysutcdatetime()  not null, 	
	RPlainAborted BIT default 0 not null,
	
	RSession_ID BIGINT null,
	RPlainBody NVARCHAR(MAX) null
	
	primary key (RPlainID)
) ON [PRIMARY] 


go



alter table RecivePlainTasks add constraint FK_RPlain_RSession foreign key (RSession_ID) references ReciveSession on delete set null

alter table ReciveSession add constraint FK_RSession_DATA foreign key (DATA_ID) references FileGroups

alter table Images add constraint FK_Image_ILevel foreign key (ILevel_ID) references ImageLevels on delete cascade
alter table Images add constraint FK_Image_RSession foreign key (RSession_ID) references ReciveSession on delete set null
alter table Images add constraint FK_Image_Sen foreign key (Sen_ID) references Sensors on delete set null
alter table Images add constraint FK_Image_DATA foreign key (DATA_ID) references FileGroups

alter table ImageAttribute add constraint FK_ImageAtt_Image foreign key (Image_ID) references Images on delete cascade
alter table ImageAttribute add constraint FK_ImageAtt_Type foreign key (AttType_ID) references AttributeType on delete cascade

alter table RemovableFiles add constraint FK_RemovableFiles_File foreign key (File_ID) references Files on delete cascade
alter table RemovableFiles add constraint FK_RemovableFiles_RStorage foreign key (RStorage_ID) references RemovableStorages on delete cascade

alter table Files add constraint FK_Files_FGroup foreign key (FGroup_ID) references FileGroups on delete cascade

go


CREATE TRIGGER ReceivePlainTasks_AI
   ON  [RecivePlainTasks]
   AFTER  INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	DECLARE @maxPackID BIGINT
	
	SET @maxPackID = (SELECT MAX(RPlainPackIdentity) FROM [RecivePlainTasks])	
	
	IF EXISTS(SELECT * FROM inserted WHERE RPlainPackIdentity < @maxPackID)	
	BEGIN
		RAISERROR (N'The value for RPlainPackIdentity should be greater then maximum value in RecivePlainTasks table = %I64d.', 10, 1, @maxPackID)		
		ROLLBACK TRANSACTION
	END
    
	UPDATE [RecivePlainTasks] SET [RecivePlainTasks].RPlainAborted = 1 
	WHERE [RecivePlainTasks].RPlainID IN 
	
	(SELECT DISTINCT [RecivePlainTasks].RPlainID 
	 FROM inserted INNER JOIN  [RecivePlainTasks] 
			ON ((inserted.RPlainStartTime <= RecivePlainTasks.RPlainEndTime) and 
				( RecivePlainTasks.RPlainStartTime <= inserted.RPlainEndTime) and 
				(RecivePlainTasks.RPlainPackIdentity< inserted.RPlainPackIdentity) and
				(RecivePlainTasks.St_Number = inserted.St_Number))
	)
END

go


print '--- ŒÕ≈÷---------------------------------------------------'
print Current_Timestamp