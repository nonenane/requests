CREATE TABLE [requests].[j_CatalogPromotionalTovars](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_tovar]		int				not null,
	[Price]			numeric(11,2)	not null,
	[SalePrice]		numeric(11,2)	not null,
	[id_Creator]	int				not	null,
	[DateCreate]	datetime		null,
 CONSTRAINT [PK_j_CatalogPromotionalTovars] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [requests].[j_CatalogPromotionalTovars] ADD CONSTRAINT FK_j_CatalogPromotionalTovars_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [requests].[j_CatalogPromotionalTovars] ADD CONSTRAINT FK_j_CatalogPromotionalTovars_id_tovar FOREIGN KEY (id_tovar)  REFERENCES [dbo].[s_tovar] (id)
GO




ALTER TABLE [requests].[s_CatalogPromotionalTovars]
	ADD [Price]			numeric(11,2)	null,
	[SalePrice]		numeric(11,2)	null,
	[id_Editor]	int				null,
	[DateEdit]	datetime		null



