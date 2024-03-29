USE [FinanceiroPECE]
GO
/****** Object:  Table [dbo].[Aluno]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aluno](
	[NumeroPece] [int] NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[Endereco] [nvarchar](max) NULL,
	[Telefone] [nvarchar](20) NULL,
 CONSTRAINT [PK_Aluno] PRIMARY KEY CLUSTERED 
(
	[NumeroPece] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[NomeAcesso] [nvarchar](max) NOT NULL,
	[SenhaAcesso] [nvarchar](max) NOT NULL,
	[TipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projeto]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projeto](
	[CodigoProjeto] [nvarchar](50) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Descricao] [nvarchar](max) NULL,
	[ValorProjeto] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Projeto] PRIMARY KEY CLUSTERED 
(
	[CodigoProjeto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matricula]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matricula](
	[IdMatricula] [int] IDENTITY(1,1) NOT NULL,
	[IdAluno] [int] NOT NULL,
	[IdProjeto] [nvarchar](50) NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_ProjetoAluno] PRIMARY KEY CLUSTERED 
(
	[IdMatricula] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogFinanceiro]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogFinanceiro](
	[IdLog] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[LogInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogFinanceiro] PRIMARY KEY CLUSTERED 
(
	[IdLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Financeiro]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Financeiro](
	[IdFinanceiro] [int] IDENTITY(1,1) NOT NULL,
	[IdMatricula] [int] NOT NULL,
	[NumeroParcelas] [int] NOT NULL,
	[PrecoReajustado] [decimal](18, 2) NOT NULL,
	[Observacoes] [nvarchar](max) NULL,
	[DiaPagamento] [datetime] NOT NULL,
	[PrimeiraParcela] [datetime] NOT NULL,
 CONSTRAINT [PK_Financeiro_1] PRIMARY KEY CLUSTERED 
(
	[IdFinanceiro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parcelas]    Script Date: 04/24/2009 05:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parcelas](
	[NumeroParcela] [int] NOT NULL,
	[IdFinanceiro] [int] NOT NULL,
	[DataVencimento] [datetime] NOT NULL,
	[DataPagamento] [datetime] NULL,
	[ValorPagar] [decimal](18, 2) NOT NULL,
	[ValorPago] [decimal](18, 2) NOT NULL,
	[Observacao] [nvarchar](max) NULL,
	[Pago] [bit] NULL,
 CONSTRAINT [PK_Parcelas] PRIMARY KEY CLUSTERED 
(
	[NumeroParcela] ASC,
	[IdFinanceiro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Financeiro_Matricula]    Script Date: 04/24/2009 05:27:54 ******/
ALTER TABLE [dbo].[Financeiro]  WITH CHECK ADD  CONSTRAINT [FK_Financeiro_Matricula] FOREIGN KEY([IdMatricula])
REFERENCES [dbo].[Matricula] ([IdMatricula])
GO
ALTER TABLE [dbo].[Financeiro] CHECK CONSTRAINT [FK_Financeiro_Matricula]
GO
/****** Object:  ForeignKey [FK_LogFinanceiro_LogFinanceiro]    Script Date: 04/24/2009 05:27:54 ******/
ALTER TABLE [dbo].[LogFinanceiro]  WITH CHECK ADD  CONSTRAINT [FK_LogFinanceiro_LogFinanceiro] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[LogFinanceiro] CHECK CONSTRAINT [FK_LogFinanceiro_LogFinanceiro]
GO
/****** Object:  ForeignKey [FK_Matricula_Aluno]    Script Date: 04/24/2009 05:27:54 ******/
ALTER TABLE [dbo].[Matricula]  WITH CHECK ADD  CONSTRAINT [FK_Matricula_Aluno] FOREIGN KEY([IdAluno])
REFERENCES [dbo].[Aluno] ([NumeroPece])
GO
ALTER TABLE [dbo].[Matricula] CHECK CONSTRAINT [FK_Matricula_Aluno]
GO
/****** Object:  ForeignKey [FK_Matricula_Projeto]    Script Date: 04/24/2009 05:27:54 ******/
ALTER TABLE [dbo].[Matricula]  WITH CHECK ADD  CONSTRAINT [FK_Matricula_Projeto] FOREIGN KEY([IdProjeto])
REFERENCES [dbo].[Projeto] ([CodigoProjeto])
GO
ALTER TABLE [dbo].[Matricula] CHECK CONSTRAINT [FK_Matricula_Projeto]
GO
/****** Object:  ForeignKey [FK_Parcelas_Financeiro]    Script Date: 04/24/2009 05:27:54 ******/
ALTER TABLE [dbo].[Parcelas]  WITH CHECK ADD  CONSTRAINT [FK_Parcelas_Financeiro] FOREIGN KEY([IdFinanceiro])
REFERENCES [dbo].[Financeiro] ([IdFinanceiro])
GO
ALTER TABLE [dbo].[Parcelas] CHECK CONSTRAINT [FK_Parcelas_Financeiro]
GO
