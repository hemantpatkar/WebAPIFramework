 
CREATE TABLE [dbo].mcountry(
	ID bigint IDENTITY(1,1) NOT NULL,
	[Code] [varchar](25) NULL,
	[Name] [varchar](25) NULL, 
	CreatedOn [datetimeoffset](7) not NULL,
	CreatedBY [varchar](30) not NULL,
	UpdatedOn [datetimeoffset](7) NULL,
	UpdatedBY [varchar](30) NULL,
	IsActive tinyint not NULL
PRIMARY KEY CLUSTERED 
(
	ID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
insert into mcountry values('IN','INDIA','2021-12-24','1',NULL,NULL,1)