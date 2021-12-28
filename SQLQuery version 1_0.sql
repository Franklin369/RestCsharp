USE [master]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BASEBRIRESTCSHARP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ARITHABORT OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET  MULTI_USER 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET QUERY_STORE = OFF
GO
USE [BASEBRIRESTCSHARP]
GO
/****** Object:  User [buman]    Script Date: 03/08/2021 10:50:03 ******/
CREATE USER [buman] FOR LOGIN [buman] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [buman]
GO
/****** Object:  Table [dbo].[EMPRESA]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPRESA](
	[Id_empresa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Empresa] [varchar](50) NULL,
	[Logo] [image] NULL,
	[Impuesto] [varchar](50) NULL,
	[Porcentaje_impuesto] [numeric](18, 0) NULL,
	[Moneda] [varchar](50) NULL,
	[Trabajas_con_impuestos] [varchar](50) NULL,
	[Carpeta_para_copias_de_seguridad] [varchar](max) NULL,
	[Ultima_fecha_de_copia_date] [datetime] NULL,
	[Pais] [varchar](max) NULL,
	[Tiponotas] [varchar](50) NULL,
 CONSTRAINT [PK_EMPRESA] PRIMARY KEY CLUSTERED 
(
	[Id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[idclientev] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NULL,
	[Direccion] [varchar](max) NULL,
	[IdentificadorFiscal] [varchar](max) NULL,
	[Celular] [varchar](max) NULL,
	[Estado] [varchar](50) NULL,
	[Saldo] [numeric](18, 2) NULL,
	[Cedula] [varchar](50) NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[idclientev] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[iddetalle_venta] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NOT NULL,
	[Id_producto] [int] NOT NULL,
	[cantidad] [numeric](18, 2) NULL,
	[preciounitario] [numeric](18, 2) NULL,
	[Total_a_pagar]  AS ([preciounitario]*[cantidad]),
	[Estado] [varchar](50) NULL,
	[Costo] [numeric](18, 2) NULL,
	[Ganancia]  AS ([cantidad]*[preciounitario]-[cantidad]*[Costo]),
	[Estado_de_pago] [varchar](50) NULL,
	[Donde_se_consumira] [varchar](50) NULL,
	[Nota] [varchar](max) NULL,
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[iddetalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto1]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto1](
	[Id_Producto1] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Imagen] [image] NULL,
	[Id_grupo] [int] NULL,
	[Precio_de_venta] [numeric](18, 2) NULL,
	[Precio_de_compra] [numeric](18, 2) NULL,
	[Estado_imagen] [varchar](50) NULL,
	[Idcolor] [int] NULL,
	[Estado] [varchar](50) NULL,
 CONSTRAINT [PK_Producto1] PRIMARY KEY CLUSTERED 
(
	[Id_Producto1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id_ticket] [int] IDENTITY(1,1) NOT NULL,
	[Id_Empresa] [int] NULL,
	[Identificador_fiscal] [varchar](max) NULL,
	[Direccion] [varchar](max) NULL,
	[Provincia_Departamento_Pais] [varchar](max) NULL,
	[Nombre_de_Moneda] [varchar](max) NULL,
	[Agradecimiento] [varchar](max) NULL,
	[pagina_Web_Facebook] [varchar](max) NULL,
	[Anuncio] [varchar](max) NULL,
	[Datos_fiscales_de_autorizacion] [varchar](max) NULL,
	[Por_defecto] [varchar](max) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id_ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NULL,
	[Login] [varchar](max) NULL,
	[Password] [varchar](max) NULL,
	[Icono] [image] NULL,
	[Correo] [varchar](max) NULL,
	[Rol] [varchar](max) NULL,
	[Estado] [varchar](max) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ventas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ventas](
	[idventa] [int] IDENTITY(1,1) NOT NULL,
	[idclientev] [int] NULL,
	[fecha_venta] [datetime] NULL,
	[Numero_de_doc] [varchar](50) NULL,
	[Monto_total] [numeric](18, 2) NULL,
	[Tipo_de_pago] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[IGV] [numeric](18, 2) NULL,
	[Comprobante] [varchar](50) NULL,
	[Id_usuario] [int] NULL,
	[Fecha_de_pago] [varchar](50) NULL,
	[Nombrellevar] [varchar](50) NULL,
	[Saldo] [numeric](18, 2) NULL,
	[Pago_con] [numeric](18, 2) NULL,
	[Porcentaje_IGV] [numeric](18, 2) NULL,
	[Idmovcaja] [int] NULL,
	[Referencia_tarjeta] [varchar](50) NULL,
	[Vuelto] [numeric](18, 2) NULL,
	[Efectivo] [numeric](18, 2) NULL,
	[Credito] [numeric](18, 2) NULL,
	[Tarjeta] [numeric](18, 2) NULL,
	[Id_mesa] [int] NULL,
	[Numero_personas] [int] NULL,
	[Nota] [varchar](max) NULL,
 CONSTRAINT [PK_ventas] PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vistaTicketVenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vistaTicketVenta]
AS
SELECT        dbo.Producto1.Descripcion, dbo.detalle_venta.cantidad, dbo.detalle_venta.preciounitario, dbo.detalle_venta.Total_a_pagar, dbo.detalle_venta.iddetalle_venta, dbo.ventas.fecha_venta, dbo.ventas.Monto_total, 
                         dbo.Usuarios.Nombre, dbo.ventas.Pago_con, dbo.ventas.Vuelto, dbo.clientes.Nombre AS Expr1, dbo.ventas.Tipo_de_pago
FROM            dbo.Ticket INNER JOIN
                         dbo.EMPRESA ON dbo.Ticket.Id_Empresa = dbo.EMPRESA.Id_empresa CROSS JOIN
                         dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa INNER JOIN
                         dbo.clientes ON dbo.ventas.idclientev = dbo.clientes.idclientev INNER JOIN
                         dbo.Usuarios ON dbo.ventas.Id_usuario = dbo.Usuarios.IdUsuario INNER JOIN
                         dbo.Producto1 ON dbo.detalle_venta.Id_producto = dbo.Producto1.Id_Producto1














GO
/****** Object:  Table [dbo].[AreasImpresion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreasImpresion](
	[Id_area] [int] IDENTITY(1,1) NOT NULL,
	[Area] [varchar](max) NULL,
	[Codigo] [varchar](50) NULL,
 CONSTRAINT [PK_Areas_de_Impresion] PRIMARY KEY CLUSTERED 
(
	[Id_area] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Caja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Caja](
	[Id_Caja] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Tema] [varchar](50) NULL,
	[Serial_PC] [varchar](max) NULL,
	[Estado] [varchar](50) NULL,
	[Tipo] [varchar](50) NULL,
 CONSTRAINT [PK_Caja] PRIMARY KEY CLUSTERED 
(
	[Id_Caja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colores]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colores](
	[Idcolor] [int] IDENTITY(1,1) NOT NULL,
	[ColorHtml] [varchar](50) NULL,
 CONSTRAINT [PK_Colores] PRIMARY KEY CLUSTERED 
(
	[Idcolor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConceptosGastos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptosGastos](
	[Id_concepto] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](max) NULL,
 CONSTRAINT [PK_CONCEPTOS] PRIMARY KEY CLUSTERED 
(
	[Id_concepto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detallemodificadores]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detallemodificadores](
	[IddetalleMod] [int] IDENTITY(1,1) NOT NULL,
	[Iddetalleventa] [int] NULL,
	[IdModificador] [int] NULL,
 CONSTRAINT [PK_Detallemodificadores] PRIMARY KEY CLUSTERED 
(
	[IddetalleMod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gastosvarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gastosvarios](
	[Idgasto] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Importe] [numeric](18, 2) NULL,
	[Descripcion] [varchar](500) NULL,
	[Id_concepto] [int] NULL,
	[Id_usuario] [int] NULL,
	[Idmovcaja] [int] NULL,
 CONSTRAINT [PK_GASTOSVARIOS] PRIMARY KEY CLUSTERED 
(
	[Idgasto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupo_de_Productos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo_de_Productos](
	[Idline] [int] IDENTITY(1,1) NOT NULL,
	[Grupo] [varchar](50) NULL,
	[Por_defecto] [varchar](50) NULL,
	[Icono] [image] NULL,
	[Estado] [varchar](50) NULL,
	[Estado_de_icono] [varchar](50) NULL,
	[Idcolor] [int] NULL,
 CONSTRAINT [PK_linea] PRIMARY KEY CLUSTERED 
(
	[Idline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpresorasArea]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpresorasArea](
	[Id_impresora] [int] IDENTITY(1,1) NOT NULL,
	[Id_Areas_de_Impresion] [int] NULL,
	[Impresora] [varchar](max) NULL,
	[Idcaja] [int] NULL,
 CONSTRAINT [PK_Impresoras_por_Area] PRIMARY KEY CLUSTERED 
(
	[Id_impresora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingresosvarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingresosvarios](
	[Idingreso] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Importe] [decimal](18, 2) NULL,
	[Descripcion] [varchar](500) NULL,
	[Idmovcaja] [int] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_INGRESOSVARIOS] PRIMARY KEY CLUSTERED 
(
	[Idingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IniciosSesion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IniciosSesion](
	[Idsesion] [int] IDENTITY(1,1) NOT NULL,
	[IdCaja] [int] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_IniciosSesion] PRIMARY KEY CLUSTERED 
(
	[Idsesion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcan]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcan](
	[S] [varchar](max) NULL,
	[F] [varchar](max) NULL,
	[E] [varchar](max) NULL,
	[FA] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[Id_mesa] [int] IDENTITY(1,1) NOT NULL,
	[Mesa] [varchar](50) NULL,
	[Id_salon] [int] NULL,
	[Estado_de_vida] [varchar](50) NULL,
	[Estado_de_Disponibilidad] [varchar](50) NULL,
 CONSTRAINT [PK_Mesas] PRIMARY KEY CLUSTERED 
(
	[Id_mesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modificadores]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modificadores](
	[IdModificador] [int] IDENTITY(1,1) NOT NULL,
	[Modificador] [varchar](max) NULL,
	[Precio] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Modificadores] PRIMARY KEY CLUSTERED 
(
	[IdModificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modulos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulos](
	[IdModulo] [int] IDENTITY(1,1) NOT NULL,
	[Modulo] [varchar](max) NULL,
 CONSTRAINT [PK_Modulos] PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovimientosCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimientosCaja](
	[IdMovimientoCaja] [int] IDENTITY(1,1) NOT NULL,
	[fechainicio] [datetime] NULL,
	[fechafin] [datetime] NULL,
	[ingresos] [numeric](18, 2) NULL,
	[egresos] [numeric](18, 2) NULL,
	[VEfectivo] [numeric](18, 2) NULL,
	[VCredito] [numeric](18, 2) NULL,
	[VTarjeta] [numeric](18, 2) NULL,
	[Idusuario] [int] NULL,
	[EfectivoCalculado] [numeric](18, 2) NULL,
	[EfectivoReal] [numeric](18, 2) NULL,
	[EfectivoDiferencia] [numeric](18, 2) NULL,
	[IdCaja] [int] NULL,
	[Estado] [varchar](50) NULL,
	[EfectivoInicial] [numeric](18, 2) NULL,
 CONSTRAINT [PK_MovimientosCaja] PRIMARY KEY CLUSTERED 
(
	[IdMovimientoCaja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[IdPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdModulo] [int] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Permisos] PRIMARY KEY CLUSTERED 
(
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Propiedades_de_mesas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Propiedades_de_mesas](
	[Id_propiedades] [int] IDENTITY(1,1) NOT NULL,
	[x] [int] NULL,
	[y] [int] NULL,
	[Tamanio_letra] [int] NULL,
 CONSTRAINT [PK_Propiedades_de_mesas] PRIMARY KEY CLUSTERED 
(
	[Id_propiedades] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SALON]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SALON](
	[Id_salon] [int] IDENTITY(1,1) NOT NULL,
	[Salon] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
 CONSTRAINT [PK_SALON] PRIMARY KEY CLUSTERED 
(
	[Id_salon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Serializacion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serializacion](
	[Id_serializacion] [int] IDENTITY(1,1) NOT NULL,
	[Serie] [varchar](50) NULL,
	[Cantidad_de_numeros] [varchar](50) NULL,
	[numerofin] [int] NULL,
	[Tipo] [varchar](50) NULL,
	[Por_defecto] [varchar](50) NULL,
 CONSTRAINT [PK_Serializacion] PRIMARY KEY CLUSTERED 
(
	[Id_serializacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudesImprimir]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitudesImprimir](
	[Idventa] [int] NULL,
	[Tipo] [varchar](50) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_Producto1] FOREIGN KEY([Id_producto])
REFERENCES [dbo].[Producto1] ([Id_Producto1])
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_Producto1]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_ventas1] FOREIGN KEY([idventa])
REFERENCES [dbo].[ventas] ([idventa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_ventas1]
GO
ALTER TABLE [dbo].[Detallemodificadores]  WITH CHECK ADD  CONSTRAINT [FK_Detallemodificadores_detalle_venta] FOREIGN KEY([Iddetalleventa])
REFERENCES [dbo].[detalle_venta] ([iddetalle_venta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Detallemodificadores] CHECK CONSTRAINT [FK_Detallemodificadores_detalle_venta]
GO
ALTER TABLE [dbo].[Detallemodificadores]  WITH CHECK ADD  CONSTRAINT [FK_Detallemodificadores_Modificadores] FOREIGN KEY([IdModificador])
REFERENCES [dbo].[Modificadores] ([IdModificador])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Detallemodificadores] CHECK CONSTRAINT [FK_Detallemodificadores_Modificadores]
GO
ALTER TABLE [dbo].[Gastosvarios]  WITH CHECK ADD  CONSTRAINT [FK_Gastosvarios_MovimientosCaja] FOREIGN KEY([Idmovcaja])
REFERENCES [dbo].[MovimientosCaja] ([IdMovimientoCaja])
GO
ALTER TABLE [dbo].[Gastosvarios] CHECK CONSTRAINT [FK_Gastosvarios_MovimientosCaja]
GO
ALTER TABLE [dbo].[ImpresorasArea]  WITH CHECK ADD  CONSTRAINT [FK_Impresoras_por_Area_Areas_de_Impresion] FOREIGN KEY([Id_Areas_de_Impresion])
REFERENCES [dbo].[AreasImpresion] ([Id_area])
GO
ALTER TABLE [dbo].[ImpresorasArea] CHECK CONSTRAINT [FK_Impresoras_por_Area_Areas_de_Impresion]
GO
ALTER TABLE [dbo].[Ingresosvarios]  WITH CHECK ADD  CONSTRAINT [FK_Ingresosvarios_MovimientosCaja] FOREIGN KEY([Idmovcaja])
REFERENCES [dbo].[MovimientosCaja] ([IdMovimientoCaja])
GO
ALTER TABLE [dbo].[Ingresosvarios] CHECK CONSTRAINT [FK_Ingresosvarios_MovimientosCaja]
GO
ALTER TABLE [dbo].[IniciosSesion]  WITH CHECK ADD  CONSTRAINT [FK_IniciosSesion_Caja] FOREIGN KEY([IdCaja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[IniciosSesion] CHECK CONSTRAINT [FK_IniciosSesion_Caja]
GO
ALTER TABLE [dbo].[IniciosSesion]  WITH CHECK ADD  CONSTRAINT [FK_IniciosSesion_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[IniciosSesion] CHECK CONSTRAINT [FK_IniciosSesion_Usuarios]
GO
ALTER TABLE [dbo].[Mesas]  WITH CHECK ADD  CONSTRAINT [FK_Mesas_SALON] FOREIGN KEY([Id_salon])
REFERENCES [dbo].[SALON] ([Id_salon])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Mesas] CHECK CONSTRAINT [FK_Mesas_SALON]
GO
ALTER TABLE [dbo].[MovimientosCaja]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosCaja_Caja] FOREIGN KEY([IdCaja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[MovimientosCaja] CHECK CONSTRAINT [FK_MovimientosCaja_Caja]
GO
ALTER TABLE [dbo].[MovimientosCaja]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosCaja_Usuarios] FOREIGN KEY([Idusuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[MovimientosCaja] CHECK CONSTRAINT [FK_MovimientosCaja_Usuarios]
GO
ALTER TABLE [dbo].[Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Permisos_Modulos] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Modulos] ([IdModulo])
GO
ALTER TABLE [dbo].[Permisos] CHECK CONSTRAINT [FK_Permisos_Modulos]
GO
ALTER TABLE [dbo].[Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Permisos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permisos] CHECK CONSTRAINT [FK_Permisos_Usuarios]
GO
ALTER TABLE [dbo].[Producto1]  WITH CHECK ADD  CONSTRAINT [FK_Producto1_Grupo_de_Productos] FOREIGN KEY([Id_grupo])
REFERENCES [dbo].[Grupo_de_Productos] ([Idline])
GO
ALTER TABLE [dbo].[Producto1] CHECK CONSTRAINT [FK_Producto1_Grupo_de_Productos]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_clientes] FOREIGN KEY([idclientev])
REFERENCES [dbo].[clientes] ([idclientev])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_clientes]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_MovimientosCaja] FOREIGN KEY([Idmovcaja])
REFERENCES [dbo].[MovimientosCaja] ([IdMovimientoCaja])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_MovimientosCaja]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_Usuarios] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_Usuarios]
GO
/****** Object:  StoredProcedure [dbo].[aumentar_tamanio_letra]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[aumentar_tamanio_letra]


as

update Propiedades_de_mesas set Tamanio_letra=Tamanio_letra+10























GO
/****** Object:  StoredProcedure [dbo].[aumentar_tamanio_mesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[aumentar_tamanio_mesa]


as
update Propiedades_de_mesas set x=x+10,y=y+10




















GO
/****** Object:  StoredProcedure [dbo].[buscar_usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[buscar_usuarios]
@buscador varchar(50)
as
select * from Usuarios 
where Login+Nombre+Correo Like '%' + @buscador + '%' and Rol <>'Cliente'



















GO
/****** Object:  StoredProcedure [dbo].[buscarClientes]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[buscarClientes]

@letra VARCHAR(50)
AS BEGIN
SELECT       top  10 idclientev, Nombre
FROM            dbo.clientes 
WHEre Nombre+Cedula+IdentificadorFiscal  LIKE  '%' + @letra+'%' 
END




















GO
/****** Object:  StoredProcedure [dbo].[buscarConceptos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[buscarConceptos]

@letra VARCHAR(50)
AS BEGIN
SELECT       top 10  Id_concepto,Descripcion as Concepto
FROM            ConceptosGastos
WHEre Descripcion  LIKE  '%' + @letra+'%' 
END




















GO
/****** Object:  StoredProcedure [dbo].[buscarGrupos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[buscarGrupos]
@buscador varchar(50)
as
select * from Grupo_de_Productos
where Grupo Like '%' + @buscador + '%' 







GO
/****** Object:  StoredProcedure [dbo].[buscarProductos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[buscarProductos]
@buscador varchar(50)
as
select Descripcion,Precio_de_venta,Id_Producto1,Colores.ColorHtml   from Producto1  
inner join Colores on Colores.Idcolor =Producto1.Idcolor
where Descripcion Like '%' + @buscador + '%' 














GO
/****** Object:  StoredProcedure [dbo].[cerrarCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cerrarCaja]
@IdMovimientoCaja as int,
	 @fechafin datetime,
	  @ingresos numeric(18,2), 
    @egresos numeric(18,2),	
	@VEfectivo numeric(18,2),
	@VCredito numeric(18,2),
	@VTarjeta numeric(18,2),
	@EfectivoCalculado numeric(18,2),
	@EfectivoReal numeric(18,2),
	@EfectivoDiferencia numeric(18,2)

as 
update MovimientosCaja  set 
fechafin=@fechafin ,
ingresos=@ingresos,
egresos=@egresos  ,
VEfectivo =@VEfectivo  ,
VCredito =@VCredito,
VTarjeta=@VTarjeta,
EfectivoCalculado=@EfectivoCalculado, 
EfectivoDiferencia=@EfectivoDiferencia ,
Estado='CAJA CERRADA',
EfectivoReal=@EfectivoReal
where IdMovimientoCaja  =@IdMovimientoCaja

































GO
/****** Object:  StoredProcedure [dbo].[Confirmar_venta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Confirmar_venta]
@idventa int,
@montototal as numeric(18,2),
@IGV as numeric(18,2),
@Tipo_de_pago varchar(50),
@Estado varchar(50),
@Comprobante varchar(50),
@fecha_venta datetime,
@Fecha_de_pago varchar(50),
@Pago_con numeric(18,2),
@Referencia_tarjeta varchar(50),
@Vuelto as numeric(18,2),
@Efectivo as numeric(18,2),
@Credito numeric(18,2),
@Tarjeta numeric(18,2),
@PorcentajeImp numeric(18,2),
@Idcliente int,
@Idmovicaja int
as
Declare @Compro as varchar(50)
Declare @Numerocomp as varchar(50)
Declare @NumeroFin as int
set @Compro = (select Tipo from Serializacion where Tipo=@Comprobante)
set @NumeroFin=(select (numerofin+1) from Serializacion where Tipo=@Comprobante)
set @Numerocomp =(select  Serie +'-'+ convert(varchar(50),@NumeroFin) from Serializacion where Tipo=@Comprobante)
update Serializacion set numerofin=@NumeroFin where Tipo=@Compro
update ventas set Monto_total  =@montototal, IGV=@IGV ,
Tipo_de_pago=@Tipo_de_pago 
,Estado=@Estado ,
Comprobante =@Compro ,
Numero_de_doc=@Numerocomp ,
fecha_venta=@fecha_venta ,Fecha_de_pago =@Fecha_de_pago ,
Pago_con=@Pago_con,Referencia_tarjeta=@Referencia_tarjeta,
Vuelto=@Vuelto ,Efectivo=@Efectivo,Credito=@Credito,Tarjeta=@Tarjeta ,
Porcentaje_IGV=@PorcentajeImp,
idclientev=@Idcliente,
Idmovcaja=@Idmovicaja
where idventa =@idventa 














GO
/****** Object:  StoredProcedure [dbo].[contar_productos_por_grupo]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[contar_productos_por_grupo]
@idgrupo int
as
select count(Id_Producto1 ) from Producto1
where Id_grupo=@idgrupo 



















GO
/****** Object:  StoredProcedure [dbo].[ContarInicioSesion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ContarInicioSesion]
@idcaja int
as
select count(Idsesion) from IniciosSesion
where IdCaja=@idcaja



















GO
/****** Object:  StoredProcedure [dbo].[ContarMesasXsalon]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ContarMesasXsalon]
@Idsalon int
as
Select count(Id_mesa)  from Mesas Where Estado_de_vida='ACTIVO'
and Id_salon=@Idsalon and Mesa <>'NULO'



















GO
/****** Object:  StoredProcedure [dbo].[contarVentasMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[contarVentasMesa]
@Idmesa int
as
select count(idventa) idventa from ventas 
where Id_mesa =@Idmesa and Estado <>'TERMINADO'














GO
/****** Object:  StoredProcedure [dbo].[disminuir_tamanio_letra]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[disminuir_tamanio_letra]


as
if exists(select Tamanio_letra  from  Propiedades_de_mesas Where Tamanio_letra<=9)
RAISERROR ('Exede el Limite de Medida', 16,1)
else
update Propiedades_de_mesas set Tamanio_letra=Tamanio_letra-10



















GO
/****** Object:  StoredProcedure [dbo].[disminuir_tamanio_mesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[disminuir_tamanio_mesa]


as

if exists(select x,y from  Propiedades_de_mesas Where x=92 and y=77)
RAISERROR ('Exede el Limite de medida', 16,1)
else
update Propiedades_de_mesas set x=x-10,y=y-10





















GO
/****** Object:  StoredProcedure [dbo].[editaMesaenVentas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[editaMesaenVentas]
@Id_mesa int
,@Idventa int
as
update ventas  set Id_mesa=@Id_mesa 
where ventas.idventa=@Idventa  


























GO
/****** Object:  StoredProcedure [dbo].[editar_mesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editar_mesa]

@mesa as varchar(50)
,@id_mesa as int
as
if EXISTS (SELECT Mesa FROM Mesas  where (Mesa  = @mesa and Id_mesa  <>@Id_mesa ))

RAISERROR ('Ya Existe una mesa con este Nombre, POR FAVOR INGRESE DE NUEVO', 16,1)
else
update Mesas set Mesa=@mesa 
where Id_mesa=@id_mesa 





















GO
/****** Object:  StoredProcedure [dbo].[editar_Modulos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[editar_Modulos]
@IdModulo As int,
@Modulo As varchar(MAX)
As
UPDATE Modulos Set

Modulo=@Modulo
Where IdModulo=@IdModulo




















GO
/****** Object:  StoredProcedure [dbo].[editar_serializacion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editar_serializacion]
@serie VARCHAR(50)  ,
@Cantidad_de_numeros AS VARCHAR(50)    ,
@numerofin as int ,
@Tipo as varchar(50), 
   @Id_serie as int
as 
update Serializacion set  Serie =@serie   ,Cantidad_de_numeros=@Cantidad_de_numeros ,numerofin=@numerofin   
         ,Tipo=@Tipo 
where Id_serializacion=@Id_serie 
























GO
/****** Object:  StoredProcedure [dbo].[editar_Usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[editar_Usuarios]
@IdUsuario As int,
@Nombre As varchar(MAX),
@Login As varchar(MAX),
@Password As varchar(MAX),
@Icono As image,
@Correo As varchar(MAX),
@Rol As varchar(MAX)
As
if EXISTS(Select Login,IdUsuario  from Usuarios where Login=@Login and IdUsuario <>@IdUsuario )
RAISERROR ('Usuario en Uso, usa otro nombre de Usuario por favor', 16,1)
else
UPDATE Usuarios Set

Nombre=@Nombre,
Login=@Login,
Password=@Password,
Icono=@Icono,
Correo=@Correo,
Rol=@Rol
Where IdUsuario=@IdUsuario




















GO
/****** Object:  StoredProcedure [dbo].[editarAdespachado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarAdespachado]
@Iddetalleventa int
as
update detalle_venta  set Estado ='DESPACHADO'
WHERE iddetalle_venta  =@Iddetalleventa




















GO
/****** Object:  StoredProcedure [dbo].[editarAenpreparacion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarAenpreparacion]
@Iddetalleventa int
as
update detalle_venta set Estado ='EN PREPARACION'
WHERE iddetalle_venta =@Iddetalleventa

























GO
/****** Object:  StoredProcedure [dbo].[editarAenviado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarAenviado]
@Iddetalleventa int
as
update detalle_venta set Estado ='ENVIADO'
WHERE iddetalle_venta =@Iddetalleventa and Estado='EN ESPERA'



















GO
/****** Object:  StoredProcedure [dbo].[editarAenviadoSolo]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarAenviadoSolo]
@Iddetalleventa int
as
update detalle_venta set Estado ='ENVIADO'
WHERE iddetalle_venta =@Iddetalleventa 



















GO
/****** Object:  StoredProcedure [dbo].[editarAespera]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarAespera]
@Iddetalleventa int
as
update detalle_venta set Estado ='EN ESPERA'
WHERE iddetalle_venta =@Iddetalleventa



















GO
/****** Object:  StoredProcedure [dbo].[editarConceptos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROC [dbo].[editarConceptos]
@Idconcepto As int,
@Descripcion As varchar(MAX)
As
if EXISTS(Select Descripcion  from ConceptosGastos where Descripcion=@Descripcion and Id_concepto <>@Idconcepto )
RAISERROR ('Concepto en uso', 16,1)
else
UPDATE ConceptosGastos Set

Descripcion=@Descripcion
Where Id_concepto=@Idconcepto



















GO
/****** Object:  StoredProcedure [dbo].[EditarDetalleventaEnviado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditarDetalleventaEnviado] 
@Iddetalle int
as
update detalle_venta set Estado='ENVIADO'
WHERE iddetalle_venta =@Iddetalle 



















GO
/****** Object:  StoredProcedure [dbo].[editarDineroInicial]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarDineroInicial]
@Id_caja as integer,
@EfectivoInicial numeric(18,2)


as
update MovimientosCaja   set  EfectivoInicial =@EfectivoInicial
where IdCaja =@Id_caja AND Estado ='CAJA APERTURADA'



















GO
/****** Object:  StoredProcedure [dbo].[EditarDvCambioMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[EditarDvCambioMesa]
@id_mesa_destino int ,
@id_mesa_origen int 
as
update ventas set Id_mesa=@id_mesa_destino
where  ventas.Id_mesa = @id_mesa_origen



















GO
/****** Object:  StoredProcedure [dbo].[editarEmpresa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROC [dbo].[editarEmpresa]
@Nombre_Empresa As varchar(50),
@Logo As image,
@Impuesto As varchar(50),
@Porcentaje_impuesto As numeric(18,0),
@Moneda As varchar(50),
@Trabajas_con_impuestos As varchar(50),
@Carpeta_para_copias_de_seguridad As varchar(MAX),
@Pais As varchar(MAX),
@Tiponotas varchar(50)
As
update  EMPRESA set Nombre_Empresa=@Nombre_Empresa,
Logo=@Logo, Impuesto=@Impuesto,Porcentaje_impuesto=@Porcentaje_impuesto,
Moneda=@Moneda,Trabajas_con_impuestos=@Trabajas_con_impuestos,
Carpeta_para_copias_de_seguridad=@Carpeta_para_copias_de_seguridad,
Pais=@Pais, Tiponotas=@Tiponotas










GO
/****** Object:  StoredProcedure [dbo].[editarEstadoDv]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarEstadoDv]
@iddetalleventa int
as
update detalle_venta  set Estado_de_pago ='PAGADO' , Estado ='ENVIADO'
WHERE iddetalle_venta   =@iddetalleventa















GO
/****** Object:  StoredProcedure [dbo].[EditarEstadoMesaLibre]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[EditarEstadoMesaLibre]
@Idmesa int
AS
UPDATE Mesas set Estado_de_Disponibilidad ='LIBRE'
where Id_mesa=@Idmesa 




















GO
/****** Object:  StoredProcedure [dbo].[EditarEstadoMesaOcupado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditarEstadoMesaOcupado]
@Idmesa int
AS
UPDATE Mesas set Estado_de_Disponibilidad ='OCUPADO'
where Id_mesa=@Idmesa 




















GO
/****** Object:  StoredProcedure [dbo].[EditarEstadoVentasEspera]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditarEstadoVentasEspera]
@idventa int
as
update ventas  set Estado ='EN ESPERA'
WHERE idventa  =@idventa




















GO
/****** Object:  StoredProcedure [dbo].[editarFormatoticket]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[editarFormatoticket]
    	  @Identificador_fiscal varchar(max),
           @Direccion varchar(max),
         
           @Provincia_Departamento_Pais varchar(max),
           @Nombre_de_Moneda varchar(max),
           @Agradecimiento varchar(max),
           @pagina_Web_Facebook varchar(max),
           @Anuncio varchar(max),
	   @Datos_fiscales_de_autorizacion varchar(max),
	   @Por_defecto  varchar(max)
    as
    update Ticket set  
	Identificador_fiscal=@Identificador_fiscal ,
          Direccion =@Direccion ,    
           Provincia_Departamento_Pais=@Provincia_Departamento_Pais ,
           Nombre_de_Moneda=@Nombre_de_Moneda ,
          Agradecimiento  =@Agradecimiento ,
          pagina_Web_Facebook = @pagina_Web_Facebook ,
         Anuncio =  @Anuncio,
		   Datos_fiscales_de_autorizacion=@Datos_fiscales_de_autorizacion,
		   Por_defecto=@Por_defecto
	




















GO
/****** Object:  StoredProcedure [dbo].[editarGrupoProd]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editarGrupoProd]
@Grupo As varchar(50),
@Idcolor int,
@Idgrupo int,
@icono image,
@estadoicono varchar(50)
As

if EXISTS (SELECT * FROM Grupo_de_Productos Where Grupo=@Grupo and Idline<>@Idgrupo)
RAISERROR ('YA EXISTE UN GRUPO CON ESTE NOMBRE, Ingrese de nuevo
', 16,1)
Else
update  Grupo_de_Productos
set 
Grupo=@Grupo,
Idcolor= @Idcolor,Icono=@icono
,Estado_de_icono =@estadoicono
where Idline=@Idgrupo 




















GO
/****** Object:  StoredProcedure [dbo].[editarIdmovCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarIdmovCaja]
@Idmovicaja int
as
update ventas set Idmovcaja =@Idmovicaja 
where Estado ='EN ESPERA';














GO
/****** Object:  StoredProcedure [dbo].[editarInicioSesion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarInicioSesion]
@idsesion int,
@idusuario int
as
update IniciosSesion set IdUsuario = @idusuario
where Idsesion = @idsesion



















GO
/****** Object:  StoredProcedure [dbo].[editarMarcan]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[editarMarcan]

	@e varchar(max),
	@fa varchar(max),
	@f  varchar(max),
	@s varchar(max)
    as
	
    UPDATE Marcan SET E=@e, FA=@fa, F=@f 
	where S=@s 




GO
/****** Object:  StoredProcedure [dbo].[EditarMarcanVencidas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[EditarMarcanVencidas]
@E varchar(max),
@Serialpc varchar(max)
as
update Marcan set E=@E
where S=@Serialpc



GO
/****** Object:  StoredProcedure [dbo].[editarMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editarMesa]
@Idmesa As int,
@mesa as varchar(50)

As
update Mesas set Mesa=@mesa
WHERE Id_mesa=@Idmesa



















GO
/****** Object:  StoredProcedure [dbo].[EditarMesaAotra]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditarMesaAotra]
@Idmesa int
AS
UPDATE Mesas set Estado_de_Disponibilidad ='LIBRE'
where Id_mesa=@Idmesa 




















GO
/****** Object:  StoredProcedure [dbo].[EditarmesaAotraOcupada]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditarmesaAotraOcupada]
@Idmesa int
AS
UPDATE Mesas set Estado_de_Disponibilidad ='OCUPADO'
where Id_mesa=@Idmesa 


























GO
/****** Object:  StoredProcedure [dbo].[editarNotaproducto]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create proc [dbo].[editarNotaproducto]
@iddetalleventa int,
@Nota varchar(max)
as
update detalle_venta set Nota=@Nota
where iddetalle_venta=@iddetalleventa










GO
/****** Object:  StoredProcedure [dbo].[editarNotas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarNotas]
@Notas varchar(max),
@Idventa int
as
update ventas set Nota=@Notas 
where idventa=@Idventa 















GO
/****** Object:  StoredProcedure [dbo].[editarProductos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[editarProductos]
@Descripcion As varchar(50),
@Imagen image,
@PrecioVenta numeric(18,2),
@Preciocompra numeric(18,2),
@Estadoimagen varchar(50),
@Idcolor int,
@Idproducto int

As

if EXISTS (SELECT * FROM Producto1 Where Descripcion=@Descripcion and Id_Producto1<>@Idproducto)
RAISERROR ('YA EXISTE UN PRODUCTO CON ESTE NOMBRE, Ingrese de nuevo
', 16,1)
Else
update  Producto1
set 
Descripcion=@Descripcion ,
Imagen=@Imagen ,
Precio_de_venta=@PrecioVenta ,
Precio_de_compra=@Preciocompra,
Estado_imagen=@Estadoimagen,
Idcolor= @Idcolor
where Id_Producto1=@Idproducto 



















GO
/****** Object:  StoredProcedure [dbo].[editarRespaldos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarRespaldos]
@Ultima_fecha_de_copia_date datetime
as
update EMPRESA  set Ultima_fecha_de_copia_date =@Ultima_fecha_de_copia_date














GO
/****** Object:  StoredProcedure [dbo].[editarSalon]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editarSalon]
@Idsalon int,
@salon varchar(50)

As
if EXISTS(select Salon  from SALON  where Salon = @Salon and Id_salon <>@Idsalon  )
RAISERROR('YA EXISTE UN SALON CON ESTE NOMBRE, ingrese de nuevo', 16,1)
else
update SALON set Salon=@salon 
WHERE Id_salon=@Idsalon 



















GO
/****** Object:  StoredProcedure [dbo].[editarSalonRestaurar]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editarSalonRestaurar]
@Idsalon int
As
update SALON set Estado='ACTIVO'
WHERE Id_salon=@Idsalon 



















GO
/****** Object:  StoredProcedure [dbo].[editarSerieDefecto]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editarSerieDefecto]
@Id_serie as int
as 
update Serializacion set  Por_defecto  ='-' 
update Serializacion set  Por_defecto  ='SI'
where Id_serializacion=@Id_serie 
























GO
/****** Object:  StoredProcedure [dbo].[editarTemacaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editarTemacaja]
@Serialpc varchar(max),
@Tema varchar(50)
as
update Caja set Tema=@Tema
where Serial_PC=@Serialpc



GO
/****** Object:  StoredProcedure [dbo].[eliminar_Modulos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[eliminar_Modulos]
@IdModulo As int

As
DELETE FROM Modulos
WHERE IdModulo=@IdModulo



















GO
/****** Object:  StoredProcedure [dbo].[eliminar_Permisos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[eliminar_Permisos]
@IdUsuario As int

As
DELETE FROM Permisos
WHERE IdUsuario=@IdUsuario



















GO
/****** Object:  StoredProcedure [dbo].[eliminar_Serializacion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminar_Serializacion]
@id integer
as
if EXISTS (SELECT Tipo FROM Serializacion  where Tipo ='TICKET' AND Id_serializacion=@id )
RAISERROR ('Este Comprobante no se Eliminara ya que generaria Error', 16,1)
else
delete from Serializacion   where Id_serializacion   =@id 














GO
/****** Object:  StoredProcedure [dbo].[eliminar_Usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[eliminar_Usuarios]
@IdUsuario As int

As
update Usuarios set Estado='ELIMINADO'
WHERE IdUsuario=@IdUsuario



















GO
/****** Object:  StoredProcedure [dbo].[eliminarDetalleventa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[eliminarDetalleventa]
@Iddetalle As int

As
DELETE FROM detalle_venta
WHERE iddetalle_venta =@Iddetalle



















GO
/****** Object:  StoredProcedure [dbo].[eliminarDvincompletos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[eliminarDvincompletos]
@Idventa As int
As
DELETE FROM detalle_venta
WHERE idventa=@Idventa and Estado ='EN ESPERA'














GO
/****** Object:  StoredProcedure [dbo].[eliminarGastos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarGastos]
@Idgasto int
as
delete from Gastosvarios
where Idgasto = @Idgasto



















GO
/****** Object:  StoredProcedure [dbo].[eliminarGrupos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarGrupos]
@Idgrupo int
as
update Grupo_de_Productos set Estado ='ELIMINADO'
WHERE Idline=@Idgrupo 



















GO
/****** Object:  StoredProcedure [dbo].[eliminarImpresorasXarea]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminarImpresorasXarea]
@Idarea int,
@Impresora varchar(max),
@Idcaja int
as
delete from ImpresorasArea 
where Id_Areas_de_Impresion=@Idarea and Impresora =@Impresora
and Idcaja=@Idcaja
GO
/****** Object:  StoredProcedure [dbo].[eliminarIngresos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[eliminarIngresos]
@Idingreso As int

As
DELETE FROM Ingresosvarios
WHERE Idingreso=@Idingreso



















GO
/****** Object:  StoredProcedure [dbo].[eliminarMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[eliminarMesa]
@Idmesa As int

As
update Mesas set Mesa='NULO'
WHERE Id_mesa=@Idmesa



















GO
/****** Object:  StoredProcedure [dbo].[eliminarProductos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarProductos]
@Idproducto int
as
update Producto1 set Estado ='ELIMINADO'
WHERE Id_Producto1 =@Idproducto 



















GO
/****** Object:  StoredProcedure [dbo].[eliminarSalon]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROC [dbo].[eliminarSalon]
@Idsalon As int

As
DELETE FROM SALON
WHERE Id_salon=@Idsalon



















GO
/****** Object:  StoredProcedure [dbo].[eliminarSolicitud]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarSolicitud]
@idventa int
as
delete from SolicitudesImprimir
where Idventa=@idventa

GO
/****** Object:  StoredProcedure [dbo].[eliminarSolicitudEsc]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarSolicitudEsc]
as
delete from SolicitudesImprimir
GO
/****** Object:  StoredProcedure [dbo].[eliminarVenIncomMovil]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminarVenIncomMovil]

@Idmesa int
as
delete from ventas where  Id_mesa =@Idmesa and  Estado ='SIN CONFIRMAR'














GO
/****** Object:  StoredProcedure [dbo].[eliminarVenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminarVenta]
@Idventa int
as
delete from ventas 
where idventa=@Idventa 














GO
/****** Object:  StoredProcedure [dbo].[eliminarVentaIncompleta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminarVentaIncompleta]

@Idmovcaja int
as
delete from ventas where  Idmovcaja =@Idmovcaja and  Estado ='SIN CONFIRMAR'



















GO
/****** Object:  StoredProcedure [dbo].[enumerarVentascocina]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[enumerarVentascocina]
as
select count(ventas.idventa) as contador,ventas.idventa,
ROW_NUMBER() OVER(ORDER BY ventas.idventa ASC) as Orden  from ventas
INNER JOIN detalle_venta on detalle_venta.idventa=ventas.idventa 

where (ventas.Estado<>'SIN CONFIRMAR' 
AND detalle_venta.Estado <> 'DESPACHADO'
and detalle_venta.Estado_de_pago<>'PAGADO' and detalle_venta.Donde_se_consumira ='EN LOCAL')
or (ventas.Estado<>'SIN CONFIRMAR' and detalle_venta.Estado <> 'DESPACHADO' and detalle_venta.Donde_se_consumira ='PARA LLEVAR')
group by ventas.idventa






GO
/****** Object:  StoredProcedure [dbo].[Insertar_caja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Insertar_caja]	
	@descripcion varchar(50),
	 @Tema varchar(50),
	  @Serial_PC varchar(max),
	   @Tipo varchar(50)

    as
	
if EXISTS (SELECT  Descripcion  FROM Caja    where  Descripcion=@descripcion)
RAISERROR ('Ya Existe una Caja con ese Nombre, puede ser que ya se haya creado una Caja para Esta Pc, Ingrese un nombre diferente e intente de Nuevo', 16,1)
else
declare @Estado as varchar(50)
set @Estado ='RECIEN CREADA'

    INSERT INTO Caja values 
	(@descripcion,@Tema ,@Serial_PC ,@Estado,@Tipo
)



















GO
/****** Object:  StoredProcedure [dbo].[insertar_cliente]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[insertar_cliente]
@Nombre varchar(MAX),
           @Direccion varchar(MAX),
            @IdentificadorFiscal varchar(MAX),                      
            @Celular varchar(max),                         
			@Estado as varchar(50)
		,@Saldo numeric(18,2),
		@Cedula varchar(50)
as
		   BEGIN
if EXISTS (SELECT Nombre  FROM clientes  where Nombre  = @Nombre)
RAISERROR ('YA EXISTE UN REGISTRO CON ESE NOMBRE', 16,1)
else
BEGIN
insert into clientes  values 
            (@Nombre
           ,@Direccion
           ,@IdentificadorFiscal
           ,@Celular                    
		   ,@Estado,
		   @Saldo,@Cedula
            )
			end
			end



























GO
/****** Object:  StoredProcedure [dbo].[insertar_Conceptos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_Conceptos]
@Descripcion varchar(max)
as 

if EXISTS (SELECT Descripcion  FROM ConceptosGastos  where Descripcion  = @Descripcion)
RAISERROR ('YA EXISTE UN REGISTRO CON ESE NOMBRE', 16,1)
else
insert into ConceptosGastos 
values ( 

@Descripcion 
)




















GO
/****** Object:  StoredProcedure [dbo].[insertar_EMPRESA]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_EMPRESA]
@Nombre_Empresa As varchar(50),
@Logo As image,
@Impuesto As varchar(50),
@Porcentaje_impuesto As numeric(18,0),
@Moneda As varchar(50),
@Trabajas_con_impuestos As varchar(50),
@Carpeta_para_copias_de_seguridad As varchar(MAX),
@Ultima_fecha_de_copia_date As datetime,
@Pais As varchar(MAX),
@Tiponotas varchar(50)
As
INSERT INTO EMPRESA
Values (
@Nombre_Empresa,
@Logo,
@Impuesto,
@Porcentaje_impuesto,
@Moneda,
@Trabajas_con_impuestos,
@Carpeta_para_copias_de_seguridad,
@Ultima_fecha_de_copia_date,
@Pais,
@Tiponotas)



















GO
/****** Object:  StoredProcedure [dbo].[insertar_Grupo_de_Productos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_Grupo_de_Productos]
@Grupo As varchar(50),
@Por_defecto As varchar(50),
@Icono As image,
@Estado As varchar(50),
@Estado_de_icono As varchar(50),
@Idcolor int
As

if EXISTS (SELECT * FROM Grupo_de_Productos Where Grupo=@Grupo)
RAISERROR ('YA EXISTE UN GRUPO CON ESTE NOMBRE, Ingrese de nuevo
', 16,1)
Else
INSERT INTO Grupo_de_Productos
Values (
@Grupo,
@Por_defecto,
@Icono,
@Estado,
@Estado_de_icono
,@Idcolor)



















GO
/****** Object:  StoredProcedure [dbo].[Insertar_marcan]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Insertar_marcan]

	
	@s varchar(MAX),
	@f varchar(MAX),
	@e varchar(MAX),
	@fa varchar(MAX)
    as
	
    INSERT INTO Marcan values 
	(@s,@f,@e ,@fa)






















GO
/****** Object:  StoredProcedure [dbo].[insertar_mesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insertar_mesa]
@mesa varchar(50),
@idsalon int
AS
declare @Estado_de_vida varchar(50)
declare @Estado_de_Disponibilidad varchar(50)
set @Estado_de_vida ='ACTIVO'
set @Estado_de_Disponibilidad = 'LIBRE'
if EXISTS(select Mesa  from Mesas  where Mesa= @mesa and Mesa <>'NULO')
RAISERROR('YA EXISTE UNA MESA CON ESE NOMBRE, ingrese de nuevo', 16,1)
else
insert into Mesas values (@mesa, @idsalon ,@Estado_de_vida , @Estado_de_Disponibilidad )




















GO
/****** Object:  StoredProcedure [dbo].[insertar_Modulos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_Modulos]
@Modulo As varchar(MAX)
As
INSERT INTO Modulos
Values (
@Modulo)




















GO
/****** Object:  StoredProcedure [dbo].[insertar_MovimientosCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_MovimientosCaja]
@Idusuario As int,
@IdCaja As int

As
 declare @fechainicio As datetime
declare @fechafin As datetime
declare @ingresos As numeric(18,2)
declare @egresos As numeric(18,2)
declare @VEfectivo As numeric(18,2)
declare @VCredito As numeric(18,2)
declare @VTarjeta As numeric(18,2)
declare @EfectivoCalculado As numeric(18,2)
declare @EfectivoReal As numeric(18,2)
declare @EfectivoDiferencia As numeric(18,2)
declare @Estado As varchar(50)
declare @EfectivoInicial As numeric(18,2)


set @fechainicio=Getdate()
set @fechafin =Getdate()
set @ingresos=0
set @egresos=0
set @VEfectivo=0
set @VCredito=0
set @VTarjeta=0
set @EfectivoCalculado=0
set @EfectivoReal=0
set @EfectivoDiferencia=0
set @Estado='CAJA APERTURADA'
set @EfectivoInicial=0
INSERT INTO MovimientosCaja
Values (
@fechainicio,
@fechafin,
@ingresos,
@egresos,
@VEfectivo,
@VCredito,
@VTarjeta,
@Idusuario,
@EfectivoCalculado,
@EfectivoReal,
@EfectivoDiferencia,
@IdCaja,
@Estado,
@EfectivoInicial)



















GO
/****** Object:  StoredProcedure [dbo].[insertar_Permisos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_Permisos]
@IdModulo As int,
@IdUsuario As int
As
INSERT INTO Permisos
Values (
@IdModulo,
@IdUsuario)




















GO
/****** Object:  StoredProcedure [dbo].[insertar_Producto]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertar_Producto]   
           @Descripcion varchar(50),
		    @Imagen image,			             
          @Id_grupo as int	,     
           @Precio_de_venta numeric(18,2)    ,
		    @Precio_de_compra numeric(18,2)    ,

		   @Estado_imagen varchar(50)   ,
		   @Idcolor int
		   AS 
		   declare @Estado varchar(50)
		   SET @Estado='ACTIVO'
if EXISTS (SELECT Descripcion  FROM Producto1  where Descripcion = @Descripcion   )
RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE NOMBRE, POR FAVOR INGRESE DE NUEVO/ SE GENERARA CODIGO AUTOMATICO', 16,1)
		ELSE
		   INSERT INTO Producto1 
     VALUES
		    (
           @Descripcion        
           ,@Imagen         
		    ,@Id_grupo ,
             @Precio_de_venta,
			 @Precio_de_compra,
	   @Estado_imagen
	    
		,@Idcolor 
		,@Estado)
		




















GO
/****** Object:  StoredProcedure [dbo].[insertar_Salon]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_Salon]
@Salon varchar(50)
as
declare @Estado varchar(50)
set @Estado ='ACTIVO'
if EXISTS(select Salon  from SALON  where Salon = @Salon )
RAISERROR('YA EXISTE UN SALON CON ESTE NOMBRE, ingrese de nuevo', 16,1)
else
insert into SALON values(@Salon , @Estado )




















GO
/****** Object:  StoredProcedure [dbo].[insertar_Serializacion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_Serializacion]
@Serie As varchar(50),
@Cantidad_de_numeros As varchar(50),
@numerofin As int,
@Tipo As varchar(50),
@Por_defecto As varchar(50)
As
INSERT INTO Serializacion
Values (
@Serie,
@Cantidad_de_numeros,
@numerofin,
@Tipo,
@Por_defecto)



















GO
/****** Object:  StoredProcedure [dbo].[insertar_Ticket]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_Ticket]

as
declare @Id_Empresa As int set @Id_Empresa=(select Id_empresa from EMPRESA )
declare @Identificador_fiscal As varchar(MAX) set @Identificador_fiscal='Identificador_fiscal'
declare @Direccion As varchar(MAX) set @Direccion='Direccion'
declare @Provincia_Departamento_Pais As varchar(MAX) set @Provincia_Departamento_Pais='Provincia_Departamento_Pais'
declare @Nombre_de_Moneda As varchar(MAX) set @Nombre_de_Moneda='Nombre_de_Moneda'
declare @Agradecimiento As varchar(MAX) set @Agradecimiento='Agradecimiento'
declare @pagina_Web_Facebook As varchar(MAX) set @pagina_Web_Facebook='pagina_Web_Facebook'
declare @Anuncio As varchar(MAX) set @Anuncio='Anuncio'
declare @Datos_fiscales_de_autorizacion As varchar(MAX) set @Datos_fiscales_de_autorizacion='Datos_fiscales_de_autorizacion'
declare @Por_defecto As varchar(MAX) set @Por_defecto='Por_defecto'

INSERT INTO Ticket
Values (
@Id_Empresa,
@Identificador_fiscal,
@Direccion,
@Provincia_Departamento_Pais,
@Nombre_de_Moneda,
@Agradecimiento,
@pagina_Web_Facebook,
@Anuncio,
@Datos_fiscales_de_autorizacion,
@Por_defecto)



















GO
/****** Object:  StoredProcedure [dbo].[insertar_Usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_Usuarios]
@Nombre As varchar(MAX),
@Login As varchar(MAX),
@Password As varchar(MAX),
@Icono As image,
@Correo As varchar(MAX),
@Rol As varchar(MAX),
@Estado As varchar(MAX)
As
if Exists(select Login from Usuarios where Login = @Login)
Raiserror('YA EXISTE UN REGISTRO CON ESE USUARIO, POR FAVOR INGRESE DE NUEVO',16,1)
else
INSERT INTO Usuarios
Values (
@Nombre,
@Login,
@Password,
@Icono,
@Correo,
@Rol,
@Estado)




















GO
/****** Object:  StoredProcedure [dbo].[insertar_ventas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[insertar_ventas]
@fecha_venta As datetime,
@Id_usuario As int,
@Nombrellevar As varchar(50),
@Idmovcaja As int,
@Id_mesa As int,
@Numero_personas As int
As
declare @idclientev  as int = (Select clientes.idclientev  from clientes where Nombre= 'GENERICO')
declare @Numero_de_doc As varchar(50) set @Numero_de_doc=0
declare @Monto_total As numeric(18,2) set @Monto_total=0
declare @Tipo_de_pago As varchar(50) set @Tipo_de_pago=0
declare @Estado As varchar(50) set @Estado='SIN CONFIRMAR'
declare @IGV As numeric(18,2) set @IGV=0
declare @Comprobante As varchar(50) set @Comprobante=0
declare @Fecha_de_pago As varchar(50) set @Fecha_de_pago=0
declare @Saldo As numeric(18,2)set @Saldo=0
declare @Pago_con As numeric(18,2)set @Pago_con=0
declare @Porcentaje_IGV As numeric(18,2)set @Porcentaje_IGV=0
declare @Referencia_tarjeta As varchar(50)set @Referencia_tarjeta=0
declare @Vuelto As numeric(18,2)set @Vuelto=0
declare @Efectivo As numeric(18,2)set @Efectivo=0
declare @Credito As numeric(18,2)set @Credito=0
declare @Tarjeta As numeric(18,2)set @Tarjeta=0
declare @Nota as varchar(max) set @Nota='-'

INSERT INTO ventas
Values (
@idclientev,
@fecha_venta,
@Numero_de_doc,
@Monto_total,
@Tipo_de_pago,
@Estado,
@IGV,
@Comprobante,
@Id_usuario,
@Fecha_de_pago,
@Nombrellevar,
@Saldo,
@Pago_con,
@Porcentaje_IGV,
@Idmovcaja,
@Referencia_tarjeta,
@Vuelto,
@Efectivo,
@Credito,
@Tarjeta,
@Id_mesa,
@Numero_personas,
@Nota)



















GO
/****** Object:  StoredProcedure [dbo].[InsertarAreasImpresion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertarAreasImpresion]
as
insert into AreasImpresion (Area,Codigo)
values('Impresora para Envios de Pedidos','pedidos'),('Impresora para Pre-cuenta','precuenta'),
('Impresora para CAJA','CAJA'),('Impresora para Reportes','reportes')



















GO
/****** Object:  StoredProcedure [dbo].[InsertarColores]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertarColores]
as
insert into Colores (ColorHtml)
values('#3ED0B0'),('#D95451'),('#D83BC9'),
('#509D52'),('#6541A1'),('#24629F'),('#F78310')



















GO
/****** Object:  StoredProcedure [dbo].[insertarDetalle_venta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertarDetalle_venta]
@idventa As int,
@Id_producto As int,
@cantidad As numeric(18,2),
@preciounitario As numeric(18,2),
@Estado As varchar(50),
@Costo As numeric(18,2),
@Estado_de_pago As varchar(50),
@Donde_se_consumira varchar(50)

As
declare @Nota as varchar(max)
set @Nota='-'
--if Exists(select Id_producto,idventa from detalle_venta 
--where Id_producto=@Id_producto and idventa=@idventa)
--update detalle_venta set 
--cantidad=cantidad+@cantidad 
--where idventa =@idventa and Id_producto=@Id_producto 
--else
INSERT INTO detalle_venta
Values (
@idventa,
@Id_producto,
@cantidad,
@preciounitario,
@Estado,
@Costo,
@Estado_de_pago,
@Donde_se_consumira,
@Nota)



















GO
/****** Object:  StoredProcedure [dbo].[insertarDetalleVentaDiv]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insertarDetalleVentaDiv]
@idventadividido int
,@iddetalleventa as int
as
update detalle_venta set idventa=@idventadividido
where iddetalle_venta = @iddetalleventa




















GO
/****** Object:  StoredProcedure [dbo].[insertarGastos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertarGastos]

@fecha datetime,
@importe numeric(18,2),
@Descripcion varchar(500),
@Id_concepto int,
@Id_usuario int,
@Idmovcaja int
as 

insert into GASTOSVARIOS 
values (@fecha, @importe,@Descripcion ,@Id_concepto,@Id_usuario,@Idmovcaja )






















GO
/****** Object:  StoredProcedure [dbo].[InsertarImpresorasArea]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertarImpresorasArea]
@Id_areas_de_impresion int,
@Impresora as varchar(max),
@Idcaja int
as
if exists(select Impresora  from ImpresorasArea where
Impresora=@Impresora and Id_Areas_de_Impresion =@Id_areas_de_impresion and
Idcaja=@Idcaja)

raiserror('Impresora ya agregada',16,1)
else
insert into ImpresorasArea 
values(@Id_areas_de_impresion,@Impresora,@Idcaja)

























GO
/****** Object:  StoredProcedure [dbo].[insertarIngresos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertarIngresos]

@fecha datetime,
@importe numeric(18,2),
@Descripcion varchar(500),
@Id_usuario int,
@Idmovcaja int
as 

insert into Ingresosvarios  
values (@fecha, @importe,@Descripcion ,@Idmovcaja,@Id_usuario)



















GO
/****** Object:  StoredProcedure [dbo].[insertarInicioSesion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insertarInicioSesion]
@idcaja int,
@idusuario int
as
insert into IniciosSesion 
values(@idcaja ,@idusuario)



















GO
/****** Object:  StoredProcedure [dbo].[insertarMesaLlevar]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertarMesaLlevar]
as
declare @idsalon int set @idsalon=(select Top 1 Id_salon from SALON )
declare @mesa varchar(50) set @mesa='!@PARA LLEVAR@!' 
declare @Estado_de_vida varchar(50) set @Estado_de_vida='!@PARA LLEVAR@!' 
declare @Estado_de_Disponibilidad varchar(50) set @Estado_de_Disponibilidad='!@PARA LLEVAR@!' 

insert into Mesas 
values(@mesa,@idsalon,@Estado_de_vida,@Estado_de_Disponibilidad)















GO
/****** Object:  StoredProcedure [dbo].[insertarPropidadesMesas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insertarPropidadesMesas]
as
declare @x int
declare @y int
declare @tamanioletra int
set @x=136
set @y=110
set @tamanioletra=10
insert into Propiedades_de_mesas 
values(@x,@y,@tamanioletra)



















GO
/****** Object:  StoredProcedure [dbo].[InsertarSolicitud]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertarSolicitud]
@Idventa int,
@Tipo varchar(50)
as
insert into SolicitudesImprimir
values(
@Idventa,
@Tipo)

GO
/****** Object:  StoredProcedure [dbo].[mostrar_AreasImpresion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[mostrar_AreasImpresion]
As
Select * FROM AreasImpresion




















GO
/****** Object:  StoredProcedure [dbo].[mostrar_id_salon_recien_ingresado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrar_id_salon_recien_ingresado]
@Salon as varchar(50)
AS
select Id_salon from SALON
where Salon= @Salon




















GO
/****** Object:  StoredProcedure [dbo].[mostrar_mesas_por_salon]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_mesas_por_salon]
@id_salon int
AS
select Mesas.*,Propiedades_de_mesas.*  from Mesas inner join SALON on SALON.Id_salon = Mesas.Id_salon  
cross join Propiedades_de_mesas
wHERE Mesas.Id_salon = @id_salon and Mesas.Mesa <>'!@PARA LLEVAR@!'













GO
/****** Object:  StoredProcedure [dbo].[mostrar_mesas_por_salon_ventas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_mesas_por_salon_ventas]
@id_salon int
AS
select Mesas.*,Propiedades_de_mesas.*  from Mesas inner join SALON on SALON.Id_salon = Mesas.Id_salon  
cross join Propiedades_de_mesas
wHERE Mesas.Id_salon = @id_salon and Mesas.Mesa<>'NULO' and Mesas.Mesa <>'!@PARA LLEVAR@!'














GO
/****** Object:  StoredProcedure [dbo].[mostrar_Modulos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[mostrar_Modulos]
As
Select * FROM Modulos





















GO
/****** Object:  StoredProcedure [dbo].[mostrar_mozo_por_mesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrar_mozo_por_mesa]
as
select Usuarios.Login, Mesas.Mesa    from ventas inner join Usuarios on
Usuarios.idUsuario=ventas.Id_usuario 
inner join Mesas on Mesas.Id_mesa =ventas.Id_mesa 
where Mesas.Estado_de_Disponibilidad ='OCUPADO' and ventas.Estado <>'TERMINADO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrar_Permisos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[mostrar_Permisos]
@idusuario int 
As
Select Permisos.IdModulo,Modulos.Modulo  FROM 
Permisos 
inner join Modulos on Modulos.IdModulo=Permisos.IdModulo

where IdUsuario=@idusuario 









GO
/****** Object:  StoredProcedure [dbo].[mostrar_productos_por_cuenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_productos_por_cuenta]
@idventa as int
,@Id_mesa int
as
SELECT      Producto1 .Id_Producto1  ,( Producto1 .Descripcion ) as Producto,
dbo.detalle_venta.cantidad as Cant, 
                         dbo.detalle_venta.preciounitario as P_Unit 
						 ,convert(numeric(18,2),dbo.detalle_venta.Total_a_pagar) as Importe
						 ,DBO.detalle_venta.iddetalle_venta ,dbo.ventas.Estado 
						 ,dbo.detalle_venta.cantidad ,ventas.idclientev 
						   ,detalle_venta.idventa
						  
FROM            dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa 
						 inner join Producto1 on detalle_venta.Id_producto=Producto1.Id_Producto1 
						 inner join Mesas on Mesas.Id_mesa=ventas.Id_mesa  
where dbo.detalle_venta .idventa =@idventa AND detalle_venta.cantidad >0 and ventas.Id_mesa = @Id_mesa
and detalle_venta.Estado_de_pago ='DEBE' OR ( ventas .Estado ='TERMINADO_EN_ESPERA' 
and dbo.detalle_venta .idventa =@idventa and ventas.Id_mesa = @Id_mesa)
order by detalle_venta.iddetalle_venta desc




















GO
/****** Object:  StoredProcedure [dbo].[mostrar_Productos_por_grupo]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_Productos_por_grupo]
@id_grupo int,
@buscador varchar(50)
as

select Producto1.*, Colores.ColorHtml,EMPRESA.Moneda from Producto1 INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo
inner join 
Colores on Colores.Idcolor= Producto1.Idcolor
cross join EMPRESA
where Descripcion Like '%' +@buscador + '%' and  Producto1.Id_grupo =@id_grupo 












GO
/****** Object:  StoredProcedure [dbo].[mostrar_ticket_impreso]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_ticket_impreso]
@idventa as int,
@total_en_letras as varchar(50)


as
declare @cantidad_de_productos as int
set @cantidad_de_productos = (select SUM(detalle_venta.cantidad) from detalle_venta inner join ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa
 where detalle_venta.idventa=@idventa)
SELECT      (Producto1.Descripcion ) as Producto, dbo.detalle_venta.cantidad as Cant, 
                         dbo.detalle_venta.preciounitario as P_Unit ,convert(numeric(18,2),dbo.detalle_venta.Total_a_pagar) as Importe, detalle_venta .Id_producto  ,DBO.detalle_venta.iddetalle_venta ,dbo.ventas.Estado 
						  ,dbo.detalle_venta.cantidad ,ventas.idclientev 
						  ,ventas.idventa,Empresa.Nombre_Empresa as Empresa
,Empresa.Logo,Ticket.Identificador_fiscal,Ticket.Direccion,Ticket.Provincia_Departamento_Pais,Ticket.Nombre_de_Moneda,
Ticket.Agradecimiento,Ticket.pagina_Web_Facebook,Ticket.Anuncio,ventas.Numero_de_doc ,ventas.fecha_venta as fecha
,Empresa.Impuesto +'('+ convert(varchar(50),EMPRESA.Porcentaje_impuesto) +'%):' as Impuesto
,convert(numeric (18,2),(ventas.Monto_total*ventas.Porcentaje_IGV)/100 )as TotalImpuesto,
ventas.Monto_total ,Usuarios.Nombre as Usuario,
ventas.Pago_con   , ventas.Vuelto ,EMPRESA.Moneda ,clientes.Nombre ,ticket.Datos_fiscales_de_autorizacion
,ventas.Tipo_de_pago,@cantidad_de_productos as Cantidad_de_productos,@total_en_letras AS total_en_letras,ventas.Comprobante
,convert(numeric (18,2),(ventas.Monto_total-( ventas.Monto_total*ventas.Porcentaje_IGV)/100) ) as SubTotal
FROM            dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa inner join Usuarios on Usuarios.idUsuario=ventas.Id_usuario 
						 INNER JOIN clientes ON clientes.idclientev =ventas.idclientev 
						  cross join Empresa
						 cross join Ticket
						 inner join Producto1 on Producto1.Id_Producto1 =detalle_venta.Id_producto 
where dbo.detalle_venta .idventa =@idventa  And ventas.Monto_total >0 order by detalle_venta.iddetalle_venta desc






























GO
/****** Object:  StoredProcedure [dbo].[mostrar_Usuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[mostrar_Usuarios]
As
Select * FROM Usuarios where Rol <>'Cliente'












GO
/****** Object:  StoredProcedure [dbo].[mostrarCajaRemota]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[mostrarCajaRemota]
as
select Id_Caja
from Caja 
where Tipo ='REMOTA'













GO
/****** Object:  StoredProcedure [dbo].[mostrarCajaSerial]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc  [dbo].[mostrarCajaSerial]
@Serial as varchar(max)
as
select Id_Caja,Tipo,Tema
from Caja 
where Serial_PC =@Serial




GO
/****** Object:  StoredProcedure [dbo].[MostrarCantidadPersonas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[MostrarCantidadPersonas]
@Idventa int
as
select Numero_personas from ventas
where idventa=@Idventa






GO
/****** Object:  StoredProcedure [dbo].[mostrarClienteEstandar]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarClienteEstandar]
as
select idclientev  from clientes 
where Nombre='GENERICO'














GO
/****** Object:  StoredProcedure [dbo].[mostrarColorxGrupo]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarColorxGrupo]
@Idgrupo int
as
Select Colores.ColorHtml,Colores.Idcolor,Icono,Estado_de_icono,Grupo_de_Productos.Grupo   from Grupo_de_Productos inner join
Colores on Colores.Idcolor= Grupo_de_Productos.Idcolor
where Idline=@Idgrupo 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarColorxProducto]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarColorxProducto]
@Idproducto int
as
Select Colores.ColorHtml,Colores.Idcolor,Imagen ,Estado_imagen,Producto1.Precio_de_compra ,
Producto1.Precio_de_venta  from Producto1 inner join
Colores on Colores.Idcolor= Producto1.Idcolor
where Producto1.Id_Producto1=@Idproducto 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarCompDefecto]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarCompDefecto]
as
Select Tipo from Serializacion
where Por_defecto='SI'














GO
/****** Object:  StoredProcedure [dbo].[mostrarCuentasMesas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarCuentasMesas]
as
select distinct Mesas.Mesa ,Mesas.Id_mesa from ventas inner join Mesas 
on Mesas.Id_mesa=ventas.Id_mesa 
where ventas.Estado<>'TERMINADO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarDetalleVenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarDetalleVenta]
@Id_mesa int,
@Idventa int
as
SELECT      Producto1 .Id_Producto1  ,( Producto1 .Descripcion ) as Producto, dbo.detalle_venta.cantidad as Cant, 
                         dbo.detalle_venta.preciounitario as P_Unit ,
						 convert(numeric(18,2),dbo.detalle_venta.Total_a_pagar) as Importe ,
						 DBO.detalle_venta.iddetalle_venta ,dbo.detalle_venta.Estado 
						 
						   ,detalle_venta.idventa
						  ,Colores.ColorHtml,detalle_venta.Nota as NotaProducto
FROM            dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa 
						 inner join Producto1 on detalle_venta.Id_producto=Producto1.Id_Producto1 
						 inner join Colores on Producto1.Idcolor =Colores.Idcolor
where  detalle_venta.cantidad >0 and ventas.Id_mesa = @Id_mesa and detalle_venta.idventa=@Idventa 
and detalle_venta.Estado_de_pago ='DEBE'
order by detalle_venta.iddetalle_venta desc



















GO
/****** Object:  StoredProcedure [dbo].[mostrarDetalleVentaCocina]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarDetalleVentaCocina]
@Idventa int
as
SELECT     Mesas.Mesa , Usuarios.Nombre as Mozo, Producto1 .Descripcion  as Producto, 
detalle_venta.cantidad as Cant, 
                         detalle_venta.iddetalle_venta ,dbo.detalle_venta .Estado 
						 ,dbo.detalle_venta.cantidad 
						   ,detalle_venta.idventa, ventas.Id_mesa , ROW_NUMBER() OVER(ORDER BY detalle_venta.iddetalle_venta ASC) as Orden
						  ,'('+RIGHT( CONVERT(DATETIME, ventas.fecha_venta, 108),8) + ')' as Hora,
						convert(varchar(50),  DATEDIFF( minute ,ventas.fecha_venta ,getdate() ))+' min' as Minutos_transcurridos
						,detalle_venta.Donde_se_consumira as Consumo,ventas.Nombrellevar
						,ventas.Nota,detalle_venta.Nota as Notaproducto 
FROM           detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa 
						 inner join Producto1 on detalle_venta.Id_producto=Producto1.Id_Producto1 
						 inner join Mesas on Mesas .Id_mesa= ventas.Id_mesa 
						 inner join Usuarios on Usuarios.IdUsuario= ventas.Id_usuario 

						 where detalle_venta.idventa  =@idventa AND detalle_venta .Estado <>'DESPACHADO' 






GO
/****** Object:  StoredProcedure [dbo].[mostrarGastosPorCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE proc [dbo].[mostrarGastosPorCaja]
@Idmovcaja int
as
Select Gastosvarios.*,ConceptosGastos.Descripcion as Concepto from Gastosvarios
inner join ConceptosGastos on ConceptosGastos.Id_concepto=Gastosvarios.Id_concepto
where Idmovcaja=@Idmovcaja




















GO
/****** Object:  StoredProcedure [dbo].[mostrarGruposProd]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarGruposProd]
as
Select Idline,Grupo,Colores.ColorHtml from Grupo_de_Productos inner join
Colores on Colores.Idcolor= Grupo_de_Productos.Idcolor Where Estado = 'ACTIVO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarGruposProdConfig]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarGruposProdConfig]
@buscador varchar(50)
as
Select Idline,Grupo,Colores.ColorHtml,Icono,Estado_de_icono,Estado,Colores.Idcolor from Grupo_de_Productos inner join
Colores on Colores.Idcolor= Grupo_de_Productos.Idcolor
where Grupo Like '%' +@buscador + '%'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarIdventaMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarIdventaMesa]
@Id_mesa int
as
select Max(idventa) idventa  from ventas
where Id_mesa=@Id_mesa and Estado <>'TERMINADO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarIdventasMesa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarIdventasMesa]
@Id_mesa int
as
select idventa,Nota  from ventas
where Id_mesa=@Id_mesa and Estado <>'TERMINADO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarImpresorasArea]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarImpresorasArea]
@idarea int,
@Idcaja int
as
select * 
 from ImpresorasArea 
where Id_Areas_de_Impresion =@idarea and Idcaja =@Idcaja

























GO
/****** Object:  StoredProcedure [dbo].[mostrarImpresorasAreaCod]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarImpresorasAreaCod]
@CodigoArea varchar(50),
@Idcaja int
as
select ImpresorasArea.Impresora 
 from ImpresorasArea inner join AreasImpresion on
 AreasImpresion.Id_area=ImpresorasArea.Id_Areas_de_Impresion 
 where AreasImpresion.Codigo =@CodigoArea and
 Idcaja=@Idcaja




















GO
/****** Object:  StoredProcedure [dbo].[mostrarIngresosPorCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create proc [dbo].[mostrarIngresosPorCaja]
@Idmovcaja int
as
Select * from Ingresosvarios 

where Idmovcaja=@Idmovcaja




















GO
/****** Object:  StoredProcedure [dbo].[mostrarInicioSesion]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarInicioSesion]
@idcaja int
as
select * from IniciosSesion 
where IdCaja = @idcaja 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarMinPedido]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarMinPedido]
@idventa as integer
as
SELECT    
			convert(varchar(50),  DATEDIFF( minute ,ventas.fecha_venta ,getdate() ))+' min' as Minutos_transcurridos
FROM            dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa 
						 inner join Producto1 on detalle_venta.Id_producto=Producto1.Id_Producto1 
						

						 where detalle_venta.idventa  =@idventa AND detalle_venta .Estado <>'DESPACHADO'



















GO
/****** Object:  StoredProcedure [dbo].[mostrarMovCajaremota]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarMovCajaremota]
as
select IdMovimientoCaja from MovimientosCaja 
inner join Caja on Caja.Id_Caja=MovimientosCaja.IdCaja 
where Caja.Tipo='REMOTA'















GO
/****** Object:  StoredProcedure [dbo].[MostrarMovCajaUser]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MostrarMovCajaUser]

@serial varchar(max),
@idusuario int

AS
SELECT IdMovimientoCaja FROM MovimientosCaja  
inner join Caja on Caja.Id_Caja = MovimientosCaja.IdCaja
 where Caja.Serial_PC = @serial    AND MovimientosCaja.Estado='CAJA APERTURADA' 
 and MovimientosCaja.Idusuario=@idusuario 




















GO
/****** Object:  StoredProcedure [dbo].[MostrarMovimientosCaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MostrarMovimientosCaja]

@serial varchar(max)

AS
SELECT IdMovimientoCaja,Usuarios.Nombre,MovimientosCaja.fechainicio ,MovimientosCaja.EfectivoInicial 
,MovimientosCaja.IdCaja 
FROM MovimientosCaja  
inner join Caja on Caja.Id_Caja = MovimientosCaja.IdCaja
inner join Usuarios on Usuarios.IdUsuario=MovimientosCaja.Idusuario
 where Caja.Serial_PC = @serial    AND MovimientosCaja.Estado='CAJA APERTURADA' 




















GO
/****** Object:  StoredProcedure [dbo].[mostrarNotasVentas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarNotasVentas]
@Idventa int
as
select Nota,Mesas.Mesa    from ventas inner join Mesas on Mesas.Id_mesa =ventas.Id_mesa 
where idventa=@Idventa 














GO
/****** Object:  StoredProcedure [dbo].[mostrarPrecioDetalle]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarPrecioDetalle]
@Iddetalle int
as
select Total_a_pagar from detalle_venta 
where iddetalle_venta = @Iddetalle 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarProductos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarProductos]
as
select Producto1.Descripcion,Producto1.Precio_de_venta
,Grupo_de_Productos.Grupo, Grupo_de_Productos.Idline from Producto1 inner join Grupo_de_Productos
on Grupo_de_Productos.Idline=Producto1.Id_grupo












GO
/****** Object:  StoredProcedure [dbo].[mostrarRoles]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarRoles]
@idusario int
as 
select 
Rol 
from Usuarios  
where idUsuario  =@idusario and Estado ='ACTIVO'




















GO
/****** Object:  StoredProcedure [dbo].[mostrarSolicitudesEsc]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[mostrarSolicitudesEsc]
as
select count(Idventa) from SolicitudesImprimir where Tipo='ESC'

GO
/****** Object:  StoredProcedure [dbo].[mostrarSolicitudesImpr]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[mostrarSolicitudesImpr]
as
select Idventa from SolicitudesImprimir where Tipo='MOVIL'
GO
/****** Object:  StoredProcedure [dbo].[mostrarultimaventa]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarultimaventa]
@idmovicaja int
as
select top 1 Monto_total,convert(numeric(18,2), ((Porcentaje_IGV*Monto_total )/100)) as Impuesto ,
Vuelto ,convert(numeric(18,2),(Monto_total - (Monto_total *Porcentaje_IGV )/100)) as Subtotal
,Pago_con 
from ventas
where Monto_total >0 and Idmovcaja  =@idmovicaja 
order by idventa Desc 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarVentasId]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrarVentasId]
@Idventa int
as
select distinct ventas.fecha_venta,
Usuarios.Login,ventas.Numero_personas,
Mesas.Mesa 
from ventas inner join Usuarios on
Usuarios.IdUsuario=ventas.Id_usuario 
inner join Mesas on Mesas.Id_mesa=ventas.Id_mesa 
where ventas.Estado<>'TERMINADO' and
ventas.idventa=@Idventa 



















GO
/****** Object:  StoredProcedure [dbo].[mostrarVentasNoTerminado]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrarVentasNoTerminado]
@idmesa int
as
select * from ventas 
where Estado <>'TERMINADO' and Id_mesa=@idmesa 



















GO
/****** Object:  StoredProcedure [dbo].[MostrarVentasnuevas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[MostrarVentasnuevas]
as
select count(detalle_venta.iddetalle_venta) from ventas inner join detalle_venta on detalle_venta.idventa=ventas.idventa 
where ventas.Estado<>'SIN CONFIRMAR' and detalle_venta.Estado <>'DESPACHADO'



















GO
/****** Object:  StoredProcedure [dbo].[ObtenerIdUsuario]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ObtenerIdUsuario]
@Login varchar(max)
as
select IdUsuario  from Usuarios where Login=@Login 



















GO
/****** Object:  StoredProcedure [dbo].[Paginar_grupos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Paginar_grupos]
@Desde int,
@Hasta int
as
BEGIN
SET NOCOUNT ON;
select
Idline, 
Grupo,
Icono ,
Estado_de_icono ,
Estado
from 
(Select
Idline, 
Grupo,
Icono ,
Estado_de_icono ,Estado
,
ROW_NUMBER() Over (Order By Idline) 'Numero_de_fila' 
From Grupo_de_Productos) as Paginado
Where (Paginado.Numero_de_fila>=@Desde ) and (Paginado.Numero_de_fila <=@Hasta ) and Estado<>'ELIMINADO'
END



















GO
/****** Object:  StoredProcedure [dbo].[paginar_Productos_por_grupo]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[paginar_Productos_por_grupo]
@Desde INT,
@Hasta INT,
@id_grupo int
AS
BEGIN
SET NOCOUNT ON;
SELECT
Id_Producto1,
Descripcion,
Imagen,
Precio_de_venta,
Estado_imagen,
Id_grupo,
Precio_de_compra,
Estado_imagen,
Estado,
Moneda
FROM
(SELECT
Id_Producto1,
Descripcion,
Imagen,
Precio_de_venta,
Estado_imagen,
Producto1 .Id_grupo,
Precio_de_compra
,Producto1.Estado,EMPRESA.Moneda
,

ROW_NUMBER() OVER (ORDER BY Idline) 'Numero_de_fila'
FROM
Producto1 INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo
cross join EMPRESA
where  Producto1.Id_grupo  =@id_grupo
AND Producto1.Estado<>'ELIMINADO'
) AS Paginado
WHERE
(Paginado.Numero_de_fila >= @Desde)AND (Paginado.Numero_de_fila<= @Hasta )
END


























GO
/****** Object:  StoredProcedure [dbo].[probar_impresora]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[probar_impresora]

as
declare @Producto as varchar(50)
declare @Cant as varchar(50)
declare @Mozo as varchar(50)
declare @Mesa as varchar(50)

set @Producto='Producto Prueba'
set @Cant='1'
set @Mozo='Mozo Prueba'
set @Mesa='Mesa Prueba'
SELECT      
		@Producto as Producto,	@Cant as Cant	,@Mozo as Mozo 	,@Mesa	 as Mesa
						  
FROM            dbo.EMPRESA  




















GO
/****** Object:  StoredProcedure [dbo].[ReemplazarSerialPc]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ReemplazarSerialPc]
@Serialpc varchar(max)
as
update Caja set Serial_PC=@Serialpc
where Tipo ='PRINCIPAL'



GO
/****** Object:  StoredProcedure [dbo].[restaurar_usuario]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[restaurar_usuario]
@idusuario as int

as	 
UPDATE Usuarios  SET Estado ='ACTIVO'
WHERE IdUsuario =@idusuario




















GO
/****** Object:  StoredProcedure [dbo].[restaurarGrupos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[restaurarGrupos]
@Idgrupo int
as
update Grupo_de_Productos set Estado ='ACTIVO'
WHERE Idline=@Idgrupo 



















GO
/****** Object:  StoredProcedure [dbo].[RestaurarProductos]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RestaurarProductos]
@Idproducto int
as
update Producto1 set Estado ='ACTIVO'
WHERE Id_Producto1 =@Idproducto 



















GO
/****** Object:  StoredProcedure [dbo].[Rptcierrecaja]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Rptcierrecaja]
@Idcaja int
as
select top 1 *,EMPRESA.Nombre_Empresa,Usuarios.Nombre,Caja.Descripcion
from MovimientosCaja
inner join Caja on Caja.Id_Caja=MovimientosCaja.IdCaja
cross join EMPRESA
inner join Usuarios on Usuarios.IdUsuario=MovimientosCaja.Idusuario
where IdCaja=@Idcaja order by MovimientosCaja.IdMovimientoCaja desc









GO
/****** Object:  StoredProcedure [dbo].[RptcodigosQR]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptcodigosQR]
@Ip varchar(max)
as
select (@Ip + '|' + CONVERT(varchar(50), Mesas.Id_mesa)) as IdmesaIp, Mesas.Mesa from Mesas 
where Mesas.Mesa <>'NULO' and Mesas.Mesa <>'!@PARA LLEVAR@!'










GO
/****** Object:  StoredProcedure [dbo].[RptComprobVenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptComprobVenta]
@Idventa int
as
SELECT  dbo.Producto1.Descripcion as Producto,sum( dbo.detalle_venta.cantidad) as Cant, 
sum(dbo.detalle_venta.preciounitario) as P_unit,
sum(dbo.detalle_venta.Total_a_pagar) as Importe,count(dbo.detalle_venta.iddetalle_venta) as iddetalle_venta, 
dbo.ventas.fecha_venta as fecha,
dbo.ventas.Monto_total, 
                         dbo.Usuarios.Nombre as Usuario, dbo.ventas.Pago_con, 
						 dbo.ventas.Vuelto, dbo.clientes.Nombre AS Cliente,
						 dbo.ventas.Tipo_de_pago,
						 EMPRESA.Nombre_Empresa as Empresa,
						 Ticket.Identificador_fiscal ,
						 Ticket.Direccion ,Ticket.Provincia_Departamento_Pais,
						 ventas.Comprobante ,ventas.Numero_de_doc ,
(ventas.Monto_total /(1+ (Porcentaje_IGV/100) )) as SubTotal,
(EMPRESA.Impuesto+'('+ Convert(varchar (50), EMPRESA.Porcentaje_impuesto) +'%):') as Impuesto,
(ventas.Monto_total - (ventas.Monto_total /(1+ (Porcentaje_IGV/100)) )) as TotalImpuestos,
Ticket.Anuncio,Ticket.pagina_Web_Facebook ,
Ticket.Agradecimiento ,Ticket.Datos_fiscales_de_autorizacion 


FROM            dbo.Ticket INNER JOIN
                         dbo.EMPRESA ON dbo.Ticket.Id_Empresa = dbo.EMPRESA.Id_empresa CROSS JOIN
                         dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa INNER JOIN
                         dbo.clientes ON dbo.ventas.idclientev = dbo.clientes.idclientev INNER JOIN
                         dbo.Usuarios ON dbo.ventas.Id_usuario = dbo.Usuarios.IdUsuario INNER JOIN
                         dbo.Producto1 ON dbo.detalle_venta.Id_producto = dbo.Producto1.Id_Producto1

						 where detalle_venta.idventa=@Idventa 

						 group by 
						 dbo.Producto1.Descripcion,
dbo.ventas.fecha_venta ,
dbo.ventas.Monto_total, 
                         dbo.Usuarios.Nombre , dbo.ventas.Pago_con, 
						 dbo.ventas.Vuelto, dbo.clientes.Nombre ,
						 dbo.ventas.Tipo_de_pago,
						 EMPRESA.Nombre_Empresa,
						 Ticket.Identificador_fiscal ,
						 Ticket.Direccion ,Ticket.Provincia_Departamento_Pais,
						 ventas.Comprobante ,ventas.Numero_de_doc ,
EMPRESA.Impuesto, EMPRESA.Porcentaje_impuesto,
Ticket.Anuncio,Ticket.pagina_Web_Facebook ,
Ticket.Agradecimiento ,Ticket.Datos_fiscales_de_autorizacion ,ventas.Porcentaje_IGV












GO
/****** Object:  StoredProcedure [dbo].[RptGastosvarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptGastosvarios]
@Idmovcaja int
as
select sum(Importe) from Gastosvarios
where Idmovcaja=@Idmovcaja



















GO
/****** Object:  StoredProcedure [dbo].[RptIngresosVarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptIngresosVarios]
@Idmovcaja int
as
select sum(Importe) from Ingresosvarios
where Idmovcaja=@Idmovcaja



















GO
/****** Object:  StoredProcedure [dbo].[RptPrecuenta]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptPrecuenta]
@Idventa int
as
SELECT  dbo.Producto1.Descripcion as Producto, dbo.detalle_venta.cantidad as Cant, 
dbo.detalle_venta.preciounitario as P_unit,
dbo.detalle_venta.Total_a_pagar as Importe, dbo.detalle_venta.iddetalle_venta, 
dbo.ventas.fecha_venta as fecha,
dbo.ventas.Monto_total, 
                       dbo.ventas.Pago_con, 
						 dbo.ventas.Vuelto,
						 dbo.ventas.Tipo_de_pago,
						 EMPRESA.Nombre_Empresa as Empresa,
					
						 ventas.Comprobante ,ventas.Numero_de_doc ,

(EMPRESA.Impuesto+'('+ Convert(varchar (50), EMPRESA.Porcentaje_impuesto) +'%):') as Impuesto,
Mesas.Mesa ,EMPRESA.Porcentaje_impuesto,
EMPRESA.Trabajas_con_impuestos as Ti


FROM          
                         dbo.EMPRESA  CROSS JOIN
                         dbo.detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa INNER JOIN
                         dbo.Producto1 ON dbo.detalle_venta.Id_producto = dbo.Producto1.Id_Producto1
						 inner join Mesas on Mesas.Id_mesa =ventas.Id_mesa 

						 where detalle_venta.idventa=@Idventa 














GO
/****** Object:  StoredProcedure [dbo].[RptproductosmasV]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RptproductosmasV]
as
select  Top 5 sum(detalle_venta.cantidad) as Cantidad,Producto1.Descripcion  
from detalle_venta inner join Producto1 on
Producto1.Id_Producto1 =detalle_venta.Id_producto 
group by Producto1.Descripcion 
order by sum(detalle_venta.cantidad) desc












GO
/****** Object:  StoredProcedure [dbo].[RptresumenventasFechas]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RptresumenventasFechas]
@fi date,
@ff date
as
declare @empleado varchar(50)
set @empleado ='Todos'

select convert(date, ventas.fecha_venta) as fecha_venta
,sum(detalle_venta.Total_a_pagar) as Monto,sum(detalle_venta.Costo*detalle_venta.cantidad) as Costo,
sum(detalle_venta.Ganancia)as Ganancia,@fi as fi,@ff as ff, @empleado as Empleado

from ventas inner join detalle_venta 
on detalle_venta.idventa =ventas.idventa 
where ventas.Monto_total >0 and(convert(date,ventas.fecha_venta) >=@fi and convert(date,ventas.fecha_venta)<=@ff)
group by convert(date, ventas.fecha_venta)












GO
/****** Object:  StoredProcedure [dbo].[RptresumenventasFechasUsuarios]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RptresumenventasFechasUsuarios]
@fi date,
@ff date,
@Idusuario int
as
select convert(date, ventas.fecha_venta) as fecha_venta
,sum(detalle_venta.Total_a_pagar) as Monto,sum(detalle_venta.Costo*detalle_venta.cantidad) as Costo,
sum(detalle_venta.Ganancia)as Ganancia,@fi as fi,@ff as ff, Usuarios.Nombre as Empleado

from ventas inner join detalle_venta 
on detalle_venta.idventa =ventas.idventa 
inner join Usuarios on Usuarios.IdUsuario =ventas.Id_usuario
where ventas.Monto_total >0 and(convert(date,ventas.fecha_venta) >=@fi and convert(date,ventas.fecha_venta)<=@ff)
and ventas.Id_usuario =@Idusuario 
group by convert(date, ventas.fecha_venta),Usuarios.Nombre












GO
/****** Object:  StoredProcedure [dbo].[RptresumenventasHoy]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RptresumenventasHoy]

as
declare @fi as varchar(50)
set @fi='Siempre'
declare @ff as varchar(50)
set @ff=CONVERT(varchar(50),  Getdate())
declare @empleado varchar(50)
set @empleado ='Todos'

select convert(date, ventas.fecha_venta) as fecha_venta
,sum(detalle_venta.Total_a_pagar) as Monto,sum(detalle_venta.Costo*detalle_venta.cantidad) as Costo,
sum(detalle_venta.Ganancia)as Ganancia,@fi as fi,@ff as ff, @empleado as Empleado

from ventas inner join detalle_venta 
on detalle_venta.idventa =ventas.idventa 
where ventas.Monto_total >0
group by convert(date, ventas.fecha_venta)












GO
/****** Object:  StoredProcedure [dbo].[RptresumenventasHoyUsuario]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RptresumenventasHoyUsuario]
@Idusuario int
as
declare @fi as varchar(50)
set @fi='Siempre'
declare @ff as varchar(50)
set @ff=CONVERT(varchar(50),  Getdate())

select convert(date, ventas.fecha_venta) as fecha_venta
,sum(detalle_venta.Total_a_pagar) as Monto,sum(detalle_venta.Costo*detalle_venta.cantidad) as Costo,
sum(detalle_venta.Ganancia)as Ganancia,@fi as fi,@ff as ff, Usuarios.Nombre as Empleado

from ventas inner join detalle_venta 
on detalle_venta.idventa =ventas.idventa 
inner join Usuarios on Usuarios.IdUsuario =ventas.Id_usuario
where ventas.Monto_total >0 and ventas.Id_usuario =@Idusuario 
group by convert(date, ventas.fecha_venta),Usuarios.Nombre












GO
/****** Object:  StoredProcedure [dbo].[rptTicketCocina]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[rptTicketCocina]
@Idventa int
as
SELECT     Mesas.Mesa , Usuarios.Nombre as Mozo, Producto1 .Descripcion  as Producto, 
detalle_venta.cantidad as Cant, 
                         detalle_venta.iddetalle_venta ,detalle_venta .Estado 		 
						   ,detalle_venta.idventa, ventas.Id_mesa 								
						,detalle_venta.Donde_se_consumira as Consumo,ventas.Nombrellevar,
						ventas.Nota ,detalle_venta.Nota as NotasIndividuales
FROM           detalle_venta INNER JOIN
                         dbo.ventas ON dbo.detalle_venta.idventa = dbo.ventas.idventa 
						 inner join Producto1 on detalle_venta.Id_producto=Producto1.Id_Producto1 
						 inner join Mesas on Mesas.Id_mesa= ventas.Id_mesa 
						 inner join Usuarios on Usuarios.IdUsuario= ventas.Id_usuario 

						 where detalle_venta.idventa  =@idventa AND detalle_venta.Estado ='EN ESPERA'














GO
/****** Object:  StoredProcedure [dbo].[RptVentasTurno]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RptVentasTurno]
@Idmovcaja int
as

select SUM(Efectivo),SUM(Tarjeta),SUM(Credito)  from ventas 
where    Idmovcaja=@Idmovcaja




















GO
/****** Object:  StoredProcedure [dbo].[validarUsuario]    Script Date: 03/08/2021 10:50:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[validarUsuario]
@password varchar(max),
@login varchar(max)

as
select IdUsuario  from Usuarios
where Password=@password and Login =@login 



















GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "detalle_venta"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "ventas"
            Begin Extent = 
               Top = 6
               Left = 277
               Bottom = 136
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 20
         End
         Begin Table = "clientes"
            Begin Extent = 
               Top = 6
               Left = 500
               Bottom = 136
               Right = 685
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Usuarios"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Producto1"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 432
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EMPRESA"
            Begin Extent = 
               Top = 138
               Left = 470
               Bottom = 268
               Right = 742
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Ticket"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 288
            End
            DisplayFlags' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vistaTicketVenta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vistaTicketVenta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vistaTicketVenta'
GO
USE [master]
GO
ALTER DATABASE [BASEBRIRESTCSHARP] SET  READ_WRITE 
GO
