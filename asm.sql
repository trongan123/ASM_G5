USE [ASMF]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[IDCTDH] [nvarchar](5) NOT NULL,
	[IDMon] [nvarchar](5) NOT NULL,
	[IDDonHang] [nvarchar](5) NOT NULL,
	[DonGia] [money] NULL,
	[SoLuong] [int] NULL,
	[TT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDCTDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[IDcomment] [nvarchar](5) NOT NULL,
	[IDKH] [nvarchar](5) NOT NULL,
	[IDMon] [nvarchar](5) NOT NULL,
	[Rate] [int] NULL,
	[Comment] [nvarchar](500) NULL,
	[TT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDcomment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[IDDonHang] [nvarchar](5) NOT NULL,
	[IDKH] [nvarchar](5) NULL,
	[Diachi] [nvarchar](300) NULL,
	[SDT] [nvarchar](13) NULL,
	[ThoiGian] [date] NULL,
	[TongTien] [money] NULL,
	[TT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[IDGioHang] [nvarchar](5) NOT NULL,
	[IDKH] [nvarchar](5) NULL,
	[IDMon] [nvarchar](5) NULL,
	[DonGia] [money] NULL,
	[SoLuong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[IDKH] [nvarchar](5) NOT NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [nvarchar](30) NULL,
	[HotenKH] [nvarchar](50) NULL,
	[DiachiKH] [nvarchar](300) NULL,
	[SDTKH] [nvarchar](13) NULL,
	[MailKH] [nvarchar](50) NULL,
	[TT] [int] NULL,
	[Role] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Loai]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai](
	[IDLoai] [nvarchar](5) NOT NULL,
	[TenLoai] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MonAn]    Script Date: 3/15/2022 8:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonAn](
	[IDMon] [nvarchar](5) NOT NULL,
	[TenMon] [nvarchar](50) NULL,
	[Hinh] [text] NULL,
	[IDLoai] [nvarchar](5) NULL,
	[DonGia] [money] NULL,
	[ChuThich] [nvarchar](300) NULL,
	[Rate] [int] NULL,
	[TT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_DonHang] FOREIGN KEY([IDDonHang])
REFERENCES [dbo].[DonHang] ([IDDonHang])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_DonHang]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_MonAn] FOREIGN KEY([IDMon])
REFERENCES [dbo].[MonAn] ([IDMon])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_MonAn]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_KhachHang] FOREIGN KEY([IDKH])
REFERENCES [dbo].[KhachHang] ([IDKH])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_KhachHang]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_MonAn] FOREIGN KEY([IDMon])
REFERENCES [dbo].[MonAn] ([IDMon])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_MonAn]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_KhachHang] FOREIGN KEY([IDKH])
REFERENCES [dbo].[KhachHang] ([IDKH])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_KhachHang]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_KhachHang] FOREIGN KEY([IDKH])
REFERENCES [dbo].[KhachHang] ([IDKH])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_KhachHang]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_MonAn] FOREIGN KEY([IDMon])
REFERENCES [dbo].[MonAn] ([IDMon])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_MonAn]
GO
ALTER TABLE [dbo].[MonAn]  WITH CHECK ADD  CONSTRAINT [FK_MonAn_Loai] FOREIGN KEY([IDLoai])
REFERENCES [dbo].[Loai] ([IDLoai])
GO
ALTER TABLE [dbo].[MonAn] CHECK CONSTRAINT [FK_MonAn_Loai]
GO
