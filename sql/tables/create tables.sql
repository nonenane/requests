CREATE TABLE [requests].[s_TovarMatrix](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_tovar]		int				not null,
	[id_Creator]	int				not	null,
	[DateCreate]	datetime		null,
 CONSTRAINT [PK_s_TovarMatrix] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [requests].[s_TovarMatrix] ADD CONSTRAINT FK_s_TovarMatrix_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [requests].[s_TovarMatrix] ADD CONSTRAINT FK_s_TovarMatrix_id_tovar FOREIGN KEY (id_tovar)  REFERENCES [dbo].[s_tovar] (id)
GO
