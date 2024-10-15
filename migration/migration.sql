
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Activity](
	[idActivity] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NOT NULL,
	[dateActivity] [datetime] NOT NULL,
	[auditorium] [varchar](50) NOT NULL,
	[place] [varchar](50) NOT NULL,
	[idThesis] [int] NOT NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
	[latitude] [varchar](50) NULL,
	[longitude] [varchar](50) NULL,
	[stateActivity] [varchar](50) NULL,
	[hasAssistance] [bit] NULL,
	[hasPayment] [bit] NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[idActivity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ActivityProfessional](
	[idActivityProfessional] [int] IDENTITY(1,1) NOT NULL,
	[idProfessional] [int] NOT NULL,
	[idActivity] [int] NOT NULL,
 CONSTRAINT [PK_ActivityProfessional] PRIMARY KEY CLUSTERED 
(
	[idActivityProfessional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ActivityVoucher](
	[idActivityVoucher] [int] IDENTITY(1,1) NOT NULL,
	[voucherType] [varchar](30) NOT NULL,
	[voucherFile] [varbinary](max) NOT NULL,
	[nameFile] [varchar](255) NOT NULL,
	[idActivity] [int] NOT NULL,
 CONSTRAINT [PK_ActivityVoucher] PRIMARY KEY CLUSTERED 
(
	[idActivityVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[Letter]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Letter](
	[idLetter] [int] IDENTITY(1,1) NOT NULL,
	[idProfessional] [int] NULL,
	[format] [varchar](50) NULL,
	[idActivity] [int] NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
 CONSTRAINT [PK_Letter] PRIMARY KEY CLUSTERED 
(
	[idLetter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Notification]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Notification](
	[idNotification] [int] IDENTITY(1,1) NOT NULL,
	[message] [varchar](50) NULL,
	[dateTime] [datetime] NULL,
	[idProfessional] [int] NULL,
	[idLetter] [int] NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[idNotification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Person]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Person](
	[idPerson] [int] IDENTITY(1,1) NOT NULL,
	[names] [varchar](60) NOT NULL,
	[lastname] [varchar](35) NOT NULL,
	[secondLastName] [varchar](35) NULL,
	[identityNumber] [varchar](15) NOT NULL,
	[phoneNumber] [varchar](20) NOT NULL,
	[email] [varchar](60) NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[idPerson] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Professional]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Professional](
	[idProfessional] [int] IDENTITY(1,1) NOT NULL,
	[specialty] [varchar](50) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[idPerson] [int] NOT NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
	[university] [varchar](100) NULL,
	[career] [varchar](100) NULL,
 CONSTRAINT [PK_Professional] PRIMARY KEY CLUSTERED 
(
	[idProfessional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Thesis]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Thesis](
	[idThesis] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
	[description] [varchar](100) NULL,
	[student] [varchar](50) NULL,
	[career] [varchar](50) NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
 CONSTRAINT [PK_Thesis] PRIMARY KEY CLUSTERED 
(
	[idThesis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[ThesisFile]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ThesisFile](
	[idThesisFile] [int] IDENTITY(1,1) NOT NULL,
	[ThesisType] [varchar](50) NOT NULL,
	[DataFile] [varbinary](max) NOT NULL,
	[nameFile] [varchar](255) NOT NULL,
	[idThesis] [int] NOT NULL,
 CONSTRAINT [PK_ThesisFile] PRIMARY KEY CLUSTERED 
(
	[idThesisFile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[User]    Script Date: 28/5/2024 20:51:57 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[User](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](35) NOT NULL,
	[password] [varchar](250) NOT NULL,
	[role] [varchar](50) NOT NULL,
	[idPerson] [int] NOT NULL,
	[registerDate] [datetime] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[idUserRegister] [int] NULL,
	[idUserLastUpdate] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([idPerson], [names], [lastname], [secondLastName], [identityNumber], [phoneNumber], [email], [registerDate], [lastUpdate], [status], [idUserRegister], [idUserLastUpdate]) VALUES (1, N'Tecnico', N'Compañia', NULL, N'12345678', N'64947823', N'berriosanderson807@gmail.com', CAST(N'2024-04-03T05:43:43.463' AS DateTime), NULL, 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Person] OFF

SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([idUser], [userName], [password], [role], [idPerson], [registerDate], [lastUpdate], [status], [idUserRegister], [idUserLastUpdate]) VALUES (1, N'Tecnico', N'57e7113182a6457a782673d74feb1866e7115e3fc1005b42f2a1ebfa4f1fc6c1', N'Administrador', 1, CAST(N'2024-05-28T20:01:26.477' AS DateTime), NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[User] ([idUser], [userName], [password], [role], [idPerson], [registerDate], [lastUpdate], [status], [idUserRegister], [idUserLastUpdate]) VALUES (1, N'Principal', N'ee94a82e3ffe6ecc306dbc4b4832ed5abb33efc81846eb9f50b4c81c7849747c', N'Principal', 1, CAST(N'2024-05-28T20:01:26.477' AS DateTime), NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[User] ([idUser], [userName], [password], [role], [idPerson], [registerDate], [lastUpdate], [status], [idUserRegister], [idUserLastUpdate]) VALUES (1, N'Secundario', N'f8c4991920065ec33a556a814428f3130a4da28669c25a87271079fabb4e8f06', N'Secundario', 1, CAST(N'2024-05-28T20:01:26.477' AS DateTime), NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF

ALTER TABLE [dbo].[Activity] ADD  CONSTRAINT [DF_Activity_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Activity] ADD  CONSTRAINT [DF_Activity_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Letter] ADD  CONSTRAINT [DF_Letter_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Letter] ADD  CONSTRAINT [DF_Letter_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Notification] ADD  CONSTRAINT [DF_Notification_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Notification] ADD  CONSTRAINT [DF_Notification_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Professional] ADD  CONSTRAINT [DF_Professional_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Professional] ADD  CONSTRAINT [DF_Professional_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Thesis] ADD  CONSTRAINT [DF_Thesis_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[Thesis] ADD  CONSTRAINT [DF_Thesis_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_registerDate]  DEFAULT (getdate()) FOR [registerDate]

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_status]  DEFAULT ((1)) FOR [status]

ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Thesis] FOREIGN KEY([idThesis])
REFERENCES [dbo].[Thesis] ([idThesis])

ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Thesis]

ALTER TABLE [dbo].[ActivityProfessional]  WITH CHECK ADD  CONSTRAINT [FK_ActivityProfessional_Activity] FOREIGN KEY([idActivity])
REFERENCES [dbo].[Activity] ([idActivity])

ALTER TABLE [dbo].[ActivityProfessional] CHECK CONSTRAINT [FK_ActivityProfessional_Activity]

ALTER TABLE [dbo].[ActivityProfessional]  WITH CHECK ADD  CONSTRAINT [FK_ActivityProfessional_Professional] FOREIGN KEY([idProfessional])
REFERENCES [dbo].[Professional] ([idProfessional])

ALTER TABLE [dbo].[ActivityProfessional] CHECK CONSTRAINT [FK_ActivityProfessional_Professional]

ALTER TABLE [dbo].[ActivityVoucher]  WITH CHECK ADD  CONSTRAINT [FK_ActivityVoucher_Activity] FOREIGN KEY([idActivity])
REFERENCES [dbo].[Activity] ([idActivity])

ALTER TABLE [dbo].[ActivityVoucher] CHECK CONSTRAINT [FK_ActivityVoucher_Activity]

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_Activity] FOREIGN KEY([idActivity])
REFERENCES [dbo].[Activity] ([idActivity])

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_Activity]

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_Professional1] FOREIGN KEY([idProfessional])
REFERENCES [dbo].[Professional] ([idProfessional])

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_Professional1]

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Letter] FOREIGN KEY([idLetter])
REFERENCES [dbo].[Letter] ([idLetter])

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Letter]

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Professional1] FOREIGN KEY([idProfessional])
REFERENCES [dbo].[Professional] ([idProfessional])

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Professional1]

ALTER TABLE [dbo].[Professional]  WITH CHECK ADD  CONSTRAINT [FK_Professional_Person1] FOREIGN KEY([idPerson])
REFERENCES [dbo].[Person] ([idPerson])

ALTER TABLE [dbo].[Professional] CHECK CONSTRAINT [FK_Professional_Person1]

ALTER TABLE [dbo].[ThesisFile]  WITH CHECK ADD  CONSTRAINT [FK_ThesisFile_Thesis] FOREIGN KEY([idThesis])
REFERENCES [dbo].[Thesis] ([idThesis])

ALTER TABLE [dbo].[ThesisFile] CHECK CONSTRAINT [FK_ThesisFile_Thesis]

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Person] FOREIGN KEY([idPerson])
REFERENCES [dbo].[Person] ([idPerson])

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Person]